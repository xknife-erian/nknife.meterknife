using System.Windows.Forms;
using NKnife.MeterKnife.Common.Scpi;

namespace NKnife.MeterKnife.Scpis.ScpiTree
{

    public class SubjectCollectionTreeNode : TreeNode
    {
        private readonly ScpiCommandSubjectList _collection;

        public SubjectCollectionTreeNode(ScpiCommandSubjectList collection)
            : this($"{collection.Brand}{collection.Name}")
        {
            _collection = collection;
        }

        public SubjectCollectionTreeNode(string name)
            : base(name)
        {
            ImageKey = "subject-collection";
            SelectedImageKey = "subject-collection";
        }

        public ScpiCommandSubjectList GetScpiSubjectCollection()
        {
            return _collection;
        }
    }
}
