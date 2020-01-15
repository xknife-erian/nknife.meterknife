using System.Collections.Generic;

namespace NKnife.GUI.WinForm.AutoLayoutPanel
{
    //internal class AutoLayoutPanelAttPairComparer : IComparer<AutoAttributeData>
    //{
    //    static IComparer<AutoAttributeData> Comparer = null;
    //    static public IComparer<AutoAttributeData> CreateComparer()
    //    {
    //        if (Comparer == null)
    //        {
    //            Comparer = new AutoLayoutPanelAttPairComparer();
    //        }
    //        return Comparer;
    //    }

    //    private AutoLayoutPanelAttPairComparer() { }

    //    public int Compare(
    //        AutoAttributeData x,
    //        AutoAttributeData y)
    //    {
    //        return x.Attribute.GroupBoxIndex - y.Attribute.GroupBoxIndex;
    //    }
    //}
    //实现比较两个对象的方法 by lisuye on 2008年5月29日
    internal class AutoLayoutPanelCtrPairComparer : IComparer<KeyValuePair<int, ValueControl>>
    {
        static IComparer<KeyValuePair<int, ValueControl>> Comparer = null;
        static public IComparer<KeyValuePair<int, ValueControl>> CreateComparer()
        {
            if (Comparer == null)
            {
                Comparer = new AutoLayoutPanelCtrPairComparer();
            }
            return Comparer;
        }

        private AutoLayoutPanelCtrPairComparer() { }

        public int Compare(
            KeyValuePair<int, ValueControl> x,
            KeyValuePair<int, ValueControl> y)
        {
            return x.Key - y.Key;
        }
    }
}
