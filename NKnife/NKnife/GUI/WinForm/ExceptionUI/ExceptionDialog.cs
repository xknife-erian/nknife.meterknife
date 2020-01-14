using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace NKnife.GUI.WinForm.ExceptionUI
{
    /// <summary>
    /// Provides a common dialog for displaying an Exception to the user.
    /// </summary>
    public sealed partial class ExceptionDialog : Form
    {
        private Exception _Exception;
        private string _SubmitUrl;

        /// <summary>
        /// Initializes a new instance of the ExceptionDialog class.
        /// </summary>
        public ExceptionDialog()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;
            AcceptButton = _buttonOK;
            KeyPreview = true;

            _textBox.ScrollBars = ScrollBars.Both;
            _textBox.WordWrap = false;
            _textBox.ReadOnly = true;

            _richTextBox.ReadOnly = true;
            _richTextBox.BackColor = SystemColors.Window;

            _buttonOK.Enabled = true;
            _buttonOK.Click += OnButtonOkClick;

            linkLabelSendErrorReport.Visible = false;
            linkLabelSendErrorReport.Enabled = false;

            buttonCopy.Visible = false;

            linkLabelSendErrorReport.Click += OnLinkLabelSendErrorReportClick;
        }

        /// <summary>
        /// Initializes a new instance of the ExceptionDialog class.
        /// </summary>
        /// <param name="ex"></param>
        public ExceptionDialog(Exception ex)
            : this()
        {
            _Exception = ex;

            if (_Exception != null) DisplayException(ref _Exception);
        }

        /// <summary>
        /// Gets or sets the exception that is displayed.
        /// </summary>
        public Exception Exception
        {
            get { return _Exception; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value", "The ExceptionDialog cannot display the details of an exception object that is null.");

                _Exception = value;

                DisplayException(ref _Exception);
            }
        }

        public string SubmitUrl
        {
            get { return _SubmitUrl; }
            set
            {
                _SubmitUrl = value;

                linkLabelSendErrorReport.Visible = !string.IsNullOrEmpty(_SubmitUrl);
            }
        }

        private InformationPanel InformationPanel
        {
            get { return _informationPanel; }
        }

        public Image InformationPanelImage
        {
            get { return InformationPanel.Image; }

            set { InformationPanel.Image = value; }
        }

        private string MessageAsText
        {
            get { return _richTextBox.Text; }
            set { _richTextBox.Text = value; }
        }

        private string MessageAsRtf
        {
            get { return _richTextBox.Rtf; }
            set
            {
                try
                {
                    _richTextBox.Rtf = value;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    ShowDialog(this, ex);
                }
            }
        }

        private string StackTrace
        {
            get { return _textBox.Text; }
            set { _textBox.Text = value; }
        }

        private object SelectedObject
        {
            get { return _propertyGrid.SelectedObject; }
            set { _propertyGrid.SelectedObject = value; }
        }

        protected override void OnShown(EventArgs e)
        {
            try
            {
                base.OnShown(e);

                CenterToParent();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Control && e.KeyCode == Keys.C)
            {
                CopyExceptionToClipboard();
            }
        }

        private void CopyExceptionToClipboard()
        {
            if (_Exception == null)
                return;

            Clipboard.SetText(_Exception.ToString());
        }

        /// <summary>
        /// Displays the details fo the exception using the embedded web browser control and Html.
        /// </summary>
        /// <param name="ex"></param>
        private void DisplayException(ref Exception ex)
        {
            string title = null;

            buttonCopy.Visible = ex != null || !string.IsNullOrEmpty(MessageAsText);

            if (string.IsNullOrEmpty(title))
                title = "Exception Encountered";

            _informationPanel.Title = title;

            if (_tabControl.TabPages.Count > 1)
                _informationPanel.Description = "Please refer to the tabs below for more information.";
            else
                _informationPanel.Description = "Please see below for more information.";

            if (!string.IsNullOrEmpty(ex.Message.Trim()))
                MessageAsText = ex.Message;
            StackTrace = ex.ToString();
            SelectedObject = ex;
        }

        private void RemoveStackTraceTab()
        {
            if (_tabControl.TabPages.Contains(_tabPageStackTrace))
                _tabControl.TabPages.Remove(_tabPageStackTrace);
        }

        private void RemovePropertiesTab()
        {
            if (_tabControl.TabPages.Contains(_tabPageProperties))
                _tabControl.TabPages.Remove(_tabPageProperties);
        }

        /// <summary>
        /// Show's the ExceptionDialog modally while displaying the details of the exception.
        /// </summary>
        /// <param name="ex"></param>
        public static void ShowDialog(Exception ex)
        {
            try
            {
                using (var dialog = new ExceptionDialog(ex))
                {
                    dialog.ShowDialog();
                }
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
            }
        }

        /// <summary>
        /// Show's the ExceptionDialog modally while displaying the details of the exception.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="ex"></param>
        public static void ShowDialog(Form owner, Exception ex)
        {
            if (owner == null || owner.IsDisposed)
            {
                ShowDialog(ex);
                return;
            }

            if (owner.InvokeRequired)
            {
                owner.Invoke(new ShowDialogDelegate(ShowDialog), owner, ex);
                return;
            }

            using (var dialog = new ExceptionDialog(ex))
            {
                dialog.ShowDialog(owner);
            }
        }

        public static void ShowDialog(IWin32Window owner, Exception ex)
        {
            if (ActiveForm != null)
            {
                ShowDialog(ActiveForm, ex);
                return;
            }

            using (var dialog = new ExceptionDialog(ex))
            {
                dialog.ShowDialog(owner);
            }
        }

        public static void ShowDialog(Control owner, Exception ex)
        {
            Form form = null;

            if (owner != null && !owner.IsDisposed)
            {
                form = owner.FindForm();
            }

            ShowDialog(form, ex);
        }

        #region Control Events

        /// <summary>
        /// Occurs when the Ok button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonOkClick(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                ShowDialog(this, ex);
            }
        }

        private void OnLinkLabelSendErrorReportClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(SubmitUrl))
                    return;

                Process.Start(SubmitUrl);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                ShowDialog(this, ex);
            }
        }

        private void ButtonCopyClick(object sender, EventArgs e)
        {
            try
            {
                CopyExceptionToClipboard();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                ShowDialog(this, ex);
            }
        }

        #endregion

        #region Nested type: ShowDialogDelegate

        private delegate void ShowDialogDelegate(Form owner, Exception ex);

        #endregion
    }
}