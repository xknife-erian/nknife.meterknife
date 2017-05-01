using System;
using System.IO;
using System.Windows.Forms;
using MeterKnife.Scpis.ScpiTree;
using NKnife.GUI.WinForm;
using NKnife.IoC;

namespace MeterKnife.Scpis
{
    public partial class InstrumentScpiGroupTreeDialog : SimpleForm
    {
        public InstrumentScpiGroupTreeDialog()
        {
            InitializeComponent();

            _NewSubjectToolStripButton.Enabled = false;
            _DeleteInstrumentToolStripButton.Enabled = false;
            _DeleteSubjectToolStripButton.Enabled = false;
            _EditInstrumentToolStripButton.Enabled = false;
            _EditSubjectToolStripButton.Enabled = false;

            _ConfirmButton.Enabled = false;

            _Tree.AfterSelect += (s, e) =>
            {
                _ConfirmButton.Enabled = _Tree.SelectedNode is SubjectGroupTreeNode;
                if (_Tree.SelectedNode is SubjectCollectionTreeNode)
                {
                    _NewSubjectToolStripButton.Enabled = true;
                    _DeleteInstrumentToolStripButton.Enabled = true;
                    _EditInstrumentToolStripButton.Enabled = true;
                    _DeleteSubjectToolStripButton.Enabled = false;
                    _EditSubjectToolStripButton.Enabled = false;
                }
                else
                {
                    _NewSubjectToolStripButton.Enabled = false;
                    _DeleteSubjectToolStripButton.Enabled = true;
                    _EditSubjectToolStripButton.Enabled = true;
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
                    var subNode = new SubjectGroupTreeNode(subject)
                    {
                        Text = subject.Name,
                        Tag = subject
                    };
                    treeNode.Nodes.Add(subNode);
                }
                _Tree.Nodes.Add(treeNode);
            }
            if (_Tree.Nodes.Count > 0)
                _Tree.Nodes[0].Expand();
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
            CurrentMeter = $"{collection.Brand}{collection.Name} {collection.Description}";
            DialogResult = DialogResult.OK;
            Close();
        }

        private void _DeleteInstrumentToolStripButton_Click(object sender, EventArgs e)
        {
            if (!(_Tree.SelectedNode is SubjectCollectionTreeNode))
                return;
            var tip = $"您选择的是 {_Tree.SelectedNode.Text} 的仪器SCPI指令集文件，您确认删除？";
            if (MessageBox.Show(this, tip, "删除", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                var node = (SubjectCollectionTreeNode) _Tree.SelectedNode;
                var file = node.GetScpiSubjectCollection().GetXmlFile();
                try
                {
                    File.Delete(file.FilePath);
                    MessageBox.Show(this, $"{_Tree.SelectedNode.Text}仪器SCPI指令集文件删除。", "删除",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateTreeNodes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"文件删除异常，您是否有其他软件打开该指令集文件。\r\n{ex.Message}", "无法删除",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            _ConfirmButton.Enabled = _Tree.SelectedNode != null;
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
            _ConfirmButton.Enabled = _Tree.SelectedNode != null;
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
            _ConfirmButton.Enabled = _Tree.SelectedNode != null;
        }

        private void _NewSubjectToolStripButton_Click(object sender, EventArgs e)
        {
            var dialog = new SubjectInfoDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var node = _Tree.SelectedNode as SubjectCollectionTreeNode;
                if (node != null)
                {
                    var collection = node.GetScpiSubjectCollection();
                    var subject = new ScpiSubject();
                    subject.OwnerCollection = collection;
                    subject.Name = dialog.GroupName;
                    collection.Add(subject);
                    collection.Save();
                    UpdateTreeNodes();
                    _Tree.SelectedNode = node;
                }
            }
            _ConfirmButton.Enabled = _Tree.SelectedNode != null;
        }

        private void _DeleteSubjectToolStripButton_Click(object sender, EventArgs e)
        {
            var tip = string.Format("功能主题 {1} 将要删除，您是否确认？\r\n({0})", _Tree.SelectedNode.Parent.Text, _Tree.SelectedNode.Text);
            if (MessageBox.Show(this, tip, "删除", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                var node = _Tree.SelectedNode as SubjectGroupTreeNode;
                if (node != null)
                {
                    var subject = (ScpiSubject) node.Tag;
                    subject.OwnerCollection.Remove(subject);
                    subject.OwnerCollection.Save();
                    UpdateTreeNodes();
                }
            }
            _ConfirmButton.Enabled = _Tree.SelectedNode != null;
        }

        private void _EditSubjectToolStripButton_Click(object sender, EventArgs e)
        {
            var node = _Tree.SelectedNode as SubjectGroupTreeNode;
            if (node != null)
            {
                var subject = (ScpiSubject) node.Tag;
                var dialog = new SubjectInfoDialog();
                dialog.GroupName = subject.Name;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    subject.Name = dialog.GroupName;
                    //subject.OwnerCollection.Add(subject);
                    subject.OwnerCollection.Save();
                    UpdateTreeNodes();
                    _Tree.SelectedNode = node;
                }
            }
            _ConfirmButton.Enabled = _Tree.SelectedNode != null;
        }

        private void _ExportButton_Click(object sender, EventArgs e)
        {
        }

        private void _ImportButton_Click(object sender, EventArgs e)
        {
        }
    }
}