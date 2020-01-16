using System.Drawing;
using System.Windows.Forms;
using MeterKnife.Util.Entities;

namespace MeterKnife.Util.GUI
{
    public class SexSelector : UserControl
    {
        private RadioButton _FemaleRadio;
        private RadioButton _MaleRadio;

        public SexSelector()
        {
            InitializeComponent();
        }

        public PersonSex Selected
        {
            get
            {
                if (_FemaleRadio.Checked)
                    return PersonSex.Female;
                if (_MaleRadio.Checked)
                    return PersonSex.Male;
                return PersonSex.None;
            }
            set
            {
                switch (value)
                {
                    case PersonSex.None:
                        _MaleRadio.Checked = false;
                        _FemaleRadio.Checked = false;
                        break;
                    case PersonSex.Male:
                        _MaleRadio.Checked = true;
                        break;
                    case PersonSex.Female:
                        _FemaleRadio.Checked = true;
                        break;
                }
            }
        }

        private void InitializeComponent()
        {
            _FemaleRadio = new RadioButton();
            _MaleRadio = new RadioButton();
            SuspendLayout();
            // 
            // radioButton2
            // 
            _FemaleRadio.AutoSize = true;
            _FemaleRadio.Location = new Point(42, 4);
            _FemaleRadio.Name = "_FemaleRadio";
            _FemaleRadio.Size = new Size(35, 16);
            _FemaleRadio.TabIndex = 10;
            _FemaleRadio.TabStop = true;
            _FemaleRadio.Text = "女";
            _FemaleRadio.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            _MaleRadio.AutoSize = true;
            _MaleRadio.Location = new Point(3, 4);
            _MaleRadio.Name = "_MaleRadio";
            _MaleRadio.Size = new Size(35, 16);
            _MaleRadio.TabIndex = 9;
            _MaleRadio.TabStop = true;
            _MaleRadio.Text = "男";
            _MaleRadio.UseVisualStyleBackColor = true;
            // 
            // SexSelector
            // 
            Controls.Add(_FemaleRadio);
            Controls.Add(_MaleRadio);
            Name = "SexSelector";
            Size = new Size(86, 22);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}