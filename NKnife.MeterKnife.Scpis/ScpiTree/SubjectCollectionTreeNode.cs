using System.Windows.Forms;
using NKnife.MeterKnife.Common.Scpi;

namespace NKnife.MeterKnife.Scpis.ScpiTree
{

    public class SubjectCollectionTreeNode : TreeNode
    {
        private readonly CareCommandSubjectList _collection;

        public SubjectCollectionTreeNode(CareCommandSubjectList collection)
            : this(string.Format("{0}{1}", collection.Brand, collection.Name))
        {
            _collection = collection;
        }

        public SubjectCollectionTreeNode(string name)
            : base(name)
        {
            ImageKey = "subject-collection";
            SelectedImageKey = "subject-collection";
        }

        public CareCommandSubjectList GetScpiSubjectCollection()
        {
            return _collection;
        }
    }
}
