using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace NKnife.GUI.WinForm.GroupboxPro
{
    /// <summary>
    /// CheckGroupBox is a GroupBox with an embeded CheckBox.
    /// </summary>
    public partial class CheckGroupBox : GroupBox
    {
        // Constants
        private const int CHECKBOX_X_OFFSET = 10;
        private const int CHECKBOX_Y_OFFSET = 0;

        // Members
        private bool _DisableChildrenIfUnchecked;

        /// <summary>
        /// CheckGroupBox public constructor.
        /// </summary>
        public CheckGroupBox()
        {
            InitializeComponent();
            _DisableChildrenIfUnchecked = true;
            _CheckBox.Parent = this;
            _CheckBox.Location = new Point(CHECKBOX_X_OFFSET, CHECKBOX_Y_OFFSET);
            Checked = true;

            // Set the color of the CheckBox's text to the color of the label in a standard groupbox control.
            var vsr = new VisualStyleRenderer(VisualStyleElement.Button.GroupBox.Normal);
            Color groupBoxTextColor = vsr.GetColor(ColorProperty.TextColor);
            _CheckBox.ForeColor = groupBoxTextColor;
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
                    return _CheckBox.Text;
                }
                // Run-time
                return " "; // Set the text of the GroupBox to a space, so the gap appears before the CheckBox.
            }
            set
            {
                base.Text = " "; // Set the text of the GroupBox to a space, so the gap appears before the CheckBox.
                _CheckBox.Text = value;
            }
        }

        /// <summary>
        /// Indicates whether the component is checked or not.
        /// </summary>
        [Description("Indicates whether the component is checked or not.")]
        [Category("Appearance")]
        [DefaultValue(true)]
        public bool Checked
        {
            get { return _CheckBox.Checked; }
            set
            {
                if (_CheckBox.Checked != value)
                {
                    _CheckBox.Checked = value;
                }
            }
        }

        /// <summary>
        /// Indicates the state of the component.
        /// </summary>
        [Description("Indicates the state of the component.")]
        [Category("Appearance")]
        [DefaultValue(CheckState.Checked)]
        public CheckState CheckState
        {
            get { return _CheckBox.CheckState; }
            set
            {
                if (_CheckBox.CheckState != value)
                {
                    _CheckBox.CheckState = value;
                }
            }
        }

        /// <summary>
        /// Determines if child controls of the GroupBox are disabled when the CheckBox is unchecked.
        /// </summary>
        [Description("Determines if child controls of the GroupBox are disabled when the CheckBox is unchecked.")]
        [Category("Appearance")]
        [DefaultValue(true)]
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
        /// Occurs whenever the Checked property of the CheckBox is changed.
        /// </summary>
        [Description("Occurs whenever the Checked property of the CheckBox is changed.")]
        public event EventHandler CheckedChanged;

        /// <summary>
        /// Occurs whenever the CheckState property of the CheckBox is changed.
        /// </summary>
        [Description("Occurs whenever the CheckState property of the CheckBox is changed.")]
        public event EventHandler CheckStateChanged;

        /// <summary>
        /// Raises the System.Windows.Forms.CheckBox.checkBox_CheckedChanged event.
        /// </summary>
        /// <param name="e">An System.EventArgs that contains the event data.</param>
        protected virtual void OnCheckedChanged(EventArgs e)
        {
            if (CheckedChanged != null)
                CheckedChanged(this, e);
        }

        /// <summary>
        /// Raises the System.Windows.Forms.CheckBox.CheckStateChanged event.
        /// </summary>
        /// <param name="e">An System.EventArgs that contains the event data.</param>
        protected virtual void OnCheckStateChanged(EventArgs e)
        {
            if (CheckStateChanged != null)
                CheckStateChanged(this, e);
        }

        #endregion Event Handlers

        #region Events

        private void CheckBoxCheckedChanged(object sender, EventArgs e)
        {
            if (_DisableChildrenIfUnchecked)
            {
                bool bEnabled = _CheckBox.Checked;
                foreach (Control control in Controls)
                {
                    if (control != _CheckBox)
                        control.Enabled = bEnabled;
                }
            }

            if (CheckedChanged != null)
                CheckedChanged(sender, e);
        }

        private void CheckBoxCheckStateChanged(object sender, EventArgs e)
        {
            if (CheckStateChanged != null)
                CheckStateChanged(sender, e);
        }

        private void CheckGroupBoxControlAdded(object sender, ControlEventArgs e)
        {
            if (_DisableChildrenIfUnchecked)
                e.Control.Enabled = Checked;
        }

        #endregion Events
    }
}