using System.Windows.Forms;
using NKnife.MeterKnife.Common.Scpi;

namespace NKnife.MeterKnife.Workbench.Controls
{
    public partial class ScpiDetailPanel : UserControl
    {
        private Scpi _scpi;

        public ScpiDetailPanel()
        {
            InitializeComponent();
            InitializeLanguage();
        }

        public Scpi Scpi
        {
            get
            {
                if (_scpi == null)
                    _scpi = new Scpi();
                _scpi.Name = _ScpiNameTextBox.Text;
                _scpi.Command = _CommandTextBox.Text;
                _scpi.Description = _ScpiDescriptionTextBox.Text;
                return _scpi;
            }
            set
            {
                _scpi = value;
                _ScpiNameTextBox.Text = _scpi.Name;
                _CommandTextBox.Text = _scpi.Command;
                _ScpiDescriptionTextBox.Text = _scpi.Description;
            }
        }

        private void InitializeLanguage()
        {
            this.Res(label1, label2, label3, label4);
        }

        public bool VerifyControlValue(out Control ctr, out string msg)
        {
            ctr = null;
            msg = string.Empty;
            if (string.IsNullOrEmpty(_ScpiNameTextBox.Text))
            {
                ctr = _ScpiNameTextBox;
                msg = this.Res("SCPI助记名未填写");
                return false;
            }
            if (_CommandTextBox.IsEmptyText())
            {
                ctr = _CommandTextBox;
                msg = this.Res("SCPI指令未填写");
                return false;
            }
            return true;
        }
    }
}