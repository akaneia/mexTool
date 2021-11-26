using HSDRaw.Common;
using MeleeMedia.Audio;
using mexTool.GUI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace mexTool.GUI
{
    public interface IDrawableListItem
    {
        /// <summary>
        /// Converts item to image
        /// Image returned will be disposed
        /// </summary>
        /// <returns></returns>
        Image ToImage();
    }

    public class DoubleBufferedListBox : ListBox
    {
        public DoubleBufferedListBox()
        {
            DoubleBuffered = true;
        }
    }

    public class MXListBox : Panel
    {
        private DoubleBufferedListBox _listBox;

        private bool mShowScroll;
        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (!mShowScroll)
                    cp.Style = cp.Style & ~0x200000;
                //cp.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED     
                return cp;
            }
        }

        public bool ShowScrollbar
        {
            get { return mShowScroll; }
            set
            {
                if (value != mShowScroll)
                {
                    mShowScroll = value;
                    if (IsHandleCreated)
                        RecreateHandle();
                }
            }
        }
        public bool DisplayItemIndices { get; set; } = false;

        public int StartingItemIndex { get; set; } = 0;

        public int ItemHeight { get => _listBox.ItemHeight; set => _listBox.ItemHeight = value; }

        public ListBox.ObjectCollection Items { get => _listBox.Items; }

        private MXScrollBar scrollBar;

        public bool EnableDragReorder
        {
            get => _enableDragReorder;
            set
            {
                _listBox.AllowDrop = value;
                _enableDragReorder = value;
            }
        }
        private bool _enableDragReorder = false;

        public event EventHandler TopItemChanged;

        public object DataSource { get => _listBox.DataSource; set => _listBox.DataSource = value; }

        public object SelectedItem { get => _listBox.SelectedItem; set => _listBox.SelectedItem = value; }

        public ListBox.SelectedObjectCollection SelectedItems { get => _listBox.SelectedItems; }

        public int SelectedIndex { get => _listBox.SelectedIndex; set => _listBox.SelectedIndex = value; }

        public SelectionMode SelectionMode { get => _listBox.SelectionMode; set => _listBox.SelectionMode = value; }

        public int ImageHeight { get; set; } = 24;

        public event EventHandler SelectedValueChanged;

        public event EventHandler DoubleClicked;


        public bool EnableTOBJ
        {
            get => _enableTOBJ;
            set
            {
                _enableTOBJ = value;

                _listBox.MultiColumn = _enableTOBJ;
            }
        }
        private bool _enableTOBJ;


        protected virtual void OnSelectedValueChanged(EventArgs e)
        {
            EventHandler handler = SelectedValueChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnDoubleClicked(EventArgs e)
        {
            EventHandler handler = DoubleClicked;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnTopItemChanged(EventArgs e)
        {
            var handler = TopItemChanged;
            if (handler != null) handler(this, e);
        }

        public MXListBox() : base()
        {
            DoubleBuffered = true;
            ShowScrollbar = false;
            HorizontalScroll.Visible = false;

            scrollBar = new MXScrollBar();
            scrollBar.Scroll += (sender, args) =>
            {
                _listBox.TopIndex = (int)Math.Ceiling(scrollBar.Value / (float)_listBox.ItemHeight);
            };
            Controls.Add(scrollBar);

            TopItemChanged += (sender, args) =>
            {
                scrollBar.Value = _listBox.TopIndex * _listBox.ItemHeight;
                scrollBar.Location = new Point(Width - SystemInformation.VerticalScrollBarWidth, 0);
                scrollBar.Invalidate();
            };

            _listBox = new DoubleBufferedListBox();
            _listBox.Dock = DockStyle.Fill;
            _listBox.SelectedValueChanged += (sender, args) => OnSelectedValueChanged(args);
            _listBox.DoubleClick += (sender, args) => OnDoubleClicked(args);
            Controls.Add(_listBox);

            _listBox.Resize += (sender, args) =>
            {
                AdjustScrollBar();
            };
            _listBox.Invalidated += (sender, args) =>
            {
                AdjustScrollBar();
            };


            Invalidated += (sender, args) => _listBox.Invalidate();

            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //DoubleBuffered = true;

            _listBox.BackColor = Color.FromArgb(50, 50, 70);
            _listBox.ForeColor = Color.White;
            _listBox.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
            _listBox.BorderStyle = BorderStyle.None;

            _listBox.ItemHeight = 24;

            _listBox.DrawMode = DrawMode.OwnerDrawFixed;

            _listBox.DrawItem += OnDrawItem;
            _listBox.ColumnWidth = 170;

            _listBox.HorizontalScrollbar = false;

            _listBox.MouseMove += (sender, args) =>
            {
                if (_enableDragReorder && args.Button == MouseButtons.Left)
                {
                    if (_listBox.SelectedItem == null)
                        return;
                    _listBox.DoDragDrop(_listBox.SelectedItem, DragDropEffects.Move);
                }
            };

            _listBox.DragOver += (object sender, DragEventArgs e) =>
            {
                if(_enableDragReorder)
                    e.Effect = DragDropEffects.Move;
            };

            _listBox.DragDrop += (object sender, DragEventArgs e) =>
            {
                if (_enableDragReorder)
                {
                    Point point = _listBox.PointToClient(new Point(e.X, e.Y));
                    int index = _listBox.IndexFromPoint(point);
                    if (index < 0) index = _listBox.Items.Count - 1;
                    object data = _listBox.SelectedItem;

                    // cleanup: don't hard code this
                    if (DataSource is List<SEMCode> codelist)
                    {
                        var selected = SelectedItem;
                        _listBox.DataSource = null;

                        codelist.Remove((SEMCode)data);
                        codelist.Insert(index, (SEMCode)data);

                        _listBox.DataSource = codelist;
                        SelectedItem = selected;
                        _listBox.Invalidate();
                    }
                    if (DataSource is BindingList<HSD_TOBJ> tobjList)
                    {
                        var selected = SelectedItem;
                        _listBox.DataSource = null;

                        tobjList.Remove((HSD_TOBJ)data);
                        tobjList.Insert(index, (HSD_TOBJ)data);

                        _listBox.DataSource = tobjList;
                        SelectedItem = selected;
                        _listBox.Invalidate();
                    }

                }
            };

            AutoScroll = false;

            Disposed += (sender, args) => { _listBox.DataSource = null; _listBox.Dispose(); };
        }

        private void AdjustScrollBar()
        {
            scrollBar.Location = new Point(Width - SystemInformation.VerticalScrollBarWidth, 0);
            scrollBar.Height = _listBox.Height;

            if (_listBox.MultiColumn)
            {
                //System.Console.WriteLine(_listBox.PreferredSize);
                scrollBar.Minimum = 0;
                scrollBar.Maximum = _listBox.ColumnWidth * (_listBox.Items.Count + 1);

                scrollBar.LargeChange = scrollBar.Maximum / scrollBar.Height + _listBox.Width;
                scrollBar.SmallChange = 15;
            }
            else
            {
                scrollBar.Minimum = 0;
                scrollBar.Maximum = _listBox.PreferredHeight;

                scrollBar.LargeChange = scrollBar.Maximum / scrollBar.Height + _listBox.Height;
                scrollBar.SmallChange = 15;
            }
        }

        private int lastTopItem;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDrawItem(object sender, DrawItemEventArgs e)
        {
            if (lastTopItem != _listBox.TopIndex)
            {
                lastTopItem = _listBox.TopIndex;
                OnTopItemChanged(EventArgs.Empty);
            }

            try
            {
                e.DrawBackground();

                var item = _listBox.Items[e.Index];

                var numbrush = new SolidBrush(Color.Gray);
                var brush = new SolidBrush(ForeColor);

                var itemText = _listBox.Items[e.Index].ToString();

                if (string.IsNullOrEmpty(itemText))
                    itemText = "-";

                int offset = 0;

                if (DisplayItemIndices)
                {
                    var indText = (StartingItemIndex + e.Index).ToString() + ".";
                    var indSize = TextRenderer.MeasureText(indText, e.Font);

                    var indexBound = new Rectangle(e.Bounds.X + offset, e.Bounds.Y, indSize.Width, indSize.Height);
                    e.Graphics.DrawString(indText, e.Font, numbrush, indexBound, StringFormat.GenericDefault);

                    offset += indSize.Width;
                }

                //
                // native tobj support
                //
                if (item is HSD_TOBJ tobj)
                {
                    using (var img = tobj.ToBitmap())
                    {
                        var width = img.Width * ImageHeight / (float)img.Height;

                        e.Graphics.DrawImage(img, e.Bounds.X + offset, e.Bounds.Y, width, ImageHeight);

                        e.DrawFocusRectangle();

                        return;
                    }
                }

                var imageSize = e.Bounds.Height;
                bool drawIcon = (item is Core.MEXSoundBank) || (item is Core.MEXFighter) || item is Core.MEXMusic || item is Core.MEXStage;
                Image icon = Properties.Resources.smashball_red;

                //
                //
                //
                if ((item is Core.MEXSoundBank soundbank && soundbank.IsMEXSound) ||
                    (item is Core.MEXMusic bgm && bgm.IsMexMusic) || 
                    (item is Core.MEXStage stage && stage.IsMEXStage))
                {
                    icon = Properties.Resources.add_fighter;
                }

                //
                //
                //
                if (item is Core.MEXFighter fighter)
                {
                    if (fighter.IsMEXFighter)
                        icon = Properties.Resources.add_fighter;

                    if (e.Index >= Core.MEX.Fighters.Count - 6)
                        icon = Properties.Resources.smashball_grey;
                }

                //
                //
                //
                if(drawIcon)
                {
                    e.Graphics.DrawImage(icon, e.Bounds.X + offset, e.Bounds.Y, imageSize, imageSize);
                    offset += imageSize + 4;
                }

                //
                //
                //
                if (item is SEMCode code)
                {
                    Dictionary<SEM_CODE, Image> codeToImage = new Dictionary<SEM_CODE, Image>() 
                    { 
                        { SEM_CODE.SET_TIMER, Properties.Resources.timer } ,
                        { SEM_CODE.SET_SFXID, Properties.Resources.music } ,
                        { SEM_CODE.SET_LOOP, Properties.Resources.replace } ,
                        { SEM_CODE.EXECUTE_LOOP, Properties.Resources.undo } ,
                        { SEM_CODE.SET_PRIORITY, Properties.Resources.priority } ,
                        { SEM_CODE.ADD_PRIORITY, Properties.Resources.priority } ,
                        { SEM_CODE.PLAY, Properties.Resources.play } ,
                        { SEM_CODE.PLAY_ADD_VOLUME, Properties.Resources.play } ,
                        { SEM_CODE.SET_CHANNEL_BALANCE, Properties.Resources.balance } ,
                        { SEM_CODE.ADD_CHANNEL_BALANCE, Properties.Resources.balance } ,
                        { SEM_CODE.SET_UNUSED, Properties.Resources.smashball_grey } ,
                        { SEM_CODE.ADD_UNUSED, Properties.Resources.smashball_grey } ,
                        { SEM_CODE.SET_PITCH, Properties.Resources.sound_wave } ,
                        { SEM_CODE.ADD_PITCH, Properties.Resources.sound_wave } ,
                        { SEM_CODE.END_PLAYBACK, Properties.Resources.stop } ,
                        { SEM_CODE.LOOP_PLAYBACK, Properties.Resources.replace } ,
                        { SEM_CODE.SET_REVERB1, Properties.Resources.sound_wave } ,
                        { SEM_CODE.ADD_REVERB1, Properties.Resources.sound_wave } ,
                        { SEM_CODE.SET_REVERB2, Properties.Resources.sound_wave } ,
                        { SEM_CODE.ADD_REVERB2, Properties.Resources.sound_wave } ,
                        { SEM_CODE.SET_REVERB3, Properties.Resources.sound_wave } ,
                        { SEM_CODE.SET_REVERB4, Properties.Resources.sound_wave } ,
                        { SEM_CODE.NULL, Properties.Resources.smashball_grey } ,
                    };

                    if(codeToImage.ContainsKey(code.Code))
                    {
                        icon = codeToImage[code.Code];

                        var width = icon.Width * ImageHeight / (float)icon.Height;

                        e.Graphics.DrawImage(icon, e.Bounds.X + offset, e.Bounds.Y, width, ImageHeight);

                        offset += (int)width + 4;
                    }

                }

                //
                //
                //
                if (item is IDrawableListItem drawable)
                {
                    e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

                    using (var img = drawable.ToImage())
                    {
                        if (img != null)
                        {
                            var width = img.Width * ImageHeight / (float)img.Height;
                            e.Graphics.DrawImage(img, e.Bounds.X + offset, e.Bounds.Y, width, ImageHeight);
                            offset += (int)width + 4;
                        }
                        else
                            offset += ImageHeight + 4;
                    }
                }

                var textBound = new Rectangle(e.Bounds.X + offset, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                e.Graphics.DrawString(itemText, e.Font, brush, textBound, StringFormat.GenericDefault);

                e.DrawFocusRectangle();
            }
            catch
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearSelected()
        {
            _listBox.ClearSelected();
            _listBox.SelectedItem = null;
        }
    }
}
