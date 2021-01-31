using MeleeMedia.Audio;
using System;
using System.Windows.Forms;

namespace mexTool.GUI.Controls
{
    public partial class MXSemEditor : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler PlayDsp;

        protected void OnPlayDsp(EventArgs e)
        {
            EventHandler handler = PlayDsp;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler ChangeDsp;

        protected void OnChangeDsp(EventArgs e)
        {
            EventHandler handler = ChangeDsp;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int SelectedSoundBankIndex { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public SEMBankScript SelectedScript
        {
            get
            {
                if (mxListBox1.SelectedItem is SEMBankScript script)
                    return script;
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public MXSemEditor()
        {
            InitializeComponent();
        }

        private SEMBank _semBank;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sb"></param>
        public void SetScript(SEMBank sb, int id)
        {
            mxListBox1.StartingItemIndex = id * 10000;
            _semBank = sb;
            mxListBox1.DataSource = sb.Scripts;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mxListBox1_DoubleClicked(object sender, EventArgs e)
        {
            if (mxListBox1.SelectedItem is SEMBankScript script)
            {
                SelectedSoundBankIndex = script.SFXID;
                OnPlayDsp(EventArgs.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mxListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (mxListBox1.SelectedItem is SEMBankScript script)
            {
                SelectedSoundBankIndex = script.SFXID;
                scriptListBox.DataSource = script.Codes;
                textBox1.DataBindings.Clear();
                textBox1.DataBindings.Add("Text", script, "Name", false, DataSourceUpdateMode.OnPropertyChanged);
                OnChangeDsp(EventArgs.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scriptListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (scriptListBox.SelectedItem is SEMCode code)
                mxPropertyGrid2.SelectedObject = code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            mxListBox1.Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void mxPropertyGrid2_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (mxListBox1.SelectedItem is SEMBankScript script)
                SelectedSoundBankIndex = script.SFXID;

            OnChangeDsp(EventArgs.Empty);

            scriptListBox.Invalidate();
            mxListBox1.Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddCode_Click(object sender, EventArgs e)
        {
            if (mxListBox1.SelectedItem is SEMBankScript script)
            {
                var index = scriptListBox.SelectedIndex + 1;

                scriptListBox.DataSource = null;

                script.Codes.Insert(Math.Max(0, index), new SEMCode(SEM_CODE.NULL));

                scriptListBox.DataSource = script.Codes;

                if (index < script.Codes.Count && index >= 0)
                    scriptListBox.SelectedIndex = Math.Max(0, index);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemoveCode_Click(object sender, EventArgs e)
        {
            if (mxListBox1.SelectedItem is SEMBankScript script)
            {
                var index = scriptListBox.SelectedIndex;

                scriptListBox.DataSource = null;

                if(index >= 0)
                    script.Codes.RemoveAt(index);

                scriptListBox.DataSource = script.Codes;

                if(index < script.Codes.Count && index >= 0)
                    scriptListBox.SelectedIndex = index;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddSound_Click(object sender, EventArgs e)
        {
            mxListBox1.DataSource = null;

            var newScript = new SEMBankScript();
            newScript.Name = "SFXNew_Script";
            newScript.Codes.Add(new SEMCode(SEM_CODE.SET_SFXID));
            newScript.Codes.Add(new SEMCode(SEM_CODE.PLAY) { Value = 128 });
            newScript.Codes.Add(new SEMCode(SEM_CODE.END_PLAYBACK));

            var scripts = _semBank.Scripts;
            Array.Resize(ref scripts, scripts.Length + 1);
            _semBank.Scripts = scripts;
            _semBank.Scripts[_semBank.Scripts.Length - 1] = newScript;

            mxListBox1.DataSource = _semBank.Scripts;
            mxListBox1.SelectedItem = newScript;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemoveSound_Click(object sender, EventArgs e)
        {
            var index = mxListBox1.SelectedIndex;

            if(index != -1)
            {
                mxListBox1.DataSource = null;

                var scripts = _semBank.Scripts;
                for (int i = index; i < scripts.Length - 1; i++)
                    scripts[i] = scripts[i + 1];
                Array.Resize(ref scripts, scripts.Length - 1);
                _semBank.Scripts = scripts;

                mxListBox1.DataSource = _semBank.Scripts;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClean_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Cleaning scripts removes unused commands and results in smaller filesize", "Clean Scripts?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int removed = 0;
                foreach (var s in _semBank.Scripts)
                    removed += s.Codes.RemoveAll(d => d.Code == SEM_CODE.NULL);

                if(removed != 0)
                    MessageBox.Show($"Removed {removed} unused codes!", "Cleaning Success");
                else
                    MessageBox.Show($"No NULL codes found", "Cleaning Success");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void RemoveSoundID(int id)
        {
            foreach (var s in _semBank.Scripts)
            {
                if (s.SFXID == id)
                    s.SFXID = 0;

                if (s.SFXID > id)
                    s.SFXID -= 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCloneScript_Click(object sender, EventArgs e)
        {
            if(mxListBox1.SelectedItem is SEMBankScript script)
            {
                mxListBox1.DataSource = null;

                var clone = script.Copy();

                var scripts = _semBank.Scripts;
                Array.Resize(ref scripts, scripts.Length + 1);
                scripts[scripts.Length - 1] = clone;
                _semBank.Scripts = scripts;

                mxListBox1.DataSource = _semBank.Scripts;
                mxListBox1.SelectedItem = clone;
            }
        }
    }
}
