using System;
using System.IO;
using System.Windows.Forms;
using NKnife.GUI.WinForm;
using NKnife.IoC;
using ScpiKnife;

namespace MeterKnife.Scpis
{
    public partial class InstrumentScpiGroupTreeDialog : SimpleForm
    {
        public InstrumentScpiGroupTreeDialog()
        {
            InitializeComponent();
            _ConfirmButton.Enabled = false;
            _Tree.AfterSelect += (s, e) =>
            {
                if (_Tree.SelectedNode is SubjectTreeNode)
                    _ConfirmButton.Enabled = true;
                else
                    _ConfirmButton.Enabled = false;
            };
        }

        public ScpiSubject ScpiSubject
        {
            get
            {
                var treeNode = _Tree.SelectedNode;
                return (ScpiSubject) treeNode.Tag;
            }
        }

        public string CurrentMeter { get; private set; }

        public string CurrentDescription { get; private set; }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            var dir = new DirectoryInfo(ScpiUtil.ScpisPath);
            var files = dir.GetFiles("*.xml", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                ParseMeterFile(file);
            }
        }

        private void ParseMeterFile(FileInfo file)
        {
            ScpiSubjectCollection collection = new ScpiSubjectCollection();
            collection.BuildScpiFile(file.FullName);
            if (collection.TryParse(null))
            {
                var meter = string.Format("{0}{1}", collection.Brand, collection.Name);
                var treeNode = new TreeNode(meter);
                foreach (var subject in collection)
                {
                    var subNode = new SubjectTreeNode
                    {
                        Text = subject.Description,
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
            CurrentDescription = _Tree.SelectedNode.Text;
            CurrentMeter = _Tree.SelectedNode.Parent.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        public class SubjectTreeNode : TreeNode
        {
        }
    }
}
