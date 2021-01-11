using HSDRaw.Common;
using HSDRaw.Common.Animation;
using HSDRaw.Tools;
using mexTool.GUI.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace mexTool.Core
{
    public class MEXCommonIcon : ISelectScreenIcon
    {
        public RectangleF IconModel;

        public HSD_JOBJ _joint;

        [Browsable(false)]
        public HSD_TOBJ Image
        {
            get => _image;
            set
            {
                _image = value;
                RefreshRenderImage();
            }
        }
        private HSD_TOBJ _image;
        private Bitmap _renderImage = null;

        [Category("1 - Animation"), DisplayName("Start Frame"), Description("")]
        public float StartFrame { get; set; } = 0;

        [Category("1 - Animation"), DisplayName("End Frame"), Description("")]
        public float EndFrame { get; set; } = 10;


        [Category("2 - End Location"), DisplayName("X"), Description("")]
        public virtual float X { get => GetTrackValue(JointTrackType.HSD_A_J_TRAX) - Width / 2; set => _joint.TX = value + Width / 2; }

        [Category("2 - End Location"), DisplayName("Y"), Description("")]
        public virtual float Y { get => GetTrackValue(JointTrackType.HSD_A_J_TRAY) - Height / 2; set => _joint.TY = value + Height / 2; }

        [Category("2 - End Location"), DisplayName("Z"), Description("")]
        public float Z { get => GetTrackValue(JointTrackType.HSD_A_J_TRAZ); set => _joint.TZ = value; }


        [Browsable(false), Category("2 - End Location"), DisplayName("Rotation X"), Description("")]
        public float RX { get => GetTrackValue(JointTrackType.HSD_A_J_ROTX); set => _joint.RX = value; }

        [Browsable(false), Category("2 - End Location"), DisplayName("Rotation Y"), Description("")]
        public float RY { get => GetTrackValue(JointTrackType.HSD_A_J_ROTY); set => _joint.RY = value; }

        [Category("2 - End Location"), DisplayName("Rotation"), Description("")]
        public float RZ { get => GetTrackValue(JointTrackType.HSD_A_J_ROTZ); set => _joint.RZ = value; }


        [Category("2 - End Location"), DisplayName("Width"), Description("")]
        public virtual float Width { get => GetTrackValue(JointTrackType.HSD_A_J_SCAX) * IconModel.Width; set => _joint.SX = value / IconModel.Width; }

        [Category("2 - End Location"), DisplayName("Height"), Description("")]
        public virtual float Height { get => GetTrackValue(JointTrackType.HSD_A_J_SCAY) * IconModel.Height; set => _joint.SY = value / IconModel.Height; }



        [Category("3 - Start Location"), DisplayName("X"), Description("")]
        public float StartX { get => _startX - StartWidth / 2; set => _startX = value + StartWidth / 2; }
        private float _startX;

        [Category("3 - Start Location"), DisplayName("Y"), Description("")]
        public float StartY { get => _startY - StartHeight / 2; set => _startY = value + StartHeight / 2; }
        private float _startY;

        [Category("3 - Start Location"), DisplayName("Z"), Description("")]
        public float StartZ { get => _startZ; set => _startZ = value; }
        private float _startZ;


        [Browsable(false), Category("3 - Start Location"), DisplayName("Rotation X"), Description("")]
        public float StartRX { get; set; }

        [Browsable(false), Category("3 - Start Location"), DisplayName("Rotation Y"), Description("")]
        public float StartRY { get; set; }

        [Category("3 - Start Location"), DisplayName("Rotation"), Description("")]
        public float StartRZ { get; set; }


        [Category("3 - Start Location"), DisplayName("Width"), Description("")]
        public float StartWidth { get => _startSX * IconModel.Width; set => _startSX = value / IconModel.Width; }
        private float _startSX;

        [Category("3 - Start Location"), DisplayName("Height"), Description("")]
        public float StartHeight { get => _startSY * IconModel.Height; set => _startSY = value / IconModel.Height; }
        private float _startSY;


        [Browsable(false)]
        public float Frame { get; set; } = -1;

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<JointTrackType, FOBJ_Player> _animPlayer = new Dictionary<JointTrackType, FOBJ_Player>();

        /// <summary>
        /// 
        /// </summary>
        public void RefreshRenderImage()
        {
            if (_renderImage != null)
                _renderImage.Dispose();

            if (_image != null)
                _renderImage = GraphicExtensions.TOBJToBitmap(_image);
            else
                _renderImage = null;
        }

        /// <summary>
        /// 
        /// </summary>
        public void GenerateAnimation()
        {
            FromAnimJoint(ToAnimJoint());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public HSD_AnimJoint ToAnimJoint()
        {
            HSD_AnimJoint joint = new HSD_AnimJoint();

            joint.AOBJ = new HSD_AOBJ();

            joint.AOBJ.EndFrame = 1600;
            joint.AOBJ.Flags = AOBJ_Flags.FIRST_PLAY;

            // generate descriptors

            GenerateFOBJ(joint.AOBJ, JointTrackType.HSD_A_J_TRAX, _startX, _joint.TX);
            GenerateFOBJ(joint.AOBJ, JointTrackType.HSD_A_J_TRAY, _startY, _joint.TY);
            GenerateFOBJ(joint.AOBJ, JointTrackType.HSD_A_J_TRAZ, _startZ, _joint.TZ);

            GenerateFOBJ(joint.AOBJ, JointTrackType.HSD_A_J_ROTX, StartRX, _joint.RX);
            GenerateFOBJ(joint.AOBJ, JointTrackType.HSD_A_J_ROTY, StartRY, _joint.RY);
            GenerateFOBJ(joint.AOBJ, JointTrackType.HSD_A_J_ROTZ, StartRZ, _joint.RZ);

            GenerateFOBJ(joint.AOBJ, JointTrackType.HSD_A_J_SCAX, _startSX, _joint.SX);
            GenerateFOBJ(joint.AOBJ, JointTrackType.HSD_A_J_SCAY, _startSY, _joint.SY);

            if (joint.AOBJ.FObjDesc == null)
                joint.AOBJ = null;

            return joint;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void GenerateFOBJ(HSD_AOBJ aobj, JointTrackType type, float startValue, float endValue)
        {
            // no need for animation
            if (startValue == endValue || StartFrame == EndFrame)
                return;

            List<FOBJKey> keys = new List<FOBJKey>();

            if (StartFrame != 0)
                keys.Add(new FOBJKey() { Frame = 0, Value = startValue, InterpolationType = GXInterpolationType.HSD_A_OP_CON });

            keys.Add(new FOBJKey() { Frame = StartFrame, Value = startValue, InterpolationType = GXInterpolationType.HSD_A_OP_LIN });
            keys.Add(new FOBJKey() { Frame = EndFrame, Value = endValue, InterpolationType = GXInterpolationType.HSD_A_OP_CON });

            if (EndFrame != 1600)
                keys.Add(new FOBJKey() { Frame = 1600, Value = endValue, InterpolationType = GXInterpolationType.HSD_A_OP_CON });

            HSD_FOBJDesc fobj = new HSD_FOBJDesc();
            fobj.SetKeys(keys, (byte)type);

            if (aobj.FObjDesc == null)
                aobj.FObjDesc = fobj;
            else
                aobj.FObjDesc.Add(fobj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void FromAnimJoint(HSD_AnimJoint value)
        {
            _animPlayer.Clear();

            // default values
            _startX = _joint.TX;
            _startY = _joint.TY;
            _startZ = _joint.TZ;
            StartRX = _joint.RX;
            StartRY = _joint.RY;
            StartRZ = _joint.RZ;
            _startSX = _joint.SX;
            _startSY = _joint.SY;

            // load starting values from track
            if (value != null && value.AOBJ != null)
            {
                foreach (var fobj in value.AOBJ.FObjDesc.List)
                {
                    // generate animation player
                    FOBJ_Player player = new FOBJ_Player(fobj);
                    _animPlayer.Add(player.JointTrackType, player);

                    // find first frame that begin interpolating
                    var index = player.Keys.FindIndex(e => e.InterpolationType != GXInterpolationType.HSD_A_OP_CON);

                    // if a frame follows it assume the interpolation ends there
                    if (index != -1 && index + 1 < player.Keys.Count)
                    {
                        StartFrame = player.Keys[index].Frame;
                        EndFrame = player.Keys[index + 1].Frame;
                    }

                    // extract animation info
                    switch (fobj.JointTrackType)
                    {
                        case JointTrackType.HSD_A_J_TRAX: _startX = player.GetValue(0); break;
                        case JointTrackType.HSD_A_J_TRAY: _startY = player.GetValue(0); break;
                        case JointTrackType.HSD_A_J_TRAZ: _startZ = player.GetValue(0); break;
                        case JointTrackType.HSD_A_J_ROTX: StartRX = player.GetValue(0); break;
                        case JointTrackType.HSD_A_J_ROTY: StartRY = player.GetValue(0); break;
                        case JointTrackType.HSD_A_J_ROTZ: StartRZ = player.GetValue(0); break;
                        case JointTrackType.HSD_A_J_SCAX: _startSX = player.GetValue(0); break;
                        case JointTrackType.HSD_A_J_SCAY: _startSY = player.GetValue(0); break;
                        case JointTrackType.HSD_A_J_SCAZ: break; // don't scale z lol
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected float GetTrackValue(JointTrackType type)
        {
            if (_animPlayer.ContainsKey(type) && Frame != -1)
                return _animPlayer[type].GetValue(Frame);

            if (_joint == null)
                return 0;

            switch (type)
            {
                case JointTrackType.HSD_A_J_TRAX: return _joint.TX;
                case JointTrackType.HSD_A_J_TRAY: return _joint.TY;
                case JointTrackType.HSD_A_J_TRAZ: return _joint.TZ;
                case JointTrackType.HSD_A_J_ROTX: return _joint.RX;
                case JointTrackType.HSD_A_J_ROTY: return _joint.RY;
                case JointTrackType.HSD_A_J_ROTZ: return _joint.RZ;
                case JointTrackType.HSD_A_J_SCAX: return _joint.SX;
                case JointTrackType.HSD_A_J_SCAY: return _joint.SY;
                case JointTrackType.HSD_A_J_SCAZ: return _joint.SZ;
            }

            return 0;
        }

        /// <summary>
        /// Relative move the icon
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void Move(PositionMode mode, float x, float y, float width, float height)
        {
            if (mode == PositionMode.Start)
            {
                StartX += x;
                StartY += y;
                StartWidth += width;
                StartHeight += height;
            }
            if (mode == PositionMode.End)
            {
                X += x;
                Y += y;
                Width += width;
                Height += height;
                if (this is MEXStageIcon stageIcon)
                {
                    stageIcon.CollisionWidth += width / 2;
                    stageIcon.CollisionHeight += height / 2;
                }
                if (this is MEXFighterIcon fighterIcon)
                {
                    fighterIcon.CollisionX -= width / 2;
                    fighterIcon.CollisionY -= height / 2;
                    fighterIcon.CollisionWidth += width;
                    fighterIcon.CollisionHeight += height;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual Image GetImage()
        {
            return _renderImage;
        }
    }
}
