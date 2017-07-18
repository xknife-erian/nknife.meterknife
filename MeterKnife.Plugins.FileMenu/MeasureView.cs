using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.IoC;
using WeifenLuo.WinFormsUI.Docking;
using IExtenderProvider = MeterKnife.Interfaces.Plugins.IExtenderProvider;

namespace MeterKnife.Plugins.FileMenu
{
    public partial class MeasureView : DockContent
    {
        private MeasureViewModel _ViewModel = new MeasureViewModel();

        public MeasureView()
        {
            InitializeComponent();
        }

        public void SetProvider(IExtenderProvider extenderProvider)
        {
            _ViewModel.SetProvider(extenderProvider);
        }
    }
}
