using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.UITests
{
    public partial class PropertyGridView : DockContent
    {
        public PropertyGridView()
        {
            InitializeComponent();
        }

        public void SetObject1(object obj)
        {
            _TextBox1.Text = obj.GetType().Name;
            _PropertyGrid1.SelectedObject = obj;
        }

        public void SetObject2(object obj)
        {
            _TextBox2.Text = obj.GetType().Name;
            _PropertyGrid2.SelectedObject = obj;
        }
    }
}
