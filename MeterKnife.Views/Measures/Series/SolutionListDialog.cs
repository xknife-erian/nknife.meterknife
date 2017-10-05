using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Models;
using NKnife.ControlKnife;

namespace MeterKnife.Views.Measures.Series
{
    public partial class SolutionListDialog : SimpleForm
    {
        public SolutionListDialog(bool isSave)
        {
            InitializeComponent();
            _NameTextBox.Enabled = isSave;
        }

        public PlotSeriesStyleSolution Solution { get; set; }
        public string SolutionName { get; set; }
    }
}
