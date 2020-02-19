using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NKnife.MeterKnife.Workbench.Controls
{
    public class EngineeringTreeNode : TreeNode
    {
        public EngineeringTreeNode(string text) 
            : base(text)
        {
            this.ImageKey = nameof(EngineeringTreeNode);
        }
    }

    public class EngineeringCreateTimeTreeNode : TreeNode
    {
        public EngineeringCreateTimeTreeNode(string text) 
            : base(text)
        {
            this.ImageKey = nameof(EngineeringCreateTimeTreeNode);
        }
    }
}
