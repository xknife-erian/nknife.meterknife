using System;
using System.Drawing;
using System.Windows.Forms;

namespace MeterKnife.Util.GUI
{
    public class TouchNumberBoxForm : Form
    {
        private ImageButton _AddButton;
        private ImageButton _SubtractButton;

        public TouchNumberBoxForm()
        {
            InitializeComponent();
            _AddButton.Click += AddButtonClick;
            _SubtractButton.Click += SubtractButtonClick;
        }

        public event EventHandler AddEvent;

        public event EventHandler SubtractEvent;

        protected virtual void OnAdd()
        {
            if (AddEvent != null)
                AddEvent(this, EventArgs.Empty);
        }

        protected virtual void OnSubtract()
        {
            if (SubtractEvent != null)
                SubtractEvent(this, EventArgs.Empty);
        }

        private void AddButtonClick(object sender, EventArgs e)
        {
            OnAdd();
        }

        private void SubtractButtonClick(object sender, EventArgs e)
        {
            OnSubtract();
        }

        #region Windows Form Designer

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            _SubtractButton = new ImageButton();
            _AddButton = new ImageButton();
            SuspendLayout();
            // 
            // _AddButton
            // 
            _AddButton.ButtonColor = Color.SlateGray;
            _AddButton.Location = new Point(0, 0);
            _AddButton.Margin = new Padding(0);
            _AddButton.Name = "_AddButton";
            _AddButton.Size = new Size(45, 45);
            _AddButton.Style = ImageButton.ImageButtonStyle.Add;
            _AddButton.TabIndex = 2;
            _AddButton.TabStop = false;
            _AddButton.UseVisualStyleBackColor = true;
            // 
            // _SubtractButton
            // 
            _SubtractButton.ButtonColor = Color.SlateGray;
            _SubtractButton.Location = new Point(45, 0);
            _SubtractButton.Margin = new Padding(0);
            _SubtractButton.Name = "_SubtractButton";
            _SubtractButton.Size = new Size(45, 45);
            _SubtractButton.Style = ImageButton.ImageButtonStyle.Subtract;
            _SubtractButton.TabIndex = 3;
            _SubtractButton.TabStop = false;
            _SubtractButton.UseVisualStyleBackColor = true;
            // 
            // TouchNumberBoxForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(90, 45);
            ControlBox = false;
            Controls.Add(_SubtractButton);
            Controls.Add(_AddButton);
            Font = new Font("Tahoma", 8.25F);
            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.Gray;
            TransparencyKey = Color.Gray;
            Name = "TouchNumberBoxForm";
            StartPosition = FormStartPosition.Manual;
            ResumeLayout(false);
        }

        #endregion
    }
}