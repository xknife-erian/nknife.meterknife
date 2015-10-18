using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MeterKnife.Scpis.ScpiTree
{

    public class SubjectCollectionTreeNode : TreeNode
    {
        public SubjectCollectionTreeNode(string name)
            : base(name)
        {
            ImageKey = "subject-collection";
            SelectedImageKey = "subject-collection";
        }
    }
}
