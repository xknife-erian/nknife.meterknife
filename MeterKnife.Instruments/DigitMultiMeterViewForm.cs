using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Instruments.DockViews;
using NKnife.IoC;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Instruments
{
    public partial class DigitMultiMeterViewForm : MeterView
    {
        public DigitMultiMeterViewForm()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            var scpiPanelView = new ScpisPanelView();
            scpiPanelView.Show(_DockPanel, DockState.DockLeft);
        }
    }
}
