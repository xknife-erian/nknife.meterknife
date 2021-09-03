using System.Windows.Forms;
using NKnife.MeterKnife.Common.Scpi;

namespace NKnife.MeterKnife.Scpis.ScpiTree
{
    public class SubjectGroupTreeNode : TreeNode
    {
        public SubjectGroupTreeNode(ScpiCommandSubject scpiSubject)
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
