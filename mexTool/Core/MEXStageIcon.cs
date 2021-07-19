using HSDRaw.Common;
using HSDRaw.Common.Animation;
using HSDRaw.MEX.Menus;
using System.ComponentModel;

namespace mexTool.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class MEXStageIcon : MEXCommonIcon
    {
        public MEX_StageIconData _icon;

        public HSD_TOBJ _previewText;


        [Category("0 - General"), DisplayName("Stage"), Description("The stage file this icon will enter"), TypeConverter(typeof(StageConverter))]
        public MEXStage Stage { get; set; }


        [Category("0 - General"), DisplayName("Random Enabled"), Description("Determines if this stage can be selected by random by default.")]
        public bool RandomEnabled { get; set; }


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
                _joint.SY = value / IconModel.Height;
            }
        }


        [Category("0 - General"), Description("Width of Collision")]
        public float CollisionWidth { get => _icon.CursorWidth; set => _icon.CursorWidth = value; }

        [Category("0 - General"), Description("Height of Collision")]
        public float CollisionHeight { get => _icon.CursorHeight; set => _icon.CursorHeight = value; }

        [Category("0 - General"), Description("Width of Outline")]
        public float OutlineWidth { get => _icon.OutlineWidth; set => _icon.OutlineWidth = value; }

        [Category("0 - General"), Description("Height of Outline")]
        public float OutlineHeight { get => _icon.OutlineHeight; set => _icon.OutlineHeight = value; }

        [Category("0 - General"), Description("Indexes the 3d model to display for the stage")]
        public byte PreviewID { get => _icon.PreviewModelID; set => _icon.PreviewModelID = value; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Stage?.ToString();
        }
    }
}
