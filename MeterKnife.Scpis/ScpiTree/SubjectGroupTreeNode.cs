using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Util.Scpi;

namespace MeterKnife.Scpis.ScpiTree
{
    public class SubjectGroupTreeNode : TreeNode
    {
        public SubjectGroupTreeNode(ScpiSubject scpiSubject)
            : this(scpiSubject.Name)
        {
        }

        public SubjectGroupTreeNode(string name)
            : base(name)
        {
            ImageKey = "subject-group";
            SelectedImageKey = "subject-group";
        }
    }
}
