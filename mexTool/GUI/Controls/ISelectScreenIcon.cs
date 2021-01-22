using System.Drawing;

namespace mexTool.GUI.Controls
{
    public interface ISelectScreenIcon
    {
        float X { get; set; }

        float Y { get; set; }

        float Z { get; set; }

        float RX { get; set; }

        float RY { get; set; }

        float RZ { get; set; }

        float Width { get; set; }

        float Height { get; set; }

        float StartX { get; set; }

        float StartY { get; set; }

        float StartZ { get; set; }

        float StartRX { get; set; }

        float StartRY { get; set; }

        float StartRZ { get; set; }

        float StartWidth { get; set; }

        float StartHeight { get; set; }

        float Frame { get; set; }

        void GenerateAnimation();

        void Move(PositionMode mode, float x, float y, float w, float h);

        Image GetImage();
    }

    public class IconUndoState
    {
        public ISelectScreenIcon Icon { get; internal set; }

        float X { get; set; }

        float Y { get; set; }

        float Z { get; set; }

        float RX { get; set; }

        float RY { get; set; }

        float RZ { get; set; }

        float Width { get; set; }

        float Height { get; set; }

        float StartX { get; set; }

        float StartY { get; set; }

        float StartZ { get; set; }

        float StartRX { get; set; }

        float StartRY { get; set; }

        float StartRZ { get; set; }

        float StartWidth { get; set; }

        float StartHeight { get; set; }

        float CollisionWidth { get; set; }

        float CollisionHeight { get; set; }

        float CollisionX { get; set; }

        float CollisionY { get; set; }

        public IconUndoState(ISelectScreenIcon icon)
        {
            Icon = icon;
            X = icon.X;
            Y = icon.Y;
            Z = icon.Z;
            RX = icon.RX;
            RY = icon.RY;
            RZ = icon.RZ;
            Width = icon.Width;
            Height = icon.Height;
            StartX = icon.StartX;
            StartY = icon.StartY;
            StartZ = icon.StartZ;
            StartRX = icon.StartRX;
            StartRY = icon.StartRY;
            StartRZ = icon.StartRZ;
            StartWidth = icon.StartWidth;
            StartHeight = icon.StartHeight;

            if(icon is Core.MEXFighterIcon fighter)
            {
                CollisionWidth = fighter.CollisionWidth;
                CollisionHeight = fighter.CollisionHeight;
                CollisionX = fighter.CollisionX;
                CollisionY = fighter.CollisionY;
            }

            if (icon is Core.MEXStageIcon stage)
            {
                CollisionWidth = stage.CollisionWidth;
                CollisionHeight = stage.CollisionHeight;
            }
        }

        public void Restore()
        {
            // width and height must be set first
            Icon.Width = Width;
            Icon.Height = Height;
            Icon.X = X;
            Icon.Y = Y;
            Icon.Z = Z;
            Icon.RX = RX;
            Icon.RY = RY;
            Icon.RZ = RZ;

            Icon.StartWidth = StartWidth;
            Icon.StartHeight = StartHeight;
            Icon.StartX = StartX;
            Icon.StartY = StartY;
            Icon.StartZ = StartZ;
            Icon.StartRX = StartRX;
            Icon.StartRY = StartRY;
            Icon.StartRZ = StartRZ;

            if (Icon is Core.MEXFighterIcon fighter)
            {
                fighter.CollisionHeight = CollisionHeight;
                fighter.CollisionWidth = CollisionWidth;
                fighter.CollisionX = CollisionX;
                fighter.CollisionY = CollisionY;
            }

            if (Icon is Core.MEXStageIcon stage)
            {
                stage.CollisionHeight = CollisionHeight;
                stage.CollisionWidth = CollisionWidth;
            }
        }
    }

}
