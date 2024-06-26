using HSDRaw;
using HSDRaw.Common;
using HSDRaw.Melee.Mn;
using HSDRaw.MEX;
using System;
using System.IO;
using System.Linq;

namespace mexTool.Core.Installer
{
    public partial class MEXInstaller
    {
        private static readonly uint EffectStringOffset = 0x3BD25C;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public static bool InstallMEX(ImageResource resource)
        {
            // patch dol
            resource.SetDOL(MEXDolPatcher.ApplyPatch(resource.GetDOL(), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib/dol.patch")));

            //using (var src = new MemoryStream(resource.GetDOL()))
            //using (System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider())
            //{
            //    byte[] vanillaFullHash = new byte[] { 39, 123, 108, 9, 132, 118, 2, 32, 152, 149, 208, 17, 197, 163, 163, 139 };
            //    byte[] vanillaDolHash = new byte[] { 135, 241, 17, 254, 252, 165, 45, 39, 50, 80, 104, 65, 216, 32, 142, 212 };
            //    byte[] patchedHash = new byte[] { 220, 216, 224, 150, 88, 53, 129, 175, 54, 201, 175, 176, 53, 71, 167, 40 };

            //    var hash = md5.ComputeHash(src);

            //    if (!hash.SequenceEqual(vanillaDolHash) && !hash.SequenceEqual(vanillaFullHash) && !hash.SequenceEqual(patchedHash))
            //        return false;

            //    if (!hash.SequenceEqual(patchedHash))
            //        using (var dest = new MemoryStream())
            //        {
            //            src.Position = 0;
            //            GCILib.BZip2.BinaryPatchUtility.Apply(src, () => new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib/dol.patch"), FileMode.Open, FileAccess.Read, FileShare.Read), dest);
                        
            //            dest.Position = 0;
            //            hash = md5.ComputeHash(dest);

            //            if (!hash.SequenceEqual(patchedHash))
            //                return false;

            //            System.Diagnostics.Debug.WriteLine(string.Join(", ", patchedHash) + " " + hash.SequenceEqual(vanillaDolHash));

            //            resource.SetDOL(dest.ToArray());
            //        }
            //}

            // generate mex files
            using (MEXDOLScrubber dol = new MEXDOLScrubber(resource.GetDOL()))
            {
                var resourceFile = new HSDRawFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib\\resource.dat"));

                // generate mxdt
                MEX_Data data = new MEX_Data();

                // generate meta data
                data.MetaData = new MEX_Meta()
                {
                    NumOfInternalIDs = 33,
                    NumOfExternalIDs = 33,
                    NumOfCSSIcons = 26,
                    NumOfInternalStage = 0,
                    NumOfExternalStage = 285,
                    NumOfSSSIcons = 30,
                    NumOfSSMs = 55,
                    NumOfMusic = 98,
                    NumOfEffects = 51,
                    EnterScene = 2,
                    LastMajor = 45, //?
                    LastMinor = 45,

                    TrophyCount = 0x125,
                    TrophySDOffset = 302,
                };

                // Version
                data.MetaData._s.SetByte(0x00, MEX.VersionMajor);
                data.MetaData._s.SetByte(0x01, MEX.VersionMinor);


                // fighter table
                InstallFighters(dol, data, resourceFile);


                // kirby table
                InstallKirby(dol, data, resourceFile);


                // sound table
                InstallSounds(dol, data, resourceFile);


                // item table
                InstallItems(dol, data, resourceFile);



                // effect table
                data.EffectTable = new MEX_EffectData();
                data.EffectTable.EffectFiles = new HSDArrayAccessor<MEX_EffectFiles>()
                {
                    _s = new HSDFixedLengthPointerArrayAccessor<HSD_String>()
                    {
                        Array = dol.ReadStringTable(EffectStringOffset, data.MetaData.NumOfEffects * 3)
                    }._s
                };
                // note: this really isn't needed here because saving will regenerate anyway
                data.EffectTable.RuntimeUnk1 = new HSDAccessor() { _s = new HSDStruct(0x60) };
                data.EffectTable.RuntimeUnk3 = new HSDAccessor() { _s = new HSDStruct(0xCC) };
                data.EffectTable.RuntimeTexGrNum = new HSDAccessor() { _s = new HSDStruct(0xCC) };
                data.EffectTable.RuntimeTexGrData = new HSDAccessor() { _s = new HSDStruct(0xCC) };
                data.EffectTable.RuntimeUnk4 = new HSDAccessor() { _s = new HSDStruct(0xCC) };
                data.EffectTable.RuntimePtclLast = new HSDAccessor() { _s = new HSDStruct(0xCC) };
                data.EffectTable.RuntimePtclData = new HSDAccessor() { _s = new HSDStruct(0xCC) };
                data.EffectTable.RuntimeLookup = new HSDAccessor() { _s = new HSDStruct(0xCC) };

                // stage table
                InstallStages(dol, data, resourceFile);


                // scene table
                InstallScenes(dol, data, resourceFile);


                // misc table
                InstallMisc(dol, data, resourceFile);


                // generate sss and css symbols
                GetIconFromDOL(dol, data);
                var cssFile = new HSDRawFile(resource.GetFileData("MnSlChr.usd"));
                var sssFile = new HSDRawFile(resource.GetFileData("MnSlMap.usd"));
                var ifallFile = new HSDRawFile(resource.GetFileData("IfAll.usd"));


                // mexSelectChr
                var mexMenuSymbol = GenerateMexSelectChrSymbol(cssFile.Roots[0].Data as SBM_SelectChrDataTable, data.MenuTable.CSSIconData.Icons);
                cssFile.Roots.RemoveAll(e => e.Name == "mexSelectChr");
                cssFile.Roots.Add(new HSDRootNode() { Name = "mexSelectChr", Data = mexMenuSymbol });


                // mexMapData
                var mexMapSymbol = LoadIconDataFromVanilla(sssFile.Roots[0].Data as SBM_MnSelectStageDataTable);
                sssFile.Roots.RemoveAll(e => e.Name == "mexMapData");
                sssFile.Roots.Add(new HSDRootNode() { Name = "mexMapData", Data = mexMapSymbol });


                // ifall data
                // load this from resources; don't generate
                if(ifallFile["bgm"] == null)
                ifallFile.Roots.Add(new HSDRootNode()
                {
                    Name = "bgm",
                    Data = resourceFile["bgm"].Data
                });
                if (ifallFile["Eblm_matanim_joint"] == null)
                    ifallFile.Roots.Add(new HSDRootNode()
                {
                    Name = "Eblm_matanim_joint",
                    Data = resourceFile["Eblm_matanim_joint"].Data
                });
                if (ifallFile["Stc_icns"] == null)
                    ifallFile.Roots.Add(new HSDRootNode()
                {
                    Name = "Stc_icns",
                    Data = resourceFile["Stc_icns"].Data
                });




#if DEBUG
                /*var mexfile2 = new HSDRawFile();
                mexfile2.Roots.Add(new HSDRootNode() { Name = "mexData", Data = data });
                mexfile2.Save("test_Mxdt.dat");
                cssFile.Save("test_CSS.dat");
                sssFile.Save("test_SSS.dat");
                ifallFile.Save("test_IfAll.dat");
                return false;*/
#endif

                // add files to resource
                resource.AddFile("codes.gct", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib\\codes.gct"));

                // null ssm file
                resource.AddFile("audio/us/null.ssm", resource.GetFileData("audio/us/end.ssm"));

                using (MemoryStream dat = new MemoryStream())
                {
                    ifallFile.Save(dat);
                    resource.AddFile("IfAll.usd", dat.ToArray());
                }
                using (MemoryStream dat = new MemoryStream())
                {
                    cssFile.Save(dat);
                    resource.AddFile("MnSlChr.usd", dat.ToArray());
                }
                using (MemoryStream dat = new MemoryStream())
                {
                    sssFile.Save(dat);
                    resource.AddFile("MnSlMap.usd", dat.ToArray());
                }
                using (MemoryStream dat = new MemoryStream())
                {
                    var mexfile = new HSDRawFile();
                    mexfile.Roots.Add(new HSDRootNode() { Name = "mexData", Data = data });
                    mexfile.Save(dat);
                    resource.AddFile("MxDt.dat", dat.ToArray());
                }
            }



            // success
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        private static void ExtractDataFromResource(HSDRawFile resourceFile, HSDAccessor acc)
        {
            foreach (var p in acc.GetType().GetProperties())
            {
                var sym = resourceFile[p.Name];
                if (sym != null)
                {
                    var i = Activator.CreateInstance(p.PropertyType);
                    ((HSDAccessor)i)._s = sym.Data._s;
                    p.SetValue(acc, i);
                }
            }
        }

    }
}
