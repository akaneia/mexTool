using mexTool.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace mexTool.GUI.Controls
{
    public enum PositionMode
    {
        End,
        Start
    }

    public partial class SelectScreenRenderer : UserControl
    {

        public int Frame { get; set; } = 0;

        public int MaxFrame { get; set; } = 1600;

        public object DataSource { get; set; }

        public IEnumerable<ISelectScreenIcon> Icons
        {
            get
            {
                if (DataSource is IEnumerable items)
                    foreach (var item in items)
                        if (item is ISelectScreenIcon icon)
                            yield return icon;
            }
        }

        public float Zoom { get; set; } = 6;


        public Size ScreenSize { get; set; } = new Size(64, 48);

        private TransformMode Mode;

        private TransformMode PendingMode;


        public PositionMode PositionMode = PositionMode.End;

        private bool MovingCamera = false;
        private bool Selecting = false;
        private PointF StartSelect { get; set; }

        private RectangleF SelectArea { get => new RectangleF(Math.Min(StartSelect.X, MouseLocation.X), Math.Min(StartSelect.Y, MouseLocation.Y), Math.Abs(MouseLocation.X - StartSelect.X), Math.Abs(MouseLocation.Y - StartSelect.Y)); }


        private PointF ScreenOffset { get; set; } = new PointF(0, 0);
        private PointF PrevMouseLocation { get; set; }
        private PointF MouseLocation
        {
            get; set;
        }
        private PointF GridSnapLocation(PointF location)
        {
            var ox = drawPanel.Width / 2;
            var oy = drawPanel.Height / 2;

            return new PointF(
                (float)Math.Round((location.X) / GridWidth) * GridWidth + ox % GridWidth,
                (float)Math.Round((location.Y) / GridHeight) * GridHeight + oy % GridWidth);

        }

        public bool EnableSnap { get; set; } = true;

        public bool GridEnabled { get; set; } = false;

        public int GridWidth { get; set; } = 40;

        public int GridHeight { get; set; } = 40;

        public IEnumerable<ISelectScreenIcon> SelectedIcons
        {
            get
            {
                foreach (var v in _selectedIcons)
                    yield return v;
            }
        }

        private List<ISelectScreenIcon> _selectedIcons = new List<ISelectScreenIcon>();


        /// <summary>
        /// 
        /// </summary>
        public void SelectItems(IEnumerable<ISelectScreenIcon> items)
        {
            Do();

            _selectedIcons.Clear();

            foreach (var v in items)
                _selectedIcons.Add(v);

            OnSelectedIconChanged(new PropertyValueChangedEventArgs(null, null));

            drawPanel.Invalidate();
        }


        public event EventHandler ItemMoved;
        protected virtual void OnItemMoved(EventArgs e)
        {
            EventHandler handler = ItemMoved;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler SelectedIconChanged;
        protected virtual void OnSelectedIconChanged(PropertyValueChangedEventArgs e)
        {
            EventHandler handler = SelectedIconChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        public event EventHandler AnimationEnded;
        protected virtual void OnAnimationEnd(EventArgs e)
        {
            EventHandler handler = AnimationEnded;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private TransformWidget _transformTool = new TransformWidget();


        public Color GridColor
        {
            get => gridColorItem.BackColor;
            set
            {
                gridColorItem.BackColor = value;
                gridColorItem.ForeColor = GraphicExtensions.ContrastColor(value); 
                drawPanel.Invalidate();
            }
        }

        public Color DrawBackColor
        {
            get => drawPanel.BackColor; set
            {
                backColorItem.BackColor = value;
                backColorItem.ForeColor = GraphicExtensions.ContrastColor(value);
                drawPanel.BackColor = value;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public SelectScreenRenderer()
        {
            InitializeComponent();

            GridColor = Color.FromArgb(20, 20, 20);
            DrawBackColor = Color.Black;

            drawPanel.KeyDown += (sender, args) =>
            {
                if(args.Control && args.KeyCode == Keys.Z)
                {
                    Undo();
                }
                if (args.Control && args.KeyCode == Keys.Y)
                {
                    Redo();
                }
            };

            drawPanel.MouseWheel += (sender, args) =>
            {
                if (Math.Sign(args.Delta) < 0)
                    ZoomOut(null, EventArgs.Empty);
                else
                    ZoomIn(null, EventArgs.Empty);
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public void Play()
        {
            frameTimer.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Pause()
        {
            frameTimer.Stop();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            frameTimer.Stop();
            Frame = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public void RefreshDraw()
        {
            drawPanel.Invalidate();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frameTimer_Tick(object sender, EventArgs e)
        {
            Frame += 1;
            if (Frame >= MaxFrame)
            {
                Frame = 0;
                playButton_Click(playButton, EventArgs.Empty);
                OnAnimationEnd(new EventArgs());
            }
            drawPanel.Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        private RectangleF GetIconBounds(ISelectScreenIcon icon, bool collision = false)
        {
            // get basic icons bounds
            var bound = new RectangleF(
                drawPanel.Width / 2 + icon.X * Zoom - icon.Z,
                drawPanel.Height / 2 - (icon.Y * Zoom - icon.Z) - (icon.Height * Zoom + icon.Z * 2),
                icon.Width * Zoom + icon.Z * 2,
                icon.Height * Zoom + icon.Z * 2);

            // get start bounds if mode enabled
            if (PositionMode == PositionMode.Start && !frameTimer.Enabled)
            {
                bound = new RectangleF(
                drawPanel.Width / 2 + icon.StartX * Zoom - icon.StartZ,
                drawPanel.Height / 2 - (icon.StartY * Zoom - icon.StartZ) - (icon.StartHeight * Zoom + icon.StartZ * 2),
                icon.StartWidth * Zoom + icon.StartZ * 2,
                icon.StartHeight * Zoom + icon.StartZ * 2);
            }

            // get collision bounds
            if (collision)
            {
                // draw collision for fighter icons
                if (icon is MEXFighterIcon fightIcon)
                {
                    return new RectangleF(
                            bound.X + bound.Width / 2 + fightIcon.CollisionX * Zoom,
                            bound.Y + bound.Height / 2 - fightIcon.CollisionY * Zoom,
                            fightIcon.CollisionWidth * Zoom,
                            fightIcon.CollisionHeight * Zoom);
                }

                // draw collision for stage icons
                if (icon is MEXStageIcon stageIcon)
                {
                    return new RectangleF(
                            bound.X + bound.Width / 2 - stageIcon.CollisionWidth * Zoom,
                            bound.Y + bound.Height / 2 - stageIcon.CollisionHeight * Zoom,
                            stageIcon.CollisionWidth * 2 * Zoom,
                            stageIcon.CollisionHeight * 2 * Zoom);
                }
            }

            // minimum 5 pixel bounds
            float minBound = 5;

            if (Math.Abs(bound.Width) < minBound)
                bound.Width = minBound * Math.Sign(bound.Width);

            if (Math.Abs(bound.Height) < minBound)
                bound.Height = minBound * Math.Sign(bound.Height);

            //
            return bound;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawPanel_Paint(object sender, PaintEventArgs e)
        {
            // store original
            e.Graphics.TranslateTransform(ScreenOffset.X, ScreenOffset.Y);


            // prepare drawing
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;


            // draw grid
            if (GridEnabled)
                PaintGrid(e.Graphics);


            // paint middle lines
            using (var pen = new Pen(GraphicExtensions.ContrastColor(DrawBackColor)))
            {
                e.Graphics.DrawLine(pen, 0, drawPanel.Height / 2, drawPanel.Width, drawPanel.Height / 2);
                e.Graphics.DrawLine(pen, drawPanel.Width / 2, 0, drawPanel.Width / 2, drawPanel.Height);
            }


            // draw icons
            foreach (var icon in Icons)
            {
                // animate if needed
                if (frameTimer.Enabled)
                    icon.Frame = Frame;
                else
                    icon.Frame = -1;

                // get icon model bounds
                var bound = GetIconBounds(icon);

                // draw image
                e.Graphics.DrawImage(icon.GetImage(), bound, PositionMode == PositionMode.Start && !frameTimer.Enabled ? icon.StartRZ : icon.RZ);

                // render collisions
                if (!frameTimer.Enabled)
                    e.Graphics.DrawFilledRectangle(GetIconBounds(icon, true), Color.FromArgb(90, Color.Orange), GraphicExtensions.ContrastColor(DrawBackColor));

            }


            // things to draw when not animating
            if (!frameTimer.Enabled)
            {
                // draw selected icons
                foreach (var icon in _selectedIcons)
                {
                    using (var brush = new SolidBrush(Color.FromArgb(50, 255, 255, 255)))
                    {
                        var bound = GetIconBounds(icon);

                        e.Graphics.DrawImage(icon.GetImage(), bound, PositionMode == PositionMode.Start ? icon.StartRZ : icon.RZ);

                        _transformTool.SetBound(bound);
                        _transformTool.Draw(e.Graphics);

                        // render animation movement preview
                        if (PositionMode == PositionMode.Start)
                        {
                            PositionMode = PositionMode.End;
                            var endBound = GetIconBounds(icon);
                            PositionMode = PositionMode.Start;

                            e.Graphics.FillRectangle(brush, endBound);

                            using (Pen p = new Pen(Color.White, 2))
                            using (AdjustableArrowCap bigArrow = new AdjustableArrowCap(5, 5))
                            {
                                p.CustomEndCap = bigArrow;

                                e.Graphics.DrawLine(p,
                                    bound.X + bound.Width / 2,
                                    bound.Y + bound.Height / 2,
                                    endBound.X + endBound.Width / 2,
                                    endBound.Y + endBound.Height / 2);
                            }
                        }
                    }
                }

                // draw selecting outline
                if (Selecting)
                {
                    using (var pen = new Pen(Color.White))
                    {
                        pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        e.Graphics.DrawRectangle(pen, SelectArea);
                    }
                }
            }
            else
            {
                // draw animation info
                using (var pen = new SolidBrush(GraphicExtensions.ContrastColor(DrawBackColor)))
                    e.Graphics.DrawString($"Playing... Frame ({Frame}\\{MaxFrame})", Font, pen, -ScreenOffset.X, -ScreenOffset.Y);
            }


            // draw screen border
            using (var pen = new Pen(GraphicExtensions.ContrastColor(DrawBackColor), 2))
            {
                e.Graphics.DrawRectangle(pen, drawPanel.Width / 2 - ScreenSize.Width * Zoom / 2, drawPanel.Height / 2 - ScreenSize.Height * Zoom / 2, ScreenSize.Width * Zoom, ScreenSize.Height * Zoom);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        private void PaintGrid(Graphics g)
        {
            var ox = drawPanel.Width / 2;
            var oy = drawPanel.Height / 2;

            using (var pen = new Pen(GridColor))
            {
                var xCells = (int)Math.Ceiling(drawPanel.Width / (float)GridWidth);
                var yCells = (int)Math.Ceiling(drawPanel.Height / (float)GridHeight);

                var xCellOffset = -GridWidth + ox % GridWidth;
                var yCellOffset = -GridHeight + oy % GridHeight;

                for (int y = 0; y < yCells; ++y)
                {
                    g.DrawLine(pen, xCellOffset, yCellOffset + y * GridHeight, xCellOffset + xCells * GridWidth, yCellOffset + y * GridHeight);
                }

                for (int x = 0; x < xCells; ++x)
                {
                    g.DrawLine(pen, xCellOffset + x * GridWidth, yCellOffset, xCellOffset + x * GridWidth, yCellOffset + yCells * GridHeight);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private TransformMode TestTransform()
        {
            // tool bounds
            foreach (var SelectedIcon in _selectedIcons)
            {
                _transformTool.SetBound(GetIconBounds(SelectedIcon));

                // transform handles
                if (_transformTool.CheckHandle(MouseLocation))
                {
                    switch (_transformTool.Mode)
                    {
                        case TransformMode.DRAG:
                            Cursor = Cursors.SizeAll;
                            return _transformTool.Mode;
                        case TransformMode.TOPLEFT:
                        case TransformMode.BOTTOMRIGHT:
                            Cursor = Cursors.SizeNWSE;
                            return _transformTool.Mode;
                        case TransformMode.TOPRIGHT:
                        case TransformMode.BOTTOMLEFT:
                            Cursor = Cursors.SizeNESW;
                            return _transformTool.Mode;
                        case TransformMode.TOPMIDDLE:
                        case TransformMode.BOTTOMMIDDLE:
                            Cursor = Cursors.SizeNS;
                            return _transformTool.Mode;
                        case TransformMode.MIDDLELEFT:
                        case TransformMode.MIDDLERIGHT:
                            Cursor = Cursors.SizeWE;
                            return _transformTool.Mode;
                        case TransformMode.ROTATE:
                            Cursor = Cursors.NoMoveHoriz;
                            return _transformTool.Mode;
                    }
                }
            }

            // can select new icon
            foreach (var icon in Icons)
            {
                if (!_selectedIcons.Contains(icon) && GetIconBounds(icon).Contains(MouseLocation))
                {
                    Cursor.Current = Cursors.Hand;
                    return TransformMode.NONE;
                }
            }

            // can do nothing
            return TransformMode.NONE;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (frameTimer.Enabled)
                return;

            // update mouse tracking
            if (PrevMouseLocation == null)
                PrevMouseLocation = drawPanel.PointToClient(MousePosition);

            PrevMouseLocation = MouseLocation;
            var clientMouse = drawPanel.PointToClient(MousePosition);
            MouseLocation = new PointF(clientMouse.X - ScreenOffset.X, clientMouse.Y - ScreenOffset.Y);


            // relative mouse movement
            var deltaMouse = new PointF((MouseLocation.X - PrevMouseLocation.X) / Zoom, (MouseLocation.Y - PrevMouseLocation.Y) / Zoom);


            // move camera
            // preform transform tool checks
            Cursor = Cursors.Default;
            if (MovingCamera)
            {
                ScreenOffset = new PointF(
                    Math.Min(Math.Max(ScreenOffset.X + deltaMouse.X * Zoom, -200), 200), 
                    Math.Min(Math.Max(ScreenOffset.Y + deltaMouse.Y * Zoom, -200), 200)
                    );

                MouseLocation = new PointF(clientMouse.X - ScreenOffset.X, clientMouse.Y - ScreenOffset.Y);
                Cursor = Cursors.SizeAll;
            }
            else
            {
                PendingMode = TestTransform();
            }
            

            // Snapping Logic
            if (_selectedIcons.Count > 0)
            {
                // Grid Snap
                if (GridEnabled)
                {
                    if (FocusedIcon == null)
                        FocusedIcon = _selectedIcons[0];
                    var focusBound = GetIconBounds(FocusedIcon);

                    // snap moue location
                    PointF snap = GridSnapLocation(
                        new PointF(
                            MouseLocation.X,
                            MouseLocation.Y
                        ));

                    if (Mode == TransformMode.DRAG)
                    {
                        // dragging snaps from center
                        snap = GridSnapLocation(
                            new PointF(
                                MouseLocation.X - FocusedIcon.Width / 2 * Zoom,
                                MouseLocation.Y - FocusedIcon.Height / 2 * Zoom
                            ));
                        deltaMouse = new PointF((snap.X - focusBound.X) / Zoom, (snap.Y - focusBound.Y) / Zoom);
                    }
                    else
                    {
                        switch (Mode)
                        {
                            case TransformMode.MIDDLELEFT:
                            case TransformMode.TOPLEFT:
                            case TransformMode.BOTTOMLEFT:
                                deltaMouse.X = (snap.X - focusBound.X) / Zoom;
                                break;
                            case TransformMode.MIDDLERIGHT:
                            case TransformMode.TOPRIGHT:
                            case TransformMode.BOTTOMRIGHT:
                                deltaMouse.X = (snap.X - (focusBound.X + focusBound.Width)) / Zoom;
                                break;
                        }
                        switch (Mode)
                        {
                            case TransformMode.BOTTOMLEFT:
                            case TransformMode.BOTTOMMIDDLE:
                            case TransformMode.BOTTOMRIGHT:
                                deltaMouse.Y = (snap.Y - (focusBound.Y + focusBound.Height)) / Zoom;
                                break;
                            case TransformMode.TOPLEFT:
                            case TransformMode.TOPMIDDLE:
                            case TransformMode.TOPRIGHT:
                                deltaMouse.Y = (snap.Y - focusBound.Y) / Zoom;
                                break;
                        }
                    }
                }
                else
                // Icon Snap
                if(EnableSnap && FocusedIcon != null)
                {
                    var focusBound = GetIconBounds(FocusedIcon, true);
                    var focalPoint = new PointF((focusBound.X + focusBound.Width / 2), (focusBound.Y + focusBound.Height / 2));

                    if (Math.Pow(MouseLocation.X - focalPoint.X, 2) +
                        Math.Pow(MouseLocation.Y - focalPoint.Y, 2) >
                        Math.Pow(5 * Zoom, 2))
                    {
                        deltaMouse = new PointF(
                            (MouseLocation.X - focalPoint.X) / Zoom,
                            (MouseLocation.Y - focalPoint.Y) / Zoom);
                    }
                    else
                    foreach (var icon in Icons)
                    {
                        // skip selected icons
                        if (_selectedIcons.Contains(icon))
                            continue;

                        // check edges of focus bound with icon
                        var iconBounds = GetIconBounds(icon, true);

                        // if edges are within threshold then snap to them 
                        // (delta = (snap.x - focus) / Zoom)

                        var side = 
                                focusBound.Y + focusBound.Height > iconBounds.Y && 
                                focusBound.Y < iconBounds.Y + iconBounds.Height
                                && Math.Abs(deltaMouse.X) < 0.2f;

                        if (side)
                        {
                            var f = focusBound.X + focusBound.Width;
                            var i = iconBounds.X;
                            if (Math.Abs(f - i) < 0.4 * Zoom) deltaMouse.X = (i - f) / Zoom;

                            f = focusBound.X;
                            i = iconBounds.X + iconBounds.Width;
                            if (Math.Abs(f - i) < 0.4 * Zoom) deltaMouse.X = (i - f) / Zoom;
                        }

                        var top =
                                focusBound.X + focusBound.Width > iconBounds.X &&
                                focusBound.X < iconBounds.X + iconBounds.Width
                                && Math.Abs(deltaMouse.Y) < 0.2f;

                        if (top)
                        {
                            var f = focusBound.Y + focusBound.Height;
                            var i = iconBounds.Y;
                            if (Math.Abs(f - i) < 0.4 * Zoom) deltaMouse.Y = (i - f) / Zoom;

                            f = focusBound.Y;
                            i = iconBounds.Y + iconBounds.Height;
                            if (Math.Abs(f - i) < 0.4 * Zoom) deltaMouse.Y = (i - f) / Zoom;
                        }
                    }
                }
            }


            // perform dragging
            foreach (var icon in _selectedIcons)
            {
                if (Mode == TransformMode.ROTATE)
                    if(PositionMode == PositionMode.End)
                        icon.RZ += deltaMouse.X / 20;
                    else
                        icon.StartRZ += deltaMouse.X / 20;

                if (Mode == TransformMode.DRAG)
                    icon.Move(PositionMode, deltaMouse.X, -deltaMouse.Y, 0, 0);

                if (Mode == TransformMode.MIDDLELEFT || Mode == TransformMode.TOPLEFT || Mode == TransformMode.BOTTOMLEFT)
                    icon.Move(PositionMode, deltaMouse.X / 2, 0, -deltaMouse.X, 0);

                if (Mode == TransformMode.MIDDLERIGHT || Mode == TransformMode.TOPRIGHT || Mode == TransformMode.BOTTOMRIGHT)
                    icon.Move(PositionMode, deltaMouse.X / 2, 0, deltaMouse.X, 0);

                if (Mode == TransformMode.TOPMIDDLE || Mode == TransformMode.TOPLEFT || Mode == TransformMode.TOPRIGHT)
                    icon.Move(PositionMode, 0, -deltaMouse.Y / 2, 0, -deltaMouse.Y);

                if (Mode == TransformMode.BOTTOMMIDDLE || Mode == TransformMode.BOTTOMLEFT || Mode == TransformMode.BOTTOMRIGHT)
                    icon.Move(PositionMode, 0, -deltaMouse.Y / 2, 0, deltaMouse.Y);
            }


            // redraw panel
            drawPanel.Invalidate();
        }

        private ISelectScreenIcon FocusedIcon;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (frameTimer.Enabled)
                return;

            // start panning camera
            if (Mode == TransformMode.NONE && e.Button == MouseButtons.Right)
            {
                MovingCamera = true;
            }

            // begin transforming
            if (!MovingCamera && e.Button == MouseButtons.Left)
            {
                Mode = PendingMode;

                if (Mode != TransformMode.NONE)
                {
                    Do();

                    FocusedIcon = _selectedIcons[0];
                    var smallest = double.MaxValue;

                    foreach (var v in SelectedIcons)
                    {
                        var b = GetIconBounds(v);
                        var distance = Math.Pow(b.X + b.Width / 2 - MouseLocation.X, 2) + Math.Pow(b.Y + b.Height / 2 - MouseLocation.Y, 2);
                        if (distance < smallest)
                        {
                            FocusedIcon = v;
                            smallest = distance;
                        }
                    }
                }
                else
                {
                    Selecting = true;
                    StartSelect = MouseLocation;
                }
            }

            // don't commit transform
            if (e.Button == MouseButtons.Right && Mode != TransformMode.NONE)
            {
                //Undo();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (frameTimer.Enabled)
                return;

            if (!MovingCamera && e.Button == MouseButtons.Left)
            {
                var selectOne = Math.Sqrt(Math.Pow(MouseLocation.X - StartSelect.X, 2) + Math.Pow(MouseLocation.Y - StartSelect.Y, 2)) < 1;
                if (Mode == TransformMode.NONE && Selecting)
                {
                    
                    if(selectOne)
                    {
                        var selIcon = Icons.LastOrDefault(ico => SelectArea.IntersectsWith(GetIconBounds(ico)));

                        if (selIcon != null)
                            SelectItems(new ISelectScreenIcon[] { selIcon });
                        else
                            SelectItems(new ISelectScreenIcon[0]);

                    }
                    else
                    {
                        List<ISelectScreenIcon> selectedIcons = new List<ISelectScreenIcon>();

                        foreach (var icon in Icons)
                            if (SelectArea.IntersectsWith(GetIconBounds(icon)))
                                selectedIcons.Add(icon);

                        SelectItems(selectedIcons);
                    }
                }
                else
                {
                    // invoke icon move event
                    if (Mode != TransformMode.NONE && 
                        _selectedIcons.Count > 0)
                        ItemMoved.Invoke(this, EventArgs.Empty);
                }

                Mode = TransformMode.NONE;
            }

            Selecting = false;
            MovingCamera = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawPanel_MouseLeave(object sender, EventArgs e)
        {
            Mode = TransformMode.NONE;
            drawPanel.Invalidate();
        }




        private Stack<List<IconUndoState>> UndoStack = new Stack<List<IconUndoState>>();
        private Stack<List<IconUndoState>> RedoStack = new Stack<List<IconUndoState>>();

        /// <summary>
        /// 
        /// </summary>
        private List<IconUndoState> GetState()
        {
            var state = new List<IconUndoState>();

            foreach (var SelectedIcon in _selectedIcons)
                state.Add(new IconUndoState(SelectedIcon));

            return state;
        }

        /// <summary>
        /// 
        /// </summary>
        private void RestoreState(List<IconUndoState> states)
        {
            // restore states
            foreach (var state in states)
                state.Restore();

            // reselect icons
            _selectedIcons = (states.Select(e => e.Icon).ToList());

            // invoke selection change hack
            if (_selectedIcons.Count > 0)
                OnSelectedIconChanged(new PropertyValueChangedEventArgs(null, null));

            // redraw panel
            drawPanel.Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Do()
        {
            RedoStack.Clear();

            var state = GetState();
            if (state.Count > 0)
                UndoStack.Push(state);
        }

        /// <summary>
        /// 
        /// </summary>
        private void Undo()
        {
            // empty undo stack
            // do nothing
            if (UndoStack.Count == 0)
                return;

            // generate redo stack
            RedoStack.Push(GetState());

            // pop undo stack
            RestoreState(UndoStack.Pop());

            ItemMoved.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        private void Redo()
        {
            // empty redo stack
            // do nothing
            if (RedoStack.Count == 0)
                return;

            // generate undo stack
            UndoStack.Push(GetState());

            // pop redo stack
            RestoreState(RedoStack.Pop());
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void undoButton_Click(object sender, EventArgs e)
        {
            Undo();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void redoButton_Click(object sender, EventArgs e)
        {
            Redo();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playButton_Click(object sender, EventArgs e)
        {
            if (!frameTimer.Enabled)
            {
                // regenerate animation
                foreach (var icon in Icons)
                    icon.GenerateAnimation();

                playButton.Image = Properties.Resources.stop;
                Play();
            }
            else
            {
                playButton.Image = Properties.Resources.play;
                Stop();
            }

            drawPanel.Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startEndToggle_Click(object sender, EventArgs e)
        {
            if (PositionMode == PositionMode.End)
            {
                startEndToggle.Image = Properties.Resources.start;
                PositionMode = PositionMode.Start;
            }
            else
            {
                startEndToggle.Image = Properties.Resources.end;
                PositionMode = PositionMode.End;
            }

            drawPanel.Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            GridEnabled = !GridEnabled;

            snapButton.Enabled = !GridEnabled;

            RefreshDraw();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xOffTextBox_TextChanged(object sender, EventArgs e)
        {
            if (sender is ToolStripTextBox tb)
            {
                if (!float.TryParse(tb.Text, out float value))
                    tb.Text = "0";
                else
                {
                    if (value > 0)
                    {
                        if (tb == gridWidthBox)
                            GridWidth = (int)value;

                        if (tb == gridHeightBox)
                            GridHeight = (int)value;
                    }
                }

                RefreshDraw();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backColorItem_Click(object sender, EventArgs e)
        {
            using (var d = new ColorDialog())
            {
                if (sender == backColorItem)
                    d.Color = DrawBackColor;

                if (sender == gridColorItem)
                    d.Color = GridColor;

                if (d.ShowDialog() == DialogResult.OK)
                {
                    if (sender == backColorItem)
                        DrawBackColor = d.Color;

                    if (sender == gridColorItem)
                        GridColor = d.Color;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomIn(object sender, EventArgs e)
        {
            if (Zoom < 30)
            {
                Zoom += 1;
                gridWidthBox.Text = (GridWidth + 4).ToString();
                gridHeightBox.Text = (GridHeight + 4).ToString();
            }
            drawPanel.Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomOut(object sender, EventArgs e)
        {
            if (Zoom > 2)
            {
                Zoom -= 1;
                gridWidthBox.Text = (GridWidth - 4).ToString();
                gridHeightBox.Text = (GridHeight - 4).ToString();
            }
            drawPanel.Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void snapButton_Click(object sender, EventArgs e)
        {
            EnableSnap = !EnableSnap;

            snapButton.Image = EnableSnap ? Properties.Resources.snap_enable : Properties.Resources.snap_disable;
        }

        private void drawPanel_Scroll(object sender, ScrollEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonResetAnimation_Click(object sender, EventArgs e)
        {
            Do();

            foreach(var icon in SelectedIcons)
            {
                icon.StartX = icon.X;
                icon.StartY = icon.Y;
                icon.StartZ = icon.Z;
                icon.StartRX = icon.RZ;
                icon.StartRY = icon.RY;
                icon.StartRZ = icon.RZ;
                icon.StartHeight = icon.Height;
                icon.StartWidth = icon.Width;
            }

            RefreshDraw();
        }
    }

    public class DoubleBufferedPanel : Panel
    {
        public new event KeyEventHandler KeyDown;
        public DoubleBufferedPanel()
        {
            DoubleBuffered = true;
            this.SetStyle(ControlStyles.Selectable, true);
            this.TabStop = true;
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            KeyDown.Invoke(this, e);
            base.OnKeyDown(e);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            this.Focus();
            base.OnMouseEnter(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();
            base.OnMouseDown(e);
        }
        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down) return true;
            if (keyData == Keys.Left || keyData == Keys.Right) return true;
            return base.IsInputKey(keyData);
        }
        protected override void OnEnter(EventArgs e)
        {
            this.Invalidate();
            base.OnEnter(e);
        }
        protected override void OnLeave(EventArgs e)
        {
            this.Invalidate();
            base.OnLeave(e);
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (this.Focused)
            {
                var rc = this.ClientRectangle;
                rc.Inflate(-2, -2);
                ControlPaint.DrawFocusRectangle(pe.Graphics, rc);
            }
        }
    }
}
