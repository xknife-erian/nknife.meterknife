using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace NKnife.GUI.WinForm
{
    /// <summary>
    /// Provides a UserControl that can scroll an Image for visualizing progress.
    /// </summary>	
    public sealed class MarqueeControl : UserControl
    {
        private readonly ManualResetEvent _StopScrolling;
        private int _FrameRate;
        private Image _Image;
        private bool _IsScrolling;
        private int _Offset;
        private int _Step;

        /// <summary>
        /// Initializes a new instance of the MargqueeControl class
        /// </summary>
        public MarqueeControl()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            LoadDefaultImage();
            StepSize = 10;
            FrameRate = 33;
            _StopScrolling = new ManualResetEvent(false);
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                IsScrolling = false;

                if (_Image != null)
                {
                    _Image.Dispose();
                    _Image = null;
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
            // 
            // MarqueeControl
            // 
            this.Name = "MarqueeControl";
            this.Size = new System.Drawing.Size(150, 10);
        }

        #endregion

        #region My Overrides

        /// <summary>
        /// Override the default painting, to scroll out image across the control
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // clear the background using our backcolor first
            e.Graphics.Clear(BackColor);

            lock (this)
            {
                // do we have an image to scroll?
                if (_Image != null)
                {
                    // if it's not in design mode
                    if (DesignMode)
                    {
                        e.Graphics.DrawImage(_Image, 0, 0, Width, Height);
                    }
                    else
                    {
                        // get the image bounds
                        GraphicsUnit gu = GraphicsUnit.Pixel;
                        RectangleF rcImage = _Image.GetBounds(ref gu);

                        // calculate the width ratio
                        float ratio = (rcImage.Width/Width);

                        var rcDstRight = new RectangleF(_Offset, 0, Width - _Offset, Height);
                        var rcSrcRight = new RectangleF(0, 0, rcDstRight.Width*ratio, rcImage.Height);

                        var rcDstLeft = new RectangleF(0, 0, _Offset, Height);
                        var rcSrcLeft = new RectangleF(rcImage.Width - _Offset*ratio, 0, _Offset*ratio, rcImage.Height);

                        e.Graphics.DrawImage(_Image, rcDstRight, rcSrcRight, GraphicsUnit.Pixel);
                        e.Graphics.DrawImage(_Image, rcDstLeft, rcSrcLeft, GraphicsUnit.Pixel);

                        // draw verticle line at offset, so we can see the seam
                        //						e.Graphics.DrawLine(new Pen(Color.Red), new Point(_offset, 0), new Point(_offset, this.Height));
                    }
                }
            }
        }

        #endregion

        #region My Public Properties

        /// <summary>
        /// Gets or sets the Image to scroll when the control is not is Design Mode.
        /// </summary>	
        [Category("Behavior")]
        [Description("The Image to scroll when the control is not is Design Mode.")]
        public Image ImageToScroll
        {
            get { return _Image; }
            set
            {
                lock (this)
                {
                    try
                    {
                        if (_Image != null)
                            _Image.Dispose();

                        _Image = value;

                        Invalidate();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a flag that determines whether or not the control is scrolling the selected image. 
        /// </summary>
        [Category("Behavior")]
        [Description("Determines whether or not the control is scrolling the selected image.")]
        public bool IsScrolling
        {
            get { return _IsScrolling; }
            set
            {
                // bail if there is no change in the value
                if (_IsScrolling == value)
                    return;

                // update whether the image should be scrolling
                _IsScrolling = value;

                if (_IsScrolling)
                {
                    // reset the event and start a thread to scroll the image
                    _StopScrolling.Reset();
                    ThreadPool.QueueUserWorkItem(ScrollImage);
                }
                else
                {
                    _StopScrolling.Set();
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of pixels to move the image on each update. The default is 10.
        /// </summary>
        [Category("Behavior")]
        [Description("The number of pixels to move the image on each update. The default is 10.")]
        public int StepSize
        {
            get { return _Step; }
            set { _Step = value; }
        }

        /// <summary>
        /// Gets or sets the number of times per second the control will draw itself. The default is 30 times a second.
        /// </summary>
        [Category("Behavior")]
        [Description("The number of times per second the control will draw itself. The default is 30 times a second.")]
        public int FrameRate
        {
            get { return _FrameRate; }
            set { _FrameRate = value; }
        }

        #endregion

        #region My Public Methods

        /// <summary>
        /// Resets the Image to its default position.
        /// </summary>
        public void Reset()
        {
            _Offset = 0;
            Invalidate();
        }

        #endregion

        #region My Private Methods

        /// <summary>
        /// Loads the default MarqueeControl Image from the Global resource file
        /// </summary>
        private void LoadDefaultImage()
        {
            //TODO: _image = (Image)Properties.Resources.ResourceManager.GetObject("MarqueeControl");
            Invalidate();
        }

        /// <summary>
        /// The background thread procedure that will handle scrolling the Image
        /// </summary>
        private void ScrollImage(object stateObject)
        {
            try
            {
                while (true)
                {
                    // step forward on the offset
                    _Offset += _Step;

                    // reset the offset if we hit the edge
                    if (_Offset >= Width)
                        _Offset = 0;

                    // repaint
                    Invalidate();

                    // snooze a bit
                    // Thread.Sleep(_frameRate);					
                    if (_StopScrolling.WaitOne(_FrameRate, false))
                        return;
                }
            }
            catch (ThreadAbortException)
            {
                // watch out for this little guy. :P
                // some days i still feel like this is not right that an exception is thrown
                // but i suppose there's not really any better way for the framework to stop the thread
                // and give you control back
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        #endregion
    }
}