using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            _EditToolStripButton.Res();
            _DeleteToolStripButton.Res();
        }
    }
}
