using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace NKnife.GUI.WinForm.ExceptionUI
{
    /// <summary>
    /// Provides a UserControl for displaying information using a Title, Description, and Image. Alternatively providing
    /// access to a scrolling MarqueeControl to provide visual feedback for long running processes.
    /// </summary>
    [DebuggerStepThrough]
    public sealed class InformationPanel : UserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private readonly Container _Components;

        private Label _LabelDescription;
        private Label _LabelTitle;
        private MarqueeControl _Marquee;
        private PictureBox _PictureBox;
        private ProgressBar _ProgressBar;

        /// <summary>
        /// Initializes a new instance of the InformationPanel class
        /// </summary>
        public InformationPanel()
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_Components != null)
                {
                    _Components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof (InformationPanel));
            this._LabelTitle = new System.Windows.Forms.Label();
            this._PictureBox = new System.Windows.Forms.PictureBox();
            this._LabelDescription = new System.Windows.Forms.Label();
            this._ProgressBar = new System.Windows.Forms.ProgressBar();
            this._Marquee = new MarqueeControl();
            ((System.ComponentModel.ISupportInitialize) (this._PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _labelTitle
            // 
            this._LabelTitle.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                             | System.Windows.Forms.AnchorStyles.Right)));
            this._LabelTitle.BackColor = System.Drawing.Color.Transparent;
            this._LabelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this._LabelTitle.Location = new System.Drawing.Point(10, 10);
            this._LabelTitle.Name = "_LabelTitle";
            this._LabelTitle.Size = new System.Drawing.Size(305, 20);
            this._LabelTitle.TabIndex = 0;
            // 
            // _pictureBox
            // 
            this._PictureBox.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._PictureBox.BackColor = System.Drawing.Color.Transparent;
            this._PictureBox.Location = new System.Drawing.Point(325, 10);
            this._PictureBox.Name = "_PictureBox";
            this._PictureBox.Size = new System.Drawing.Size(64, 64);
            this._PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._PictureBox.TabIndex = 1;
            this._PictureBox.TabStop = false;
            // 
            // _labelDescription
            // 
            this._LabelDescription.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                    | System.Windows.Forms.AnchorStyles.Left)
                                                                                   | System.Windows.Forms.AnchorStyles.Right)));
            this._LabelDescription.BackColor = System.Drawing.Color.Transparent;
            this._LabelDescription.Location = new System.Drawing.Point(30, 35);
            this._LabelDescription.Name = "_LabelDescription";
            this._LabelDescription.Size = new System.Drawing.Size(285, 58);
            this._LabelDescription.TabIndex = 2;
            // 
            // _progressBar
            // 
            this._ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                                                                              | System.Windows.Forms.AnchorStyles.Right)));
            this._ProgressBar.Enabled = false;
            this._ProgressBar.Location = new System.Drawing.Point(0, 80);
            this._ProgressBar.Name = "_ProgressBar";
            this._ProgressBar.Size = new System.Drawing.Size(400, 23);
            this._ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this._ProgressBar.TabIndex = 3;
            this._ProgressBar.Visible = false;
            // 
            // _marquee
            // 
            this._Marquee.BackColor = System.Drawing.Color.Transparent;
            this._Marquee.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._Marquee.FrameRate = 33;
            this._Marquee.ImageToScroll = ((System.Drawing.Image) (resources.GetObject("_marquee.ImageToScroll")));
            this._Marquee.IsScrolling = false;
            this._Marquee.Location = new System.Drawing.Point(0, 98);
            this._Marquee.Name = "_Marquee";
            this._Marquee.Size = new System.Drawing.Size(400, 5);
            this._Marquee.StepSize = 10;
            this._Marquee.TabIndex = 0;
            // 
            // InformationPanel
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this._ProgressBar);
            this.Controls.Add(this._Marquee);
            this.Controls.Add(this._LabelDescription);
            this.Controls.Add(this._PictureBox);
            this.Controls.Add(this._LabelTitle);
            this.Name = "InformationPanel";
            this.Size = new System.Drawing.Size(400, 103);
            ((System.ComponentModel.ISupportInitialize) (this._PictureBox)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        #region My Overrides

        /// <summary>
        /// Override the Refresh method to refresh the child controls 
        /// </summary>
        public override void Refresh()
        {
            base.Refresh();

            // refresh all of the child controls to prevent flickering
            // this should really be all GDI+ stuff if we wanted to be pure
            foreach (Control c in Controls)
                c.Refresh();
        }

        #endregion

        #region My Public Properties

        /// <summary>
        /// Gets or sets the Title displayed
        /// </summary>
        [Category("Appearance")]
        [Description("Gets or sets the Title displayed")]
        public string Title
        {
            get { return _LabelTitle.Text; }
            set
            {
                _LabelTitle.Text = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the description displayed
        /// </summary>		
        [Category("Appearance")]
        [Description("Gets or sets the description displayed by this InformationPanel")]
        public string Description
        {
            get { return _LabelDescription.Text; }
            set
            {
                _LabelDescription.Text = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the Image displayed
        /// </summary>		
        [Category("Appearance")]
        [Description("Gets or sets the Image displayed by this InformationPanel")]
        public Image Image
        {
            get { return _PictureBox.Image; }
            set
            {
                _PictureBox.Image = value;
                Refresh();
            }
        }

        /// <summary>
        /// Returns the PictureBox that contains the Image displayed
        /// </summary>
        [Category("Child Controls")]
        [Description("Returns the PictureBox that contains the Image displayed by the InformationPanel.")]
        public PictureBox ImagePictureBox
        {
            get { return _PictureBox; }
        }

        /// <summary>
        /// Returns the MarqueeControl displayed
        /// </summary>
        [Category("Child Controls")]
        [Description("Returns the MarqueeControl displayed at the bottom of this InformationPanel")]
        public MarqueeControl Marquee
        {
            get { return _Marquee; }
        }

        /// <summary>
        /// Returns the ProgressBar displayed
        /// </summary>
        [Category("Child Controls")]
        [Description("Returns the ProgressBar displayed at the bottom of this InformationPanel")]
        public ProgressBar ProgressBar
        {
            get { return _ProgressBar; }
        }

        #endregion
    }
}