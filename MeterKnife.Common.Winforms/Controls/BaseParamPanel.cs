using System.Windows.Forms;
using MeterKnife.Util.Scpi;

namespace MeterKnife.Common.Winforms.Controls
{
    public partial class BaseParamPanel : UserControl
    {
        protected BaseParamPanel()
        {
            InitializeComponent();
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            Dock = DockStyle.Fill;
        }

        public virtual ScpiGroup ScpiCommands { get; protected set; }

        /*
            _Panel.ColumnCount = 2;
            _Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            _Panel.RowCount = 2;
            _Panel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
         */
    }
}
