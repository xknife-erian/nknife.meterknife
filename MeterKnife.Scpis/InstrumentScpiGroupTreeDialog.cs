using System;
using System.IO;
using System.Windows.Forms;
using MeterKnife.Scpis.ScpiTree;
using NKnife.GUI.WinForm;
using NKnife.IoC;
using NKnife.Utility;
using ScpiKnife;

namespace MeterKnife.Scpis
{
    public partial class InstrumentScpiGroupTreeDialog : SimpleForm
    {
        public InstrumentScpiGroupTreeDialog()
        {
            InitializeComponent();

            _DeleteInstrumentToolStripButton.Enabled = false;
            _EditInstrumentToolStripButton.Enabled = false;

            _ConfirmButton.Enabled = false;

            _Tree.AfterSelect += (s, e) =>
            {
                _ConfirmButton.Enabled = (_Tree.SelectedNode != null);
                if (_Tree.SelectedNode is SubjectCollectionTreeNode)
                {
                    _DeleteInstrumentToolStripButton.Enabled = true;
                    _EditInstrumentToolStripButton.Enabled = true;
                }
                else
                {
                    _DeleteInstrumentToolStripButton.Enabled = false;
                    _EditInstrumentToolStripButton.Enabled = false;
                }
            };
        }

        public ScpiSubject SelectedScpiSubject
        {
            get
            {
                var treeNode = _Tree.SelectedNode;
                return (ScpiSubject) treeNode.Tag;
            }
        }
        public ScpiSubjectCollection SelectedScpiSubjectCollection
        {
            get
            {
                var node = _Tree.SelectedNode as SubjectCollectionTreeNode;
                if (node != null) 
                    return node.GetScpiSubjectCollection();
                return null;
            }
        }

        public bool CurrentIsSubject { get; private set; }

        public string CurrentMeter { get; private set; }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            UpdateTreeNodes();
        }

        private void UpdateTreeNodes()
        {
            _Tree.Nodes.Clear();
            var collections = DI.Get<IScpiInfoGetter>().GetScpiSubjectCollections();
            foreach (var collection in collections)
            {
                var treeNode = new SubjectCollectionTreeNode(collection);
                foreach (var subject in collection)
                {
                    var subNode = new SubjectGroupTreeNode
                    {
                        Text = subject.Name,
                        Tag = subject
                    };
                    treeNode.Nodes.Add(subNode);
                }
                _Tree.Nodes.Add(treeNode);
            }
            if (_Tree.Nodes.Count > 0)
            {
                _Tree.Nodes[0].Expand();
            }
        }

        private void _CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void _ConfirmButton_Click(object sender, EventArgs e)
        {
            var treeNode = _Tree.SelectedNode;
            ScpiSubjectCollection collection = null;
            var node = treeNode as SubjectCollectionTreeNode;
            if (node != null)
            {
                CurrentIsSubject = false;
                collection = node.GetScpiSubjectCollection();
            }
            else
            {
                CurrentIsSubject = true;
                collection = ((SubjectCollectionTreeNode) treeNode.Parent).GetScpiSubjectCollection();
            }
            CurrentMeter = string.Format("{0}{1} {2}", collection.Brand, collection.Name, collection.Description);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void _DeleteInstrumentToolStripButton_Click(object sender, EventArgs e)
        {
            if (!(_Tree.SelectedNode is SubjectCollectionTreeNode))
                return;
            var tip = string.Format("您选择的是 {0} 的仪器SCPI指令集文件，您确认删除？", _Tree.SelectedNode.Text);
            if (MessageBox.Show(this, tip, "删除", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                var node = (SubjectCollectionTreeNode) _Tree.SelectedNode;
                var file = node.GetScpiSubjectCollection().GetXmlFile();
                try
                {
                    File.Delete(file.FilePath);
                    MessageBox.Show(this, string.Format("{0}仪器SCPI指令集文件删除。", _Tree.SelectedNode.Text), "删除",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateTreeNodes();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(this, "文件删除异常，您是否有其他软件打开该指令集文件。", "无法删除",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void _EditInstrumentToolStripButton_Click(object sender, EventArgs e)
        {
            if (!(_Tree.SelectedNode is SubjectCollectionTreeNode))
                return;
            var node = (SubjectCollectionTreeNode) _Tree.SelectedNode;
            var collection = node.GetScpiSubjectCollection();
            var dialog = new InstrumentInfoDialog
            {
                InstBrand = collection.Brand,
                InstName = collection.Name,
                InstDescription = collection.Description
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                collection.Brand = dialog.InstBrand;
                collection.Name = dialog.InstName;
                collection.Description = dialog.InstDescription;
                collection.Save();
                UpdateTreeNodes();
            }
        }

        private void _NewToolStripButton_Click(object sender, EventArgs e)
        {
            var dialog = new InstrumentInfoDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var collection = new ScpiSubjectCollection
                {
                    Brand = dialog.InstBrand,
                    Name = dialog.InstName,
                    Description = dialog.InstDescription
                };
                var fileName = string.Format("{0}{1}.xml", collection.Brand, collection.Name);
                collection.BuildScpiFile(Path.Combine(ScpiUtil.ScpisPath, fileName));
                collection.Save();
                UpdateTreeNodes();
            }
        }
    }
}