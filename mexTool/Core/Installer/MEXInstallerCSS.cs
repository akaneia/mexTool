using HSDRaw;
using HSDRaw.Common;
using HSDRaw.Common.Animation;
using HSDRaw.GX;
using HSDRaw.Melee.Mn;
using HSDRaw.MEX;
using HSDRaw.MEX.Menus;
using HSDRaw.MEX.Stages;
using HSDRaw.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mexTool.Core.Installer
{
    public partial class MEXInstaller
    {
        /// <summary>
        /// 
        /// </summary>
        private static void GetIconFromDOL(MEXDOLScrubber dol, MEX_Data data)
        {
            // generate menu table
            data.MenuTable = new MEX_MenuTable();
            data.MenuTable.Parameters = new MEX_MenuParameters()
            {
                CSSHandScale = 1
            };
            dol.ExtractDataFromMap(data.MenuTable);


            // expand stage select node
            var sss = data.MenuTable.SSSIconData._s;
            var stageIcons = new MEX_StageIconData[sss.Length / 0x1C];
            for (int i = 0; i < stageIcons.Length; i++)
            {
                stageIcons[i] = new MEX_StageIconData()
                {
                    _s = new HSDStruct(sss.GetSubData(i * 0x1C, 0x1C))
                };
                stageIcons[i]._s.Resize(0x20);
                stageIcons[i].ExternalID = sss.GetByte(i * 0x1C + 0x0B);
            }
            data.MenuTable.SSSIconData.Array = stageIcons;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="icons"></param>
        /// <returns></returns>
        private static MEX_mexSelectChr GenerateMexSelectChrSymbol(SBM_SelectChrDataTable table, MEX_CSSIcon[] cssIcons)
        {
            // create mexSelectChr struct
            MEX_mexSelectChr mex = new MEX_mexSelectChr();

            // generate icon model
            var icon_joint = HSDAccessor.DeepClone<HSD_JOBJ>(table.MenuModel.Children[2].Child);
            icon_joint.TX = 0;
            icon_joint.TY = 0;
            icon_joint.TZ = 0;
            icon_joint.Next = null;
            var center = RegenerateIcon(icon_joint);

            // generate material_anim_joint
            var icon_matanim_joint = HSDAccessor.DeepClone<HSD_MatAnimJoint>(table.MenuMaterialAnimation.Children[2].Child);
            icon_matanim_joint.Next = null;

            // general base models
            HSD_JOBJ position_joint = new HSD_JOBJ();
            position_joint.Flags = JOBJ_FLAG.CLASSICAL_SCALING | JOBJ_FLAG.ROOT_XLU;
            position_joint.SX = 1; position_joint.SY = 1; position_joint.SZ = 1;

            HSD_AnimJoint anim_joint = new HSD_AnimJoint();

            HSD_MatAnimJoint matanim_joint = new HSD_MatAnimJoint();

            // create icon data
            var joints = table.MenuModel.BreathFirstList;
            var matanims = table.MenuAnimation.BreathFirstList;
            foreach (var ico in cssIcons)
            {
                if (joints[ico.JointID].Dobj == null)
                    continue;

                HSD_JOBJ joint = HSDAccessor.DeepClone<HSD_JOBJ>(icon_joint);
                joint.Dobj.Pobj.Attributes = icon_joint.Dobj.Pobj.Attributes;
                joint.Dobj.Next.Pobj.Attributes = icon_joint.Dobj.Pobj.Attributes;
                joint.Dobj.Next.Mobj.Textures = HSDAccessor.DeepClone<HSD_TOBJ>(joints[ico.JointID].Dobj.Next.Mobj.Textures);

                var worldPosition = new GXVector3(joints[ico.JointID].TX, joints[ico.JointID].TY, joints[ico.JointID].TZ);

                // get anim
                var anim = matanims[ico.JointID].AOBJ;

                // if it's a clone get parent location
                if (ico.JointID < 15)
                {
                    worldPosition = new GXVector3(joints[ico.JointID - 1].TX, joints[ico.JointID - 1].TY, joints[ico.JointID - 1].TZ);
                    anim = matanims[ico.JointID - 1].AOBJ;
                }

                // check animation for world position
                if (anim != null)
                    foreach (var v in anim.FObjDesc.List)
                    {
                        System.Diagnostics.Debug.WriteLine(v.JointTrackType);
                        if (v.JointTrackType == JointTrackType.HSD_A_J_TRAX)
                        {
                            var keys = v.GetDecodedKeys();
                            worldPosition.X = keys[keys.Count - 1].Value;
                        }
                    }

                joint.TX = worldPosition.X + center.X;
                joint.TY = worldPosition.Y + center.Y;
                joint.TZ = worldPosition.Z + center.Z;

                position_joint.AddChild(joint);
                anim_joint.AddChild(new HSD_AnimJoint());
                matanim_joint.AddChild(HSDAccessor.DeepClone<HSD_MatAnimJoint>(icon_matanim_joint));
            }

            mex.IconModel = position_joint;
            mex.IconAnimJoint = anim_joint;
            mex.IconMatAnimJoint = matanim_joint;
            mex.CSPMatAnim = HSDAccessor.DeepClone<HSD_MatAnim>(table.MenuMaterialAnimation.Children[6].Child.MaterialAnimation);

            var cspkeys = mex.CSPMatAnim.TextureAnimation.AnimationObject.FObjDesc.GetDecodedKeys();
            
            foreach(var k in cspkeys)
                if ((k.Frame % 30) >= 19)
                    k.Frame++;

            mex.CSPMatAnim.TextureAnimation.AnimationObject.FObjDesc.SetKeys(cspkeys, (byte)TexTrackType.HSD_A_T_TIMG);
            mex.CSPMatAnim.TextureAnimation.AnimationObject.FObjDesc.Next.SetKeys(cspkeys, (byte)TexTrackType.HSD_A_T_TCLT);

            mex.CSPStride = 30;

            return mex;
        }



        private static Dictionary<int, int> unswizzle = new Dictionary<int, int>()
        {
            { 13, 11},
            { 14, 12},
            { 15, 13},
            { 16, 14},
            { 17, 15},

            { 18, 16},

            { 11, 17},
            { 12, 18},
        };

        private static Dictionary<int, int> texunswizzle = new Dictionary<int, int>()
        {
            { 0, 0},
            { 1, 1},
            { 2, 2},
            { 3, 4},
            { 4, 5},
            { 5, 6},
            { 6, 3},
            { 7, 9},
            { 8, 8},
            { 9, 7},
            { 10, 10},
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stage"></param>
        /// <returns></returns>
        public static MEX_mexMapData LoadIconDataFromVanilla(SBM_MnSelectStageDataTable stage)
        {
            List<HSD_TOBJ> nameTags = new List<HSD_TOBJ>();

            List<HSD_TOBJ> iconTOBJs = new List<HSD_TOBJ>();

            HSD_JOBJ root = new HSD_JOBJ()
            {
                SX = 1,
                SY = 1,
                SZ = 1,
                Flags = JOBJ_FLAG.CLASSICAL_SCALING
            };

            HSD_AnimJoint animRoot = new HSD_AnimJoint();


            var tex0 = stage.IconDoubleMatAnimJoint.Child.Next.MaterialAnimation.Next.TextureAnimation.ToTOBJs();
            var tex0_extra = stage.IconDoubleMatAnimJoint.Child.MaterialAnimation.Next.TextureAnimation.ToTOBJs();
            var tex1 = stage.IconLargeMatAnimJoint.Child.MaterialAnimation.Next.TextureAnimation.ToTOBJs();
            var tex2 = stage.IconSpecialMatAnimJoint.Child.MaterialAnimation.Next.TextureAnimation.ToTOBJs();

            var nameTOBJs = stage.StageNameMatAnimJoint.Child.Child.MaterialAnimation.TextureAnimation.ToTOBJs();
            var nameTOBJsAnim = stage.StageNameMatAnimJoint.Child.Child.MaterialAnimation.TextureAnimation.AnimationObject.FObjDesc.GetDecodedKeys();

            var positionAnimation = new List<HSD_AnimJoint>();
            foreach (var c in stage.PositionAnimation.Children)
            {
                var pos = new HSD_AnimJoint();
                pos.AOBJ = HSDAccessor.DeepClone<HSD_AOBJ>(c.AOBJ);
                positionAnimation.Add(pos);
            }

            var g1 = tex0.Length - 2;
            var g2 = tex0.Length - 2 + tex1.Length - 2;
            var g3 = tex0.Length - 2 + tex1.Length - 2 + tex2.Length - 2;

            for (int i = 0; i < stage.PositionModel.Children.Length; i++)
            {
                var childIndex = i;
                if (unswizzle.ContainsKey(i))
                    childIndex = unswizzle[i];

                HSD_TOBJ icon = null;
                HSD_TOBJ name = null;
                var keys = positionAnimation[childIndex].AOBJ.FObjDesc.GetDecodedKeys();
                var Y = stage.PositionModel.Children[childIndex].TY;
                var Z = stage.PositionModel.Children[childIndex].TZ;
                var SX = 1f;
                var SY = 1f;

                if (i >= g3)
                {
                    //RandomIcon
                    name = nameTOBJs[(int)nameTOBJsAnim[nameTOBJsAnim.Count - 1].Value];
                }
                else
                if (i >= g2)
                {
                    name = nameTOBJs[(int)nameTOBJsAnim[24 + (i - g2)].Value];
                    icon = tex2[i - g2 + 2];
                    SX = 0.8f;
                    SY = 0.8f;
                }
                else
                if (i >= g1)
                {
                    name = nameTOBJs[(int)nameTOBJsAnim[22 + texunswizzle[i - g1]].Value];
                    icon = tex1[i - g1 + 2];
                    SY = 1.1f;
                }
                else
                {
                    icon = tex0[texunswizzle[i] + 2];
                    name = nameTOBJs[(int)nameTOBJsAnim[texunswizzle[i]].Value * 2];

                    root.AddChild(new HSD_JOBJ()
                    {
                        TX = keys[keys.Count - 1].Value,
                        TY = Y,
                        TZ = Z,
                        SX = SX,
                        SY = SY,
                        SZ = 1,
                        Flags = JOBJ_FLAG.CLASSICAL_SCALING
                    });
                    iconTOBJs.Add(icon);
                    nameTags.Add(name);
                    animRoot.AddChild(HSDAccessor.DeepClone<HSD_AnimJoint>(positionAnimation[childIndex]));

                    Y -= 5.6f;
                    Z = 0;
                    icon = tex0_extra[texunswizzle[i] + 2];
                    name = nameTOBJs[(int)nameTOBJsAnim[texunswizzle[i]].Value * 2 + 1];
                }

                root.AddChild(new HSD_JOBJ()
                {
                    TX = keys[keys.Count - 1].Value,
                    TY = Y,
                    TZ = Z,
                    SX = SX,
                    SY = SY,
                    SZ = 1,
                    Flags = JOBJ_FLAG.CLASSICAL_SCALING
                });
                iconTOBJs.Add(icon);
                nameTags.Add(name);
                animRoot.AddChild(HSDAccessor.DeepClone<HSD_AnimJoint>(positionAnimation[childIndex]));
            }


            var extraIcons = stage.IconLargeMatAnimJoint.Child.MaterialAnimation.Next.TextureAnimation.ToTOBJs();
            iconTOBJs.Insert(0, extraIcons[0]);
            iconTOBJs.Insert(0, extraIcons[1]);
            iconTOBJs.Add(extraIcons[0]);


            var iconJOBJ = HSDAccessor.DeepClone<HSD_JOBJ>(stage.IconDoubleModel);
            iconJOBJ.Child = iconJOBJ.Child.Next;

            var iconAnimJoint = HSDAccessor.DeepClone<HSD_AnimJoint>(stage.IconDoubleAnimJoint);
            iconAnimJoint.Child = iconAnimJoint.Child.Next;

            var iconMatAnimJoint = HSDAccessor.DeepClone<HSD_MatAnimJoint>(stage.IconDoubleMatAnimJoint);
            iconMatAnimJoint.Child = iconMatAnimJoint.Child.Next;
            iconMatAnimJoint.Child.MaterialAnimation.Next.TextureAnimation.FromTOBJs(iconTOBJs.ToArray(), true);


            var mapdata = new MEX_mexMapData();

            mapdata.IconModel = iconJOBJ;
            mapdata.IconAnimJoint = iconAnimJoint;
            mapdata.IconMatAnimJoint = iconMatAnimJoint;
            mapdata.PositionModel = root;
            mapdata.PositionAnimJoint = animRoot;
            mapdata.StageNameMaterialAnimation = HSDAccessor.DeepClone<HSD_MatAnimJoint>(stage.StageNameMatAnimJoint);
            mapdata.StageNameMaterialAnimation.Child.Child.MaterialAnimation.TextureAnimation.FromTOBJs(nameTags, true);

            return mapdata;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootJOBJ"></param>
        private static GXVector3 RegenerateIcon(HSD_JOBJ rootJOBJ)
        {
            GXVector3 Min = new GXVector3(float.MaxValue, float.MaxValue, float.MaxValue);
            GXVector3 Max = new GXVector3(float.MinValue, float.MinValue, float.MinValue);

            foreach (var jobj in rootJOBJ.BreathFirstList)
                if (jobj.Dobj != null)
                    foreach (var dobj in jobj.Dobj.List)
                        if (dobj.Pobj != null)
                            foreach (var pobj in dobj.Pobj.List)
                                foreach (var v in pobj.ToDisplayList().Vertices)
                                {
                                    Min.X = Math.Min(Min.X, v.POS.X);
                                    Min.Y = Math.Min(Min.Y, v.POS.Y);
                                    Min.Z = Math.Min(Min.Z, v.POS.Z);
                                    Max.X = Math.Max(Max.X, v.POS.X);
                                    Max.Y = Math.Max(Max.Y, v.POS.Y);
                                    Max.Z = Math.Max(Max.Z, v.POS.Z);
                                }

            var center = new GXVector3((Min.X + Max.X) / 2, (Min.Y + Max.Y) / 2, (Min.Z + Max.Z) / 2);

            var compressor = new POBJ_Generator();
            foreach (var jobj in rootJOBJ.BreathFirstList)
            {
                if (jobj.Dobj != null)
                    foreach (var dobj in jobj.Dobj.List)
                    {
                        if (dobj.Pobj != null)
                        {
                            List<GX_Vertex> triList = new List<GX_Vertex>();

                            foreach (var pobj in dobj.Pobj.List)
                            {
                                var dl = pobj.ToDisplayList();
                                int off = 0;
                                foreach (var pri in dl.Primitives)
                                {
                                    var strip = dl.Vertices.GetRange(off, pri.Count);

                                    if (pri.PrimitiveType == GXPrimitiveType.TriangleStrip)
                                        StripToList(strip, out strip);

                                    if (pri.PrimitiveType == GXPrimitiveType.Quads)
                                        QuadToList(strip, out strip);

                                    off += pri.Count;

                                    for (int i = 0; i < strip.Count; i++)
                                    {
                                        var v = strip[i];

                                        v.POS.X -= center.X;
                                        v.POS.Y -= center.Y;
                                        v.POS.Z -= center.Z;

                                        strip[i] = v;
                                    }

                                    triList.AddRange(strip);
                                }
                            }

                            dobj.Pobj = compressor.CreatePOBJsFromTriangleList(triList, dobj.Pobj.Attributes.Select(e => e.AttributeName).ToArray(), null, null);
                        }
                    }
            }
            compressor.SaveChanges();

            center.X *= rootJOBJ.SX;
            center.Y *= rootJOBJ.SY;
            center.Z *= rootJOBJ.SZ;

            return center;
        }

        /// <summary>
        /// Converts a list of quads into triangles
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="outVertices"></param>
        public static void QuadToList<T>(List<T> vertices, out List<T> outVertices)
        {
            outVertices = new List<T>();

            for (int index = 0; index < vertices.Count; index += 4)
            {
                outVertices.Add(vertices[index]);
                outVertices.Add(vertices[index + 1]);
                outVertices.Add(vertices[index + 2]);
                outVertices.Add(vertices[index + 1]);
                outVertices.Add(vertices[index + 3]);
                outVertices.Add(vertices[index + 2]);
            }
        }

        /// <summary>
        /// Converts a list of triangle strips into triangles
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="outVertices"></param>
        public static void StripToList(List<GX_Vertex> vertices, out List<GX_Vertex> outVertices) 
        {
            outVertices = new List<GX_Vertex>();

            for (int index = 2; index < vertices.Count; index++)
            {
                bool isEven = (index % 2 != 1);

                var vert1 = vertices[index - 2];
                var vert2 = isEven ? vertices[index] : vertices[index - 1];
                var vert3 = isEven ? vertices[index - 1] : vertices[index];

                if (vert1 != vert2
                    && vert2 != vert3
                    && vert3 != vert1)
                {
                    outVertices.Add(vert3);
                    outVertices.Add(vert2);
                    outVertices.Add(vert1);
                }
                else
                {
                    //Console.WriteLine("ignoring degenerate triangle");
                }
            }
        }
    }
}
