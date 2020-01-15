using System.Collections.Generic;
using System.Drawing;

namespace NKnife.GUI.WinForm.CloudsControl
{

    public class CloudCollection : IList<CloudLabel>
    {
        private List<CloudLabel> _labelList = new List<CloudLabel>();

        public CloudCollection()
        {

        }

        /// <summary>
        /// 返回计算出的总高度
        /// </summary>
        /// <returns></returns>
        public float GetHeight()
        {
            if (_labelList.Count <= 0)
            {
                return 1;
            }
            RectangleF rect = new RectangleF(_labelList[Count - 1].Location, _labelList[Count - 1].Size);
            return rect.Y + rect.Height;
        }

        #region AddRange, ToArray, TrimExcess

        public void AddRange(IEnumerable<CloudLabel> clouds)
        {
            _labelList.AddRange(clouds);
        }

        public CloudLabel[] ToArray()
        {
            return _labelList.ToArray();
        }

        public void TrimExcess()
        {
            _labelList.TrimExcess();
        }

        #endregion

        #region IList<CloudLabel>

        public int IndexOf(CloudLabel item)
        {
            return _labelList.IndexOf(item);
        }

        public void Insert(int index, CloudLabel item)
        {
            _labelList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _labelList.Remove(_labelList[index]);
        }

        public CloudLabel this[int index]
        {
            get { return _labelList[index]; }
            set { _labelList[index] = value; }
        }

        #endregion

        #region ICollection<CloudLabel>

        public void Add(CloudLabel item)
        {
            Insert(Count, item);
        }

        public void Clear()
        {
            _labelList.Clear();
        }

        public bool Contains(CloudLabel item)
        {
            return _labelList.Contains(item);
        }

        public void CopyTo(CloudLabel[] array, int arrayIndex)
        {
            _labelList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _labelList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(CloudLabel item)
        {
            bool bl = _labelList.Remove(item);
            return bl;
        }

        #endregion

        #region IEnumerable<CloudLabel>

        public IEnumerator<CloudLabel> GetEnumerator()
        {
            return _labelList.GetEnumerator();
        }

        #endregion

        #region IEnumerable

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _labelList.GetEnumerator();
        }

        #endregion
    }

}
