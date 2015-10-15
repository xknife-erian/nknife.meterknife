using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Scpis;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Instruments.DockViews
{
    public partial class ScpisPanelView : DockContent
    {
        public ScpisPanelView()
        {
            InitializeComponent();
            var scpiPanel = new CustomerScpiSubjectPanel {Dock = DockStyle.Fill};
            Controls.Add(scpiPanel);
        }
    }
}
