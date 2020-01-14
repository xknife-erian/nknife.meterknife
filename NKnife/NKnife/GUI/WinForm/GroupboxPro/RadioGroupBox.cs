using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace NKnife.GUI.WinForm.GroupboxPro
{
    /// <summary>
    /// RadioGroupBox is a GroupBox with an embeded RadioButton.
    /// </summary>
    public partial class RadioGroupBox : GroupBox
    {
        // Constants
        private const int RADIOBUTTON_X_OFFSET = 10;
        private const int RADIOBUTTON_Y_OFFSET = -2;

        // Members
        private bool _DisableChildrenIfUnchecked;

        /// <summary>
        /// RadioGroupBox public constructor.
        /// </summary>
        public RadioGroupBox()
        {
            InitializeComponent();
            _DisableChildrenIfUnchecked = false;
            _RadioButton.Parent = this;
            _RadioButton.Location = new Point(RADIOBUTTON_X_OFFSET, RADIOBUTTON_Y_OFFSET);
            Checked = false;

            // Set the color of the RadioButon's text to the color of the label in a standard groupbox control.
            var vsr = new VisualStyleRenderer(VisualStyleElement.Button.GroupBox.Normal);
            Color groupBoxTextColor = vsr.GetColor(ColorProperty.TextColor);
            _RadioButton.ForeColor = groupBoxTextColor;
        }

        #region Properties

        /// <summary>
        /// The text associated with the control.
        /// </summary>
        public override string Text
        {
            get
            {
                if (Site != null && Site.DesignMode)
                {
                    // Design-time mode
                    return _RadioButton.Text;
                }
                else
                {
                    // Run-time
                    return " "; // Set the text of the GroupBox to a space, so the gap appears before the RadioButton.
                }
            }
            set
            {
                base.Text = " "; // Set the text of the GroupBox to a space, so the gap appears before the RadioButton.
                _RadioButton.Text = value;
            }
        }

        /// <summary>
        /// Indicates whether the radio button is checked or not.
        /// </summary>
        [Description("Indicates whether the radio button is checked or not.")]
        [Category("Appearance")]
        [DefaultValue(false)]
        public bool Checked
        {
            get { return _RadioButton.Checked; }
            set
            {
                if (_RadioButton.Checked != value)
                {
                    _RadioButton.Checked = value;
                }
            }
        }

        /// <summary>
        /// Determines if child controls of the GroupBox are disabled when the CheckBox is unchecked.
        /// </summary>
        [Description("Determines if child controls of the GroupBox are disabled when the RadioButton is unchecked.")]
        [Category("Appearance")]
        [DefaultValue(false)]
        public bool DisableChildrenIfUnchecked
        {
            get { return _DisableChildrenIfUnchecked; }
            set
            {
                if (_DisableChildrenIfUnchecked != value)
                {
                    _DisableChildrenIfUnchecked = value;
                }
            }
        }

        #endregion Properties

        #region Event Handlers

        /// <summary>
        /// Occurs when the 'checked' property changes value.
        /// </summary>
        [Description("Occurs when the 'checked' property changes value.")]
        public event EventHandler CheckedChanged;

        //
        // Summary:
        //     Raises the System.Windows.Forms.RadioButton.checkBox_CheckedChanged event.
        /// <summary>
        /// Raises the System.Windows.Forms.
        /// </summary>
        /// <param name="e">An System.EventArgs that contains the event data.</param>
        protected virtual void OnCheckedChanged(EventArgs e)
        {
        }

        #endregion Event Handlers

        #region Events

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;

            var target = radioButton.Parent as RadioGroupBox;
            if (target == null)
                return;

            if (_DisableChildrenIfUnchecked)
            {
                bool bEnabled = _RadioButton.Checked;
                foreach (Control control in Controls)
                {
                    if (control != _RadioButton)
                    {
                        control.Enabled = bEnabled;
                    }
                }
            }

            if (target.Checked == false)
                return;

            Control parentControl = target.Parent;
            if (parentControl == null)
                return;

            foreach (Control childControl in parentControl.Controls)
            {
                if (childControl is RadioGroupBox)
                {
                    if (childControl != this)
                    {
                        (childControl as RadioGroupBox).Checked = false;
                    }
                }
            }

            if (CheckedChanged != null)
            {
                CheckedChanged(sender, e);
            }
        }

        private void CheckGroupBox_ControlAdded(object sender, ControlEventArgs e)
        {
            if (_DisableChildrenIfUnchecked)
            {
                e.Control.Enabled = Checked;
            }
        }

        #endregion Events
    }
}