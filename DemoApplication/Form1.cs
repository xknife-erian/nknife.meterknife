using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Instruments.Specified.Agilent;

namespace DemoApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var inst = new Ag34401A();
            _SplitContainer.Panel1.Controls.Add(inst.ParamPanel);
            _SplitContainer.Panel2.Controls.Add(inst.ParamPanel);
        }
    }
}
