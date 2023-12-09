using mexTool.GUI.FileSystem;
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
        private CodesManager MEXCodes = new CodesManager();

        private BindingListExt<AddCode> Codes = new BindingListExt<AddCode>();

        private static Color ValidBackColor = Color.FromArgb(64, 64, 64);
        private static Color InvalidBackColor = Color.FromArgb(128, 64, 64);

        public CodesPage()
        {
            InitializeComponent();

            InitMEXCodes();

            if (MEX.ImageResource.FileExists("codes.ini"))
            {
                foreach (var c in CodesINI.LoadCodeINI(MEX.ImageResource.GetFileData("codes.ini")))
                    Codes.Add(c);
            }
            else
            {
                if (File.Exists(ApplicationSettings.MexAddCodePath))
                    foreach (var c in CodesINI.LoadCodeINI(File.ReadAllBytes(ApplicationSettings.MexAddCodePath)))
                    {
                        c.SetCheckState(true);
                        Codes.Add(c);
                    }
            }

            codeList.DataSource = Codes;
            codeList.SelectedIndex = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        public void InitMEXCodes()
        {
            MEXCodes.LoadCodesGCT(System.IO.File.ReadAllBytes(ApplicationSettings.MexCodePath));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="failedCodes"></param>
        public void SaveCodes(out string[] failedCodes)
        {
            failedCodes = null;
            MEX.ImageResource?.AddFile("codes.gct", MEXCodes.GenerateCodes(CompileCodeList(false, true, out failedCodes)));
            MEX.ImageResource?.AddFile("codes.ini", CodesINI.GenerateCodeINI(Codes));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void codeList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (codeList.SelectedItem is AddCode code)
            {
                tbCodeName.Text = code.Name;
                tbCreator.Text = code.Creator;
                tbCodeDescription.Text = code.Description;
                tbCodeData.Text = code.Code;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Codes.Add(new AddCode() { Enabled = true });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (codeList.SelectedItem is AddCode c &&
                MessageBox.Show("Are you sure you want to remove this code?", "Remove Code", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Codes.Remove(c);
            }
        }

        /// <summary>
        /// returns true if code conflicts are found
        /// </summary>
        /// <param name="data"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private bool CheckCodeConflicts(AddCode code, out string error)
        {
            foreach (var addr in code.UsedAddresses())
            {
                // check conflicts with mex codes
                if (MEXCodes.CheckCodeConflicts(addr, out error))
                {
                    return true;
                }

                // check for additional code conflicts
                foreach (var c in Codes)
                {
                    if (c == code)
                        continue;

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
        private void tbCodeData_TextChanged(object sender, EventArgs e)
        {
            if (codeList.SelectedItem is AddCode c)
            {
                c.Code = tbCodeData.Text;

                if (c.TryCompileCode(out byte[] data, out AddCodeError error))
                {
                    // check conflicts with other codes
                    if (CheckCodeConflicts(c, out string errormessage))
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
        /// <param name="show_errors"></param>
        /// <returns></returns>
        private List<Code> CompileCodeList(bool show_errors, bool only_enabled, out string[] failedcodes)
        {
            // testing save
            List<Code> data = new List<Code>();
            List<string> failed = new List<string>();

            foreach (var v in Codes)
            {
                if (v.Enabled || !only_enabled)
                {
                    if (v.TryCompileCode(out byte[] d, out AddCodeError error) &&
                        CodesManager.TryLoadCodes(d, data, false, out string err))
                    {
                    }
                    else
                    {
                        failed.Add(v.Name);

                        if (show_errors)
                            MessageBox.Show($"Failed to add code: {v.Name}\nSee code for details", "Code Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            failedcodes = failed.ToArray();

            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            File.WriteAllBytes("test.gct", MEXCodes.GenerateCodes(CompileCodeList(true, true, out string[] failed)));
            File.WriteAllBytes("test.ini", CodesINI.GenerateCodeINI(Codes));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCodeName_TextChanged(object sender, EventArgs e)
        {
            if (codeList.SelectedItem is AddCode c)
                c.Name = tbCodeName.Text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCreator_TextChanged(object sender, EventArgs e)
        {
            if (codeList.SelectedItem is AddCode c)
                c.Creator = tbCreator.Text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCodeDescription_TextChanged(object sender, EventArgs e)
        {
            if (codeList.SelectedItem is AddCode c)
                c.Description = tbCodeDescription.Text;
        }
    }
}
