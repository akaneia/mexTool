using mexTool.GUI.FileSystem;
using System;
using System.Windows.Forms;
using mexTool.Core;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;

namespace mexTool.GUI.Pages
{
    public partial class CodesPage : UserControl
    {
        private CodesManager MEXCodes = new CodesManager();

        private BindingListExt<AddCode> Codes = new BindingListExt<AddCode>();

        private static Regex RegHEX = new Regex(@"[0-9a-fA-F]+");

        private static Color ValidBackColor = Color.FromArgb(64, 64, 64);
        private static Color InvalidBackColor = Color.FromArgb(128, 64, 64);

        public class AddCodeError
        {
            public string Description;

            public int LineIndex;
        }

        private class AddCode : ICheckable
        {
            public bool Enabled = false;

            public string Name = "";

            public string Creator = "";

            public string Description = "";

            public string Code = "";

            public bool IsChecked()
            {
                return Enabled;
            }

            public void SetCheckState(bool state)
            {
                Enabled = state;
            }

            // public List<byte> Data = new List<byte>();

            public override string ToString()
            {
                return Name;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public bool TryCompileCode(out byte[] code, out AddCodeError error)
            {
                code = null;
                error = new AddCodeError();
                error.Description = "";
                error.LineIndex = 0;

                var lines = Code.Split(
                    new string[] { "\r\n", "\r", "\n" },
                    StringSplitOptions.None
                    );

                List<byte> data = new List<byte>();

                foreach (var l in lines)
                {
                    if (string.IsNullOrEmpty(l))
                    {
                        error.LineIndex += 1;
                        continue;
                    }

                    // remove spaces
                    var trimmed = Regex.Replace(l.Trim(), @"\s+", "");

                    // check if valid code line
                    // check if in hex format
                    if (trimmed.Length == 16 && RegHEX.Match(trimmed).Success)
                    {
                        data.AddRange(StringToByteArray(trimmed));
                    }
                    else
                    {
                        error.Description = "Invalid HEX Format";
                        return false;
                    }
                    error.LineIndex += 1;
                }

                code = data.ToArray();
                return true;
            }
        }

        public CodesPage()
        {
            InitializeComponent();

            InitMEXCodes();

            LoadCodeINI();

            codeList.DataSource = Codes;
            codeList.SelectedIndex = -1;
        }

        public void InitMEXCodes()
        {
            MEXCodes.LoadCodesGCT(System.IO.File.ReadAllBytes(ApplicationSettings.MexCodePath));
        }

        public void SaveCodes(out string[] failedCodes)
        {
            failedCodes = null;
            MEX.ImageResource?.AddFile("codes.gct", MEXCodes.GenerateCodes(CompileCodeList(false, true, out failedCodes)));
            MEX.ImageResource?.AddFile("codes.ini", GenerateCodeINI());
        }

        private static byte[] StringToByteArray(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }

        public static int GetHexVal(char hex)
        {
            int val = (int)hex;
            //For uppercase A-F letters:
            //return val - (val < 58 ? 48 : 55);
            //For lowercase a-f letters:
            //return val - (val < 58 ? 48 : 87);
            //Or the two combined, but a bit slower:
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadCodeINI()
        {
            if (MEX.ImageResource.FileExists("codes.ini"))
            {
                using (MemoryStream s = new MemoryStream(MEX.ImageResource.GetFileData("codes.ini")))
                using (StreamReader r = new StreamReader(s))
                {
                    AddCode c = null;

                    while (!r.EndOfStream)
                    {
                        var line = r.ReadLine();

                        if (line.StartsWith("$"))
                        {
                            if (c != null && c.Code.Length > 0)
                                Codes.Add(c);

                            c = new AddCode();

                            var l = line.Substring(1);

                            if (l.StartsWith("!"))
                            {
                                c.Enabled = true;
                                l = l.Substring(1);
                            }

                            var name = Regex.Match(l, @"(?<=\[).+?(?=\]\s*$)");

                            if (name.Success)
                            {
                                c.Name = l.Substring(0, name.Groups[0].Index - 1).Trim();
                                c.Creator = name.Value;
                            }
                            else
                            {
                                c.Name = l;
                                c.Creator = "";
                            }
                        }

                        if (c != null)
                        {
                            if (line.StartsWith("*"))
                                c.Description += line.Substring(1).Trim() + Environment.NewLine;
                            else
                            {
                                var trimmed = Regex.Replace(line, @"\s+", "");

                                // check if valid code line
                                if (trimmed.Length == 16 && RegHEX.Match(trimmed).Success)
                                    c.Code += line + Environment.NewLine;
                            }
                        }
                    }

                    if (c != null && c.Code.Length > 0)
                        Codes.Add(c);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private byte[] GenerateCodeINI()
        {
            using (MemoryStream s = new MemoryStream())
            using (StreamWriter w = new StreamWriter(s) { AutoFlush = true })
            {
                foreach (var c in Codes)
                {
                    w.WriteLine($"${(c.Enabled ? "!" : "")}{c.Name}{(string.IsNullOrEmpty(c.Creator) ? "" : $" [{c.Creator}]")}");

                    var desc_lines = c.Description.Split(
                        new string[] { "\r\n", "\r", "\n" },
                        StringSplitOptions.None
                        );

                    foreach (var l in desc_lines)
                    {
                        if (string.IsNullOrEmpty(l))
                            continue;
                        w.WriteLine($"*{l}");
                    }

                    w.WriteLine(c.Code);
                }

                return s.ToArray();
            }
        }

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
                    if (CheckCodeConflicts(c, out string errormessage1))
                    {
                        labelError.Text = errormessage1;
                        tbCodeData.BackColor = InvalidBackColor;
                    }
                    else
                    // check conflicts with mex codes
                    if (MEXCodes.CheckCodeConflicts(data, out string errormessage))
                    {
                        labelError.Text = errormessage;
                        tbCodeData.BackColor = InvalidBackColor;
                    }
                    // no errors found
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
            File.WriteAllBytes("test.ini", GenerateCodeINI());
        }

        private void tbCodeName_TextChanged(object sender, EventArgs e)
        {
            if (codeList.SelectedItem is AddCode c)
                c.Name = tbCodeName.Text;
        }

        private void tbCreator_TextChanged(object sender, EventArgs e)
        {
            if (codeList.SelectedItem is AddCode c)
                c.Creator = tbCreator.Text;
        }

        private void tbCodeDescription_TextChanged(object sender, EventArgs e)
        {
            if (codeList.SelectedItem is AddCode c)
                c.Description = tbCodeDescription.Text;
        }
    }
}
