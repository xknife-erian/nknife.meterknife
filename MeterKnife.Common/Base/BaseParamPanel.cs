using System.Windows.Forms;
using MeterKnife.Common.DataModels;

namespace MeterKnife.Common.Base
{
    public abstract partial class BaseParamPanel : UserControl
    {
        protected BaseParamPanel()
        {
            InitializeComponent();
        }

        public abstract GpibCommandList GpibCommands { get; }

        /*
            _Panel.ColumnCount = 2;
            _Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            _Panel.RowCount = 2;
            _Panel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
         */
    }
}
