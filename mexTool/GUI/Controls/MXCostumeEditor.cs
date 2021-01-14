using HSDRaw;
using HSDRaw.Common;
using HSDRaw.MEX.Misc;
using mexTool.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace mexTool.GUI.Controls
{
    public partial class MXCostumeEditor : UserControl
    {
        private MEXFighter _fighter;
        private BindingList<MEXCostume> _costumes;
        private BindingList<MEXCostume> _kirbyCostumes;

        public bool KirbyEnabled
        {
            get
            {
                return enableKirbyButton.Image == ImageEnable;
            }
            set
            {
                enableKirbyButton.Image = value ? ImageEnable : ImageDisable;
                kirbyPanel.Visible = value;
            }
        }

        private Bitmap ImageEnable = Properties.Resources.kirby_costume;
        private Bitmap ImageDisable = Properties.Resources.kirby_costume_no;

        /// <summary>
        /// 
        /// </summary>
        public MXCostumeEditor()
        {
            InitializeComponent();

            mxListBox1.ItemHeight = 32;

            ((Control)cspBox).AllowDrop = true;

            Disposed += (sender, args) =>
            {
                _costumes.ListChanged -= CostumeUpdated;

                if (cspBox.Image != null)
                    cspBox.Image.Dispose();
            };
        }

        private static int KirbyIndex = 4;

        private static int GAWIndex = 24;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fighter"></param>
        public void SetFighter(MEXFighter fighter)
        {
            _fighter = fighter;

            _costumes = fighter.Costumes;
            _kirbyCostumes = fighter.KirbyCostumes;
            
            // kirby costume editing
            if (MEX.Fighters.IndexOf(_fighter) == KirbyIndex ||
                MEX.Fighters.IndexOf(_fighter) == GAWIndex)
            {
                enableKirbyButton.Enabled = false;
                _costumes.ListChanged -= CostumeUpdated;
                _costumes.ListChanged += CostumeUpdated;
            }
            else
            {
                enableKirbyButton.Enabled = true;
            }

            // general editing
            mxListBox1.DataSource = _costumes;
            kirbyListbox.DataSource = _kirbyCostumes;

            KirbyEnabled = _kirbyCostumes.Count != 0;

            ResizeKirbyPanel();
        }

        private HSDRaw.MEX.Misc.MEX_GawColor _removedGawCostume;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void CostumeUpdated(object sender, ListChangedEventArgs args)
        {
            // game and watch costume adjustments
            switch (args.ListChangedType)
            {
                case ListChangedType.ItemDeleted:
                    if (MEX.Fighters.IndexOf(_fighter) == GAWIndex)
                    {
                        _removedGawCostume = MEX.GaWColors[args.NewIndex];
                        MEX.GaWColors.RemoveAt(args.NewIndex);
                    }
                    if (MEX.Fighters.IndexOf(_fighter) == KirbyIndex)
                    {
                        foreach (var v in MEX.Fighters)
                        {
                            if (v.KirbyCostumes != null && v.KirbyCostumes.Count > 0)
                                v.KirbyCostumes.RemoveAt(args.NewIndex);
                        }
                    }
                    break;
                case ListChangedType.ItemAdded:
                    if (MEX.Fighters.IndexOf(_fighter) == GAWIndex)
                    {
                        MEX.GaWColors.Insert(args.NewIndex,
                            _removedGawCostume == null ?
                            new HSDRaw.MEX.Misc.MEX_GawColor() { FillColor = Color.Black, OutlineColor = Color.FromArgb(128, 255, 255, 255) } :
                            _removedGawCostume);
                        _removedGawCostume = null;

                        _costumes[args.NewIndex].Costume.FileName = "PlGwNr.dat";
                        _costumes[args.NewIndex].Costume.JointSymbol = "PlyGamewatch5K_Share_joint";
                    }

                    if (MEX.Fighters.IndexOf(_fighter) == KirbyIndex)
                    {
                        foreach (var v in MEX.Fighters)
                        {
                            if (v.KirbyCostumes != null && v.KirbyCostumes.Count > 0)
                                v.KirbyCostumes.Insert(args.NewIndex, GenerateCostume());
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mxListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            RefreshSelected();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kirbyListbox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (kirbyListbox.SelectedItem is MEXCostume costume)
                kirbyPropertyGrid.SelectedObject = costume;
        }

        /// <summary>
        /// 
        /// </summary>
        private void RefreshSelected()
        {
            if (mxListBox1.SelectedItem is MEXCostume costume)
            {
                if (cspBox.Image != null)
                    cspBox.Image.Dispose();
                cspBox.Image = GraphicExtensions.TOBJToBitmap(costume.CSP);

                gawButton.Visible = MEX.Fighters.IndexOf(_fighter) == GAWIndex;

                if (MEX.Fighters.IndexOf(_fighter) == GAWIndex)
                    mxPropertyGrid1.SelectedObject = MEX.GaWColors[mxListBox1.SelectedIndex];
                else
                    mxPropertyGrid1.SelectedObject = costume;
            }
            mxListBox1.Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enableKirbyButton_Click(object sender, EventArgs e)
        {
            KirbyEnabled = !KirbyEnabled;

            kirbyPanel.Visible = KirbyEnabled;

            if (KirbyEnabled)
                for (int i = 0; i < MEX.Fighters[KirbyIndex].Costumes.Count; i++)
                    _kirbyCostumes.Add(new MEXCostume());
            else
                _kirbyCostumes.Clear();

            RefreshSelected();

            ResizeKirbyPanel();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClone_Click(object sender, EventArgs e)
        {
            if (mxListBox1.SelectedItem is MEXCostume costume)
            {
                _costumes.Add(new MEXCostume()
                {
                    Costume = HSDAccessor.DeepClone<HSDRaw.MEX.MEX_CostumeFileSymbol>(costume.Costume),
                    Icon = HSDAccessor.DeepClone<HSD_TOBJ>(costume.Icon),
                    CSP = costume.CSP == null ? null : HSDAccessor.DeepClone<HSD_TOBJ>(costume.CSP)
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            _costumes.Add(GenerateCostume());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private MEXCostume GenerateCostume()
        {
            var costume = new MEXCostume()
            {
                Costume = new HSDRaw.MEX.MEX_CostumeFileSymbol()
            };

            using (var bmp = new Bitmap(24, 24))
                costume.FromImage(bmp);

            return costume;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (mxListBox1.SelectedItem is MEXCostume costume &&
                MessageBox.Show(
                    "Are you sure you want to delete the selected costume?",
                    "Delete Costume",
                    MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                if (!(MEX.Fighters.IndexOf(_fighter) == GAWIndex) &&
                    MEX.ImageResource.FileExists(costume.FileName) &&
                    MessageBox.Show(
                        "Do you want to delete costume file as well?",
                        "Delete Costume File",
                        MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                    MEX.ImageResource.DeleteFile(costume.FileName);

                _costumes.Remove(costume);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveUp_Click(object sender, EventArgs e)
        {
            if (MEX.Fighters.IndexOf(_fighter) == KirbyIndex)
                return;

            var index = mxListBox1.SelectedIndex;
            if (_costumes.MoveUp(index))
                mxListBox1.SelectedIndex = index - 1;
            _removedGawCostume = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveDown_Click(object sender, EventArgs e)
        {
            if (MEX.Fighters.IndexOf(_fighter) == KirbyIndex)
                return;

            var index = mxListBox1.SelectedIndex;
            if (_costumes.MoveDown(index))
                mxListBox1.SelectedIndex = index + 1;
            _removedGawCostume = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mxButton2_Click(object sender, EventArgs e)
        {
            if (mxListBox1.SelectedItem is MEXCostume costume)
            {
                using (SaveFileDialog d = new SaveFileDialog())
                {
                    d.Filter = "PNG (*.png)|*.png";

                    if (d.ShowDialog() == DialogResult.OK)
                        using (var bmp = costume.ToImage())
                            bmp.Save(d.FileName);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mxButton1_Click(object sender, EventArgs e)
        {
            if (mxListBox1.SelectedItem is MEXCostume costume)
            {
                using (OpenFileDialog d = new OpenFileDialog())
                {
                    d.Filter = "PNG (*.png)|*.png";

                    if (d.ShowDialog() == DialogResult.OK)
                        using (var bmp = new Bitmap(d.FileName))
                        {
                            costume.FromImage(bmp);
                            RefreshSelected();
                        }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Resize(object sender, EventArgs e)
        {
            ResizeKirbyPanel();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResizeKirbyPanel()
        {
            var start = new Point(panel1.Location.X, panel1.Location.Y + (Location.Y - panel1.Location.Y));

            if (_fighter != null && !KirbyEnabled)
            {
                fighterPanel.Location = start;
                fighterPanel.Width = panel1.Width;
                fighterPanel.Height = panel1.Height;
            }
            else
            {
                fighterPanel.Location = start;
                fighterPanel.Width = panel1.Width;
                fighterPanel.Height = panel1.Height / 2;

                kirbyPanel.Location = new Point(start.X, start.Y + panel1.Height / 2);
                kirbyPanel.Width = panel1.Width;
                kirbyPanel.Height = panel1.Height / 2;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datFile"></param>
        /// <param name="stc"></param>
        /// <param name="csp"></param>
        /// <returns></returns>
        private MEXCostume ToCostume(string fileName, HSDRawFile file, HSD_TOBJ stc, HSD_TOBJ csp)
        {
            // create blank stock icon if one isn't set
            if (stc == null)
                using (var bmp = new Bitmap(24, 24))
                    stc = bmp.ToTOBJ(HSDRaw.GX.GXTexFmt.CI4, HSDRaw.GX.GXTlutFmt.IA8);

            // 
            var costume = new MEXCostume()
            {
                Costume = new HSDRaw.MEX.MEX_CostumeFileSymbol()
                {
                    FileName = Path.GetFileName(fileName)
                },
                Icon = stc,
                CSP = csp
            };

            // detect roots
            foreach (var r in file.Roots)
            {
                if (r.Name.EndsWith("_matanim_joint"))
                    costume.MaterialSymbol = r.Name;
                else
                if (r.Name.EndsWith("_joint"))
                    costume.ModelSymbol = r.Name;
            }

            return costume;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addPackage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog d = new OpenFileDialog())
            {
                d.Filter = "Costume DAT or Package (*.dat, *.zip)|*.dat;*.zip;";

                if(d.ShowDialog() == DialogResult.OK)
                {
                    switch (Path.GetExtension(d.FileName).ToLower())
                    {
                        case ".zip":
                            using (FileStream zipToOpen = new FileStream(d.FileName, FileMode.Open))
                            using (var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
                            {
                                string fileName = "";
                                byte[] datFile = null;
                                HSD_TOBJ csp = null;
                                HSD_TOBJ stc = null;
                                foreach (var v in archive.Entries)
                                {
                                    Console.WriteLine(v.FullName);
                                    if (v.Name.EndsWith(".dat"))
                                    {
                                        using (var s = v.Open())
                                        using (var deom = new MemoryStream())
                                        {
                                            s.CopyTo(deom);
                                            datFile = deom.ToArray();
                                            fileName = v.Name;
                                        }
                                    }
                                    if (v.Name.Equals("stc.png"))
                                    {
                                        using (var s = v.Open())
                                        using (var img = new Bitmap(s))
                                            stc = img.ToTOBJ(HSDRaw.GX.GXTexFmt.CI4, HSDRaw.GX.GXTlutFmt.RGB5A3);
                                    }
                                    if (v.Name.Equals("csp.png"))
                                    {
                                        using (var s = v.Open())
                                        using (var img = new Bitmap(s))
                                            csp = img.ToTOBJ(HSDRaw.GX.GXTexFmt.CI8, HSDRaw.GX.GXTlutFmt.RGB5A3);
                                    }
                                }

                                if (datFile != null)
                                {
                                    var costume = ToCostume(fileName, new HSDRawFile(datFile), stc, csp);
                                    MEX.ImageResource.AddFile(fileName, datFile);
                                    _costumes.Add(costume);
                                }
                            }
                            break;
                        case ".dat":
                            {
                                var costume = ToCostume(Path.GetFileName(d.FileName), new HSDRawFile(d.FileName), null, null);
                                MEX.ImageResource.AddFile(Path.GetFileName(d.FileName), d.FileName);
                                _costumes.Add(costume);
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReplaceCSP_Click(object sender, EventArgs e)
        {
            if (mxListBox1.SelectedItem is MEXCostume costume)
            {
                using (OpenFileDialog d = new OpenFileDialog())
                {
                    d.Filter = "PNG (*.png)|*.png";

                    if (d.ShowDialog() == DialogResult.OK)
                        using (var bmp = new Bitmap(d.FileName))
                        {
                            costume.CSP = bmp.ToTOBJ(HSDRaw.GX.GXTexFmt.CI8, HSDRaw.GX.GXTlutFmt.RGB5A3);
                            RefreshSelected();
                        }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExportCSP_Click(object sender, EventArgs e)
        {
            if (mxListBox1.SelectedItem is MEXCostume costume)
            {
                using (SaveFileDialog d = new SaveFileDialog())
                {
                    d.Filter = "PNG (*.png)|*.png";

                    if (d.ShowDialog() == DialogResult.OK)
                        using (var bmp = GraphicExtensions.TOBJToBitmap(costume.CSP))
                            bmp.Save(d.FileName);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cspBox_DragDrop(object sender, DragEventArgs e)
        {
            if (mxListBox1.SelectedItem is MEXCostume costume)
                if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
                {
                    Array data = ((IDataObject)e.Data).GetData("FileDrop") as Array;
                    if (data != null)
                    {
                        if ((data.Length == 1) && (data.GetValue(0) is String))
                        {
                            var filename = ((string[])data)[0];

                            using (var bmp = new Bitmap(filename))
                                costume.CSP = bmp.ToTOBJ(HSDRaw.GX.GXTexFmt.CI8, HSDRaw.GX.GXTlutFmt.RGB5A3);

                            RefreshSelected();
                        }
                    }
                }
        }

        private void cspBox_DragOver(object sender, DragEventArgs e)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cspBox_DragEnter(object sender, DragEventArgs e)
        {
            string filename = String.Empty;

            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                Array data = ((IDataObject)e.Data).GetData("FileDrop") as Array;
                if (data != null)
                {
                    if ((data.Length == 1) && (data.GetValue(0) is String))
                    {
                        filename = ((string[])data)[0];
                        string ext = Path.GetExtension(filename).ToLower();
                        if ((ext == ".jpg") || (ext == ".png") || (ext == ".bmp"))
                        {
                            e.Effect = DragDropEffects.Copy;
                            return;
                        }
                    }
                }
            }

            e.Effect = DragDropEffects.None;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gawButton_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gawButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine(mxPropertyGrid1.SelectedObject.GetType());
            if (mxPropertyGrid1.SelectedObject is MEX_GawColor gaw && 
                mxListBox1.SelectedItem is MEXCostume costume)
            {
                using (var bmp = Tools.GawUIGen.GenerateCSP(gaw.FillColor, gaw.OutlineColor))
                    costume.CSP = bmp.ToTOBJ(HSDRaw.GX.GXTexFmt.CI8, HSDRaw.GX.GXTlutFmt.RGB5A3);

                using (var bmp = Tools.GawUIGen.GenerateStock(gaw.FillColor, gaw.OutlineColor))
                    costume.FromImage(bmp);

                RefreshSelected();
            }
        }
    }
}
