using HSDRaw.Common;
using HSDRaw.Common.Animation;
using HSDRaw.MEX.Menus;
using System;
using System.ComponentModel;

namespace mexTool.Core
{
    public class MEXFighterIcon : MEXCommonIcon
    {
        /// <summary>
        /// 
        /// </summary>
        public MEX_CSSIcon Icon;

        public HSD_MatAnimJoint MaterialAnimation;


        [Category("0 - General"), DisplayName("Fighter"), Description(""), TypeConverter(typeof(FighterConverter))]
        public MEXFighter Fighter { get; set; }

        [Category("0 - General"), DisplayName("Sound Effect ID"), Description("Sound to play when fighter is selected")]
        public int SoundEffectID { get => Icon.SFXID; set => Icon.SFXID = value; }



        [Category("2 - End Location"), DisplayName("X"), Description("")]
        public override float X
        {
            get => GetTrackValue(JointTrackType.HSD_A_J_TRAX) - Width / 2;
            set
            {
                CollisionX += ((value + Width / 2) - _joint.TX);
                _joint.TX = value + Width / 2;
            }
        }

        [Category("2 - End Location"), DisplayName("Y"), Description("")]
        public override float Y
        {
            get => GetTrackValue(JointTrackType.HSD_A_J_TRAY) - Height / 2;
            set
            {
                CollisionY += (value + Height / 2) - _joint.TY;
                _joint.TY = value + Height / 2;
            }
        }


        [Category("2 - End Location"), DisplayName("Width"), Description("")]
        public override float Width
        {
            get => GetTrackValue(JointTrackType.HSD_A_J_SCAX) * IconModel.Width;
            set
            {
                _joint.SX = value / IconModel.Width;
            }
        }

        [Category("2 - End Location"), DisplayName("Height"), Description("")]
        public override float Height
        {
            get => GetTrackValue(JointTrackType.HSD_A_J_SCAY) * IconModel.Height;
            set
            {
                //CollisionHeight *= 1 + (value / IconModel.Height) - _joint.SY;
                _joint.SY = value / IconModel.Height;
            }
        }


        [Category("0 - General"), Description("X Offset from Joint")]
        public float CollisionX { get => Icon.X1 - _joint.TX; set { var w = CollisionWidth; Icon.X1 = value + _joint.TX; CollisionWidth = w; } }

        [Category("0 - General"), Description("Y Offset from Joint")]
        public float CollisionY { get => Icon.Y1 - _joint.TY + CollisionHeight; set { var h = CollisionHeight; Icon.Y1 = value + _joint.TY - h; CollisionHeight = h; } }

        [Category("0 - General"), Description("Width of Collision")]
        public float CollisionWidth { get => Icon.X2 - Icon.X1; set => Icon.X2 = Icon.X1 + Math.Abs(value); }

        [Category("0 - General"), Description("Height of Collision")]
        public float CollisionHeight { get => Icon.Y2 - Icon.Y1; set => Icon.Y2 = Icon.Y1 + Math.Abs(value); }


        public override string ToString()
        {
            return Fighter?.ToString();
        }
    }
}
