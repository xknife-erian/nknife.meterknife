using System.Windows.Forms;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Winforms.Dialogs;

namespace MeterKnife.Common.Winforms.Controls.Tree
{
    public abstract class BaseTreeNode : TreeNode
    {
        protected readonly IMeterKernel _meterKernel;

        protected BaseTreeNode(IMeterKernel meterKernel)
        {
        }

//        public virtual void SetTag(IProjectItem item)
//        {
//            Text = item != null ? item.Name : string.Empty;
//            Tag = item;
//            if (item != null)
//            {
//                item.NameChanged += (s, e) => { Text = e.NewItem; };
//            }
//
//            if (Parent!= null)
//            {
//                var parentItem = ((IProjectItem)Parent.Tag);
//                parentItem.Items.Add(item);
//            }
//        }
    }
}
