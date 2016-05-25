using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.Electronics;

namespace MeterKnife.Tools.InverseProportionOperationAmplifier
{
    public partial class ResistanceControl : UserControl
    {
        public ResistanceControl()
        {
            InitializeComponent();
        }

        public ResistanceControl(Resistance.Unit unit):this()
        {
            _UnitLabel.Text = unit.ToString();
        }

        public decimal Value { get { return _ValueBox.Value; } }
    }
}
