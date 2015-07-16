using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.GUI.WinForm;
using ScpiKnife;

namespace MeterKnife.Common.Scpi
{
    public partial class MeterScpiGroupTreeDialog : SimpleForm
    {
        public MeterScpiGroupTreeDialog()
        {
            InitializeComponent();
        }

        public ScpiSubject ScpiSubject
        {
            get
            {
                var treeNode = _Tree.SelectedNode;
                return (ScpiSubject) treeNode.Tag;
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            var path = Path.Combine(Application.StartupPath, "MeterInfos");
            var dir = new DirectoryInfo(path);
            var files = dir.GetFiles();
            foreach (var file in files)
            {
                ParseMeterFile(file);
            }
        }

        private void ParseMeterFile(FileInfo file)
        {
            var parser = new MeterInfoParser();
            List<ScpiSubject> list;
            if (parser.TryParse(file, out list))
            {
                var treeNode = new TreeNode(string.Format("{0}{1}", parser.Brand, parser.Name));
                foreach (var subject in list)
                {
                    var subNode = new TreeNode(subject.Description);
                    subNode.Tag = subject;
                    treeNode.Nodes.Add(subNode);
                }
                _Tree.Nodes.Add(treeNode);
            }
            _Tree.Nodes[0].Expand();
        }

        private void _CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void _ConfirmButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
