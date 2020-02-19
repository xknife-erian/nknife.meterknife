using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.MeterKnife.Workbench.Properties;
using NKnife.Win.Quick.Controls;

namespace NKnife.MeterKnife.Workbench.Views
{
    public partial class InstrumentView : SingletonDockContent
    {
        public InstrumentView()
        {
            InitializeComponent();
            this.Res();
            _NewToolStripButton.Res();
            _NewToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _NewToolStripButton.Image = Resources.ints_add;
            _EditToolStripButton.Res();
            _EditToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _EditToolStripButton.Image = Resources.ints_edit;
            _DeleteToolStripButton.Res();
            _DeleteToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _DeleteToolStripButton.Image = Resources.ints_delete;
        }
    }
}
