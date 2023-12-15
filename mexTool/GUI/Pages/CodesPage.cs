using System;
using System.Windows.Forms;
using mexTool.Core;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using mexTool.Tools;
using System.Linq;

namespace mexTool.GUI.Pages
{
    public partial class CodesPage : UserControl
    {
        private IEnumerable<Codes> AllCodes
        {
            get
            {
                yield return MexCode;

                foreach (var c in CodesMEX)
                {
                    yield return c;
                }

                foreach (var c in CodesCustom)
                {
                    yield return c;
                }
            }
        }

        private Codes MexCode
        {
            get
            {
                return CodeLoader.FromGCT(File.ReadAllBytes(ApplicationSettings.MexCodePath));
            }
        }

        private BindingListExt<Codes> CodesMEX = new BindingListExt<Codes>();

        private BindingListExt<Codes> CodesCustom = new BindingListExt<Codes>();

        private static Color ValidBackColor = Color.FromArgb(64, 64, 64);
        private static Color InvalidBackColor = Color.FromArgb(128, 64, 64);

        /// <summary>
        /// 
        /// </summary>
        public CodesPage()
        {
            InitializeComponent();

            // load codes from file system
            if (MEX.ImageResource.FileExists("codes.ini"))
            {
                foreach (var c in CodeLoader.FromINI(MEX.ImageResource.GetFileData("codes.ini")))
                    CodesCustom.Add(c);
            }

            // reload mex codes
            ReloadCodes();

            // init code list
            codeList.DataSource = CodesMEX;
            codeList.SelectedIndex = -1;
            toolStripAddCode.Visible = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ReloadCodes()
        {
            CodesMEX.Clear();

            if (File.Exists(ApplicationSettings.MexAddCodePath))
            {
                foreach (var c in CodeLoader.FromINI(File.ReadAllBytes(ApplicationSettings.MexAddCodePath)))
                {
                    CodesMEX.Add(c);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="failedCodes"></param>
        public void SaveCodes(out string[] failedCodes)
        {
            List<Codes> success = new List<Codes>();
            List<Codes> failed = new List<Codes>();

            CompileCodeList(true, success, failed);
            failedCodes = failed.Select(e => e.Name).ToArray();

            MEX.ImageResource?.AddFile("codes.gct", CodeLoader.ToGCT(success));
            MEX.ImageResource?.AddFile("codes.ini", CodeLoader.ToINI(CodesCustom));

            //if (show_errors)
            //    MessageBox.Show($"Failed to add code: {v.Name}\nSee code for details", "Code Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (File.Exists(ApplicationSettings.MexAddCodePath))
                File.WriteAllBytes(ApplicationSettings.MexAddCodePath, CodeLoader.ToINI(CodesMEX));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="show_errors"></param>
        /// <returns></returns>
        private void CompileCodeList(bool only_enabled, List<Codes> success, List<Codes> failed)
        {
            // testing save
            foreach (var c in AllCodes)
            {
                if (only_enabled && !c.IsChecked())
                    continue;

                if (c.GetCompiled() == null ||
                    CheckCodeConflicts(c, success, false, out string errormessage))
                {
                    failed.Add(c);
                }
                else
                {
                    success.Add(c);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void codeList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (codeList.SelectedItem is Codes code)
            {
                tbCodeName.Text = code.Name;
                tbCreator.Text = code.Creator;
                tbCodeDescription.Text = code.Description;
                tbCodeData.Text = code.GetSource();
            }
            else
            {
                tbCodeName.Text = "";
                tbCreator.Text = "";
                tbCodeDescription.Text = "";
                tbCodeData.Text = "";
            }
        }

        /// <summary>
        /// returns true if code conflicts are found
        /// </summary>
        /// <param name="data"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private bool CheckCodeConflicts(Codes code, IEnumerable<Codes> codesToCheck, bool only_check_enabled, out string error)
        {
            // 
            foreach (var addr in code.UsedAddresses())
            {
                // check for additional code conflicts
                foreach (var c in codesToCheck)
                {
                    // skip the code we are checking for
                    if (c == code)
                        continue;

                    // skip non enabled checks
                    if (only_check_enabled && !c.IsChecked())
                        continue;

                    // check address conflicts with code
                    foreach (var addr2 in c.UsedAddresses())
                    {
                        if (addr == addr2)
                        {
                            error = $"Injection Address Conflict Code: {c.Name} Address: {addr.ToString("X")}";
                            return true;
                        }
                    }
                }
            }
            error = "";
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCodeName_TextChanged(object sender, EventArgs e)
        {
            if (codeList.SelectedItem is Codes c)
                c.Name = tbCodeName.Text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCreator_TextChanged(object sender, EventArgs e)
        {
            if (codeList.SelectedItem is Codes c)
                c.Creator = tbCreator.Text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCodeDescription_TextChanged(object sender, EventArgs e)
        {
            if (codeList.SelectedItem is Codes c)
                c.Description = tbCodeDescription.Text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCodeData_TextChanged(object sender, EventArgs e)
        {
            if (codeList.SelectedItem is Codes c)
            {
                if (c.SetSource(tbCodeData.Text, out CodesError error))
                {
                    // check conflicts with other codes
                    if (CheckCodeConflicts(c, AllCodes, false, out string errormessage))
                    {
                        labelError.Text = errormessage;
                        tbCodeData.BackColor = InvalidBackColor;
                    }
                    else
                    {
                        labelError.Text = $"Success: Code appears valid";
                        tbCodeData.BackColor = ValidBackColor;
                    }
                }
                else
                {
                    labelError.Text = $"{error.Description} on line {error.LineIndex}";
                    tbCodeData.BackColor = InvalidBackColor;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMEX_Click(object sender, EventArgs e)
        {
            toolStripAddCode.Visible = false;
            codeList.DataSource = CodesMEX;
            codeList.SelectedIndex = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCustom_Click(object sender, EventArgs e)
        {
            toolStripAddCode.Visible = true;
            codeList.DataSource = CodesCustom;
            codeList.SelectedIndex = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonAddCode_Click(object sender, EventArgs e)
        {
            CodesCustom.Add(new Codes() { Name = "New Code" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonRemoveCode_Click(object sender, EventArgs e)
        {
            if (codeList.SelectedItem is Codes c &&
                MessageBox.Show("Are you sure you want to remove this code?", "Remove Code", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CodesCustom.Remove(c);
            }
        }
    }
}
