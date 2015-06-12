using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Fairy
{
    public partial class FairyForm : Form
    {
        private readonly DockPanel _DockPanel = new DockPanel();

        public FairyForm()
        {
            InitializeComponent();
            InitializeDockPanel();
        }

        private void InitializeDockPanel()
        {
            _StripContainer.ContentPanel.Controls.Add(_DockPanel);

            _DockPanel.DocumentStyle = DocumentStyle.DockingWindow;
            _DockPanel.Dock = DockStyle.Fill;
            _DockPanel.BringToFront();

//            var view = new DigitMultiMeterView();
//            view.Show(_DockPanel, DockState.Document);
        }
    }
}
