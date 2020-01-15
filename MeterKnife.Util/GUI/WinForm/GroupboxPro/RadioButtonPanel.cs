using System;
using System.Windows.Forms;

namespace NKnife.GUI.WinForm.GroupboxPro
{
    /// <summary>
    /// This class oversees the checkin/unchecking when mixing RadioButton objects with RadioGroupBox objects within the same container.
    /// </summary>
    public class RadioButtonPanel : Panel
    {
        /// <summary>
        /// Hooks Check callback events of RadioButton objects within the panel to the RadioButtonPanel object.
        /// </summary>
        public void AddCheckEventListeners()
        {
            foreach (Control control in Controls)
            {
                var radioButton = control as RadioButton;
                if (radioButton != null)
                {
                    radioButton.CheckedChanged += RadioButtonCheckedChanged;
                }
                else
                {
                    var radioGroupBox = control as RadioGroupBox;
                    if (radioGroupBox != null)
                    {
                        radioGroupBox.CheckedChanged += RadioGroupBoxCheckedChanged;
                    }
                }
            }
        }

        /// <summary>
        /// Event callback called when a RadioButton's Check property is changed.
        /// </summary>
        /// <param name="sender">Object(RadioButton)</param>
        /// <param name="e">EventArgs</param>
        private void RadioButtonCheckedChanged(object sender, EventArgs e)
        {
            HandleRadioButtonClick((Control) sender);
        }

        /// <summary>
        /// Event callback called when a RadioGroupBox's Check property is changed.
        /// </summary>
        /// <param name="sender">Object(RadioGroupBox)</param>
        /// <param name="e">EventArgs</param>
        private void RadioGroupBoxCheckedChanged(object sender, EventArgs e)
        {
            HandleRadioButtonClick((Control) sender);
        }

        private void HandleRadioButtonClick(Control clickedControl)
        {
            if (clickedControl == null)
                return;

            if (clickedControl.Parent is RadioGroupBox)
            {
                // If a RadioGroupBox is checked, the sender is the RadioButton,
                // not the RadioGroupBox, but we need the RadioGroupBox object.
                clickedControl = clickedControl.Parent;
            }

            var clickedRadioButton = clickedControl as RadioButton;
            if (clickedRadioButton != null)
            {
                if (clickedRadioButton.Checked != true)
                {
                    // Only respond to check events that pertain to the control being checked on
                    return;
                }
            }
            else
            {
                var clickedRadioGroupBox = clickedControl as RadioGroupBox;
                if (clickedRadioGroupBox != null)
                {
                    if (clickedRadioGroupBox.Checked != true)
                    {
                        // Only respond to check events that pertain to the control being checked on
                        return;
                    }
                }
            }

            foreach (Control control in Controls)
            {
                if (control != clickedControl)
                {
                    var radioButton = control as RadioButton;
                    if (radioButton != null)
                    {
                        // Normally .NET and WinForms would take care of this but
                        // we need a mechanism that turns off radio buttons if a
                        // radio group box is checked.
                        if (radioButton.Checked)
                            radioButton.Checked = false;
                    }
                    else
                    {
                        var radioGroupBox = control as RadioGroupBox;
                        if (radioGroupBox != null)
                        {
                            radioGroupBox.Checked = false;
                        }
                    }
                }
            }
        }
    }
}