using HSDRaw.MEX;
using mexTool.Core;
using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace mexTool.GUI.Controls
{
    public partial class ItemEditor : UserControl
    {
        public object DataSource { get => mxListBox1.DataSource; set { mxPropertyGrid1.SelectedObject = null;  mxListBox1.DataSource = value; } }

        /// <summary>
        /// 
        /// </summary>
        public ItemEditor()
        {
            InitializeComponent();

            // load default items
            cloneCommonItemToolStripMenuItem.DropDownItems.AddRange(DefaultItemNames.CommonItemNames.Select((i, e) => GenerateNameTS(i, e)).ToArray());
            cloneFighterItemToolStripMenuItem.DropDownItems.AddRange(DefaultItemNames.FighterItemNames.Select((i, e) => GenerateNameTS(i, e)).ToArray());
            clonePokemonItemToolStripMenuItem.DropDownItems.AddRange(DefaultItemNames.PokemonItemNames.Select((i, e) => GenerateNameTS(i, e)).ToArray());
            cloneStageItemToolStripMenuItem.DropDownItems.AddRange(DefaultItemNames.StageItemNames.Select((i, e) => GenerateNameTS(i, e)).ToArray());

            VisibleChanged += (sender, args) => {
                mxListBox1.SelectedItem = null;
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private ToolStripMenuItem GenerateNameTS(string name, int index)
        {
            var ts = new ToolStripMenuItem(name);
            ts.ForeColor = Color.White;
            ts.Click += cloneMenuItem_Click;
            ts.Tag = index;
            return ts;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mxListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            mxPropertyGrid1.SelectedObject = mxListBox1.SelectedItem;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeItemButton_Click(object sender, EventArgs e)
        {
            if (mxListBox1.DataSource is BindingList<MEX_Item> items && mxListBox1.SelectedItem is MEX_Item item)
                items.Remove(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addBlankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mxListBox1.DataSource is BindingList<MEX_Item> items)
                items.Add(new MEX_Item());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportItemButton_Click(object sender, EventArgs e)
        {
            if (mxListBox1.SelectedItem is MEX_Item item)
            {
                using (SaveFileDialog d = new SaveFileDialog())
                {
                    d.Filter = "Item YAML (*.yml)|*.yml";

                    if(d.ShowDialog() == DialogResult.OK)
                    {
                        Serialize(d.FileName, item);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DataSource == null)
                return;

            using (OpenFileDialog d = new OpenFileDialog())
            {
                d.Filter = "Item YAML (*.yml)|*.yml";

                if (d.ShowDialog() == DialogResult.OK)
                {
                    if (mxListBox1.DataSource is BindingList<MEX_Item> items)
                        items.Add(Deserialize(File.ReadAllText(d.FileName)));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cloneMenuItem_Click(object sender, EventArgs e)
        {
            if (DataSource == null)
                return;

            MEX_Item item = null;

            var owner = ((ToolStripMenuItem)sender).OwnerItem;

            if (owner == cloneCommonItemToolStripMenuItem)
                item = HSDRaw.HSDAccessor.DeepClone<MEX_Item>(MEX.CommonItems[cloneCommonItemToolStripMenuItem.DropDownItems.IndexOf((ToolStripMenuItem)sender)]);

            if (owner == cloneFighterItemToolStripMenuItem)
                item = HSDRaw.HSDAccessor.DeepClone<MEX_Item>(MEX.FighterItems[cloneFighterItemToolStripMenuItem.DropDownItems.IndexOf((ToolStripMenuItem)sender)]);

            if (owner == clonePokemonItemToolStripMenuItem)
                item = HSDRaw.HSDAccessor.DeepClone<MEX_Item>(MEX.PokemonItems[clonePokemonItemToolStripMenuItem.DropDownItems.IndexOf((ToolStripMenuItem)sender)]);

            if (owner == cloneStageItemToolStripMenuItem)
                item = HSDRaw.HSDAccessor.DeepClone<MEX_Item>(MEX.StageItems[cloneStageItemToolStripMenuItem.DropDownItems.IndexOf((ToolStripMenuItem)sender)]);

            if (item != null)
                AddItem(item);
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddItem(MEX_Item item)
        {
            if (mxListBox1.DataSource is BindingList<MEX_Item> items)
                items.Add(item);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public MEX_Item Deserialize(string data)
        {
            var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .WithTypeInspector(inspector => new MEXTypeInspector(inspector))
            .Build();

            return deserializer.Deserialize<MEX_Item>(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        public void Serialize(string filepath, MEX_Item item)
        {
            var builder = new SerializerBuilder()
            .WithTypeInspector(inspector => new MEXTypeInspector(inspector))
            .WithNamingConvention(CamelCaseNamingConvention.Instance);

            using (StreamWriter writer = File.CreateText(filepath))
            {
                builder.Build().Serialize(writer, item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (mxListBox1.DataSource is BindingList<MEX_Item> items)
            {
                var index = mxListBox1.SelectedIndex;
                if(items.MoveUp(index))
                    mxListBox1.SelectedIndex = index - 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (mxListBox1.DataSource is BindingList<MEX_Item> items)
            {
                var index = mxListBox1.SelectedIndex;
                if(items.MoveDown(index))
                    mxListBox1.SelectedIndex = index + 1;
            }
        }
        private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void mxListBox1_VisibleChanged(object sender, EventArgs e)
        {
        }
    }
}
