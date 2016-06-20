using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Scpis.Properties;

namespace MeterKnife.Scpis.ScpiTree
{
    public class SubjectFileTree : TreeView
    {
        public SubjectFileTree()
        {
            SetStyle(
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint, true);
            GetImageList();
            ShowLines = false;
            FullRowSelect = true;
            ItemHeight = 22;
        }

        private void GetImageList()
        {
            ImageList = new ImageList
            {
                ColorDepth = ColorDepth.Depth32Bit,
                ImageSize = new Size(16, 16)
            };
            ImageList.Images.Add("subject-collection", Resources.subject_collection);
            ImageList.Images.Add("subject-group", Resources.subject_group);
        }
    }
}
