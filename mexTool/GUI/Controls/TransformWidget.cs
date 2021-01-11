using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mexTool.GUI.Controls
{
    public enum TransformMode
    {
        NONE,
        DRAG,
        TOPLEFT,
        TOPMIDDLE,
        TOPRIGHT,
        MIDDLELEFT,
        MIDDLERIGHT,
        BOTTOMLEFT,
        BOTTOMMIDDLE,
        BOTTOMRIGHT,
        ROTATE
    }

    public class TransformWidget
    {
        public TransformMode Mode { get; internal set; } = TransformMode.NONE;

        public float X;
        public float Y;
        public float Width;
        public float Height;

        public float HandleSize = 6;
        private float HandleOffset { get => HandleSize / 2; }

        public RectangleF TopLeftHandle { get => new RectangleF(X - HandleOffset, Y - HandleOffset, HandleSize, HandleSize); }
        public RectangleF TopMiddleHandle { get => new RectangleF(X + Width / 2 - HandleOffset, Y - HandleOffset, HandleSize, HandleSize); }
        public RectangleF TopRightHandle { get => new RectangleF(X + Width - HandleOffset, Y - HandleOffset, HandleSize, HandleSize); }

        public RectangleF MiddleLeft { get => new RectangleF(X - HandleOffset, Y + Height / 2 - HandleOffset, HandleSize, HandleSize); }

        public RectangleF MiddleRight { get => new RectangleF(X + Width - HandleOffset, Y + Height / 2 - HandleOffset, HandleSize, HandleSize); }

        public RectangleF BottomLeftHandle { get => new RectangleF(X - HandleOffset, Y + Height - HandleOffset, HandleSize, HandleSize); }
        public RectangleF BottomMiddleHandle { get => new RectangleF(X + Width / 2 - HandleOffset, Y + Height - HandleOffset, HandleSize, HandleSize); }
        public RectangleF BottomRightHandle { get => new RectangleF(X + Width - HandleOffset, Y + Height - HandleOffset, HandleSize, HandleSize); }


        private static Pen DottedPen = new Pen(Color.White);
        private static Pen HandlePen = new Pen(Color.Black);
        private static Brush HandleFill = new SolidBrush(Color.White);

        public TransformWidget()
        {
            DottedPen.DashPattern = new float[] { 5, 5, 5, 5 };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="area"></param>
        public void SetBound(RectangleF area)
        {
            Mode = TransformMode.NONE;
            X = area.X;
            Y = area.Y;
            Width = area.Width;
            Height = area.Height;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public bool CheckHandle(PointF mouse)
        {
            Mode = TransformMode.NONE;

            if (new RectangleF(X, Y, Width, Height).Contains(mouse)) 
                Mode = TransformMode.DRAG;
            else
            {
                if (new RectangleF(X, Y - 5, Width, Height + 10).Contains(mouse))
                    Mode = TransformMode.ROTATE;
            }

            if (TopLeftHandle.Contains(mouse)) Mode = TransformMode.TOPLEFT;
            if (TopMiddleHandle.Contains(mouse)) Mode = TransformMode.TOPMIDDLE;
            if (TopRightHandle.Contains(mouse)) Mode = TransformMode.TOPRIGHT;
            if (MiddleLeft.Contains(mouse)) Mode = TransformMode.MIDDLELEFT;
            if (MiddleRight.Contains(mouse)) Mode = TransformMode.MIDDLERIGHT;
            if (BottomLeftHandle.Contains(mouse)) Mode = TransformMode.BOTTOMLEFT;
            if (BottomMiddleHandle.Contains(mouse)) Mode = TransformMode.BOTTOMMIDDLE;
            if (BottomRightHandle.Contains(mouse)) Mode = TransformMode.BOTTOMRIGHT;


            return Mode != TransformMode.NONE;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Draw(Graphics g)
        {
            g.DrawLine(DottedPen, X, Y, X + Width, Y);
            g.DrawLine(DottedPen, X, Y, X, Y + Height);
            g.DrawLine(DottedPen, X + Width, Y, X + Width, Y + Height);
            g.DrawLine(DottedPen, X, Y + Height, X + Width, Y + Height);

            g.FillRectangle(HandleFill, TopLeftHandle);
            g.DrawRectangle(HandlePen, TopLeftHandle);

            g.FillRectangle(HandleFill, TopMiddleHandle);
            g.DrawRectangle(HandlePen, TopMiddleHandle);

            g.FillRectangle(HandleFill, TopRightHandle);
            g.DrawRectangle(HandlePen, TopRightHandle);

            g.FillRectangle(HandleFill, MiddleLeft);
            g.DrawRectangle(HandlePen, MiddleLeft);

            g.FillRectangle(HandleFill, MiddleRight);
            g.DrawRectangle(HandlePen, MiddleRight);

            g.FillRectangle(HandleFill, BottomLeftHandle);
            g.DrawRectangle(HandlePen, BottomLeftHandle);

            g.FillRectangle(HandleFill, BottomMiddleHandle);
            g.DrawRectangle(HandlePen, BottomMiddleHandle);

            g.FillRectangle(HandleFill, BottomRightHandle);
            g.DrawRectangle(HandlePen, BottomRightHandle);
        }
    }

}
