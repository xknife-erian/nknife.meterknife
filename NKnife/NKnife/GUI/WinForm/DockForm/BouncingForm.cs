using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NKnife.GUI.WinForm.DockForm
{
    /// <summary>
    /// This form class restricts the form to stay within the bounds of the monitor as well as bounce off of the edges.
    /// </summary>
    /// <remarks>
    /// Inheriting from this form class will enable your form to bounce off of the sides of the monitors' working area
    /// as it falls to the bottom in a way that is meant to mimic the effects of gravity.  The bouncing is intended merely
    /// for entertainment, but the form also has a more useful functionality.  It is constrained to stay within the bounds
    /// of the monitors' working area.  If you only want this functionality then you can set the value of the 'FormBouncingOn'
    /// property to false.
    /// </remarks>
    public partial class BouncingForm : Form
    {
        #region Local Variables/Properties

        //Variables
        Point formToCursorDiff;
        Point currentMouseLocation;
        Point previousFormLocation;
        System.Windows.Forms.Timer moveTimer;
        System.Windows.Forms.Timer drawTimer;
        bool formMoving;
        bool bouncingOn;
        double releaseVelocityX;
        double releaseVelocityY;

        private const int WM_SYSCOMMAND = 0x0112;
        private const int WM_LBUTTONUP = 0x202;
        private const int WM_NCPAINT = 0x85;
        private const int WM_NCLBUTTONDOWN = 0xa1;
        private const int SC_MOVE = 0xF010;
        private const int HTCAPTION = 2;
        private const int SC_RESTORE = 0xF120;

        /// <summary>
        /// Indicates whether the form should bounce when dragged and released.
        /// </summary>
        [Description("Turns form bouncing on or off")]
        [Category("Behavior")]
        public bool FormBouncingOn
        {
            get { return bouncingOn; }
            set { bouncingOn = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates and initializes the BouncingForm object.
        /// </summary>
        public BouncingForm()
        {
            InitializeComponent();

            currentMouseLocation = Cursor.Position;
            bouncingOn = true;

            Microsoft.Win32.SystemEvents.DisplaySettingsChanged += new EventHandler(SystemEvents_DisplaySettingsChanged);

            drawTimer = new System.Windows.Forms.Timer();
            drawTimer.Interval = 15;
            drawTimer.Tick += new EventHandler(drawTimer_Tick);

            moveTimer = new System.Windows.Forms.Timer();
            moveTimer.Interval = 15;
            moveTimer.Tick += new EventHandler(moveTimer_Tick);
        }

        #endregion

        #region Local Routines

        /// <summary>
        /// Gets the new form location after checking the bounds of the screen and calculating bouncing.
        /// </summary>
        /// <param name="xCoord">Proposed new X form location coordinate.</param>
        /// <param name="yCoord">Proposed new Y form location coordinate.</param>
        /// <returns>New location of the BouncingForm after taking screen bounds and bouncing parameters into consideration.</returns>
        private Point GetNewFormLocationWithinScreen(int xCoord, int yCoord)
        {
            int newX = xCoord;
            int newY = yCoord;
            Screen targetScreen = Screen.FromControl(this);
            bool isLeftOnScreen = false;
            bool isRightOnScreen = false;
            bool isTopOnScreen = false;
            bool isBottomOnScreen = false;
            foreach (Screen scr in Screen.AllScreens)
            {
                if (((newX > scr.WorkingArea.X) && (newX < (scr.WorkingArea.X + scr.WorkingArea.Width))) &&
                    (((newY > scr.WorkingArea.Y) && (newY < (scr.WorkingArea.Y + scr.WorkingArea.Height))) ||
                    (((newY + this.Height) > scr.WorkingArea.Y) && ((newY + this.Height) < (scr.WorkingArea.Y + scr.WorkingArea.Height)))))
                {
                    isLeftOnScreen = true;
                }
                if ((((newX + this.Width) > scr.WorkingArea.X) && ((newX + this.Width) < (scr.WorkingArea.X + scr.WorkingArea.Width))) &&
                    (((newY > scr.WorkingArea.Y) && (newY < (scr.WorkingArea.Y + scr.WorkingArea.Height))) ||
                    (((newY + this.Height) > scr.WorkingArea.Y) && ((newY + this.Height) < (scr.WorkingArea.Y + scr.WorkingArea.Height)))))
                {
                    isRightOnScreen = true;
                }
                if (((newY > scr.WorkingArea.Y) && (newY < (scr.WorkingArea.Y + scr.WorkingArea.Height))) &&
                    (((newX > scr.WorkingArea.X) && (newX < (scr.WorkingArea.X + scr.WorkingArea.Width))) ||
                    (((newX + this.Width) > scr.WorkingArea.X) && ((newX + this.Width) < (scr.WorkingArea.X + scr.WorkingArea.Width)))))
                {
                    isTopOnScreen = true;
                }
                if ((((newY + this.Height) > scr.WorkingArea.Y) && ((newY + this.Height) < (scr.WorkingArea.Y + scr.WorkingArea.Height))) &&
                    (((newX > scr.WorkingArea.X) && (newX < (scr.WorkingArea.X + scr.WorkingArea.Width))) ||
                    (((newX + this.Width) > scr.WorkingArea.X) && ((newX + this.Width) < (scr.WorkingArea.X + scr.WorkingArea.Width)))))
                {
                    isBottomOnScreen = true;
                }
            }

            if ((isLeftOnScreen == false) && (newX < targetScreen.WorkingArea.X)) newX = targetScreen.WorkingArea.X;
            if ((isRightOnScreen == false) &&
                ((newX + this.Width) > (targetScreen.WorkingArea.X + targetScreen.WorkingArea.Width)))
                newX = targetScreen.WorkingArea.X + targetScreen.WorkingArea.Width - this.Width;
            if ((isTopOnScreen == false) && (newY < targetScreen.WorkingArea.Y)) newY = targetScreen.WorkingArea.Y;
            if ((isBottomOnScreen == false) &&
                ((newY + this.Height) > (targetScreen.WorkingArea.Y + targetScreen.WorkingArea.Height)))
                newY = targetScreen.WorkingArea.Y + targetScreen.WorkingArea.Height - this.Height;

            if (bouncingOn)
            {
                if ((newX == targetScreen.WorkingArea.X) || (newX == targetScreen.WorkingArea.X + targetScreen.WorkingArea.Width - this.Width))
                {
                    releaseVelocityX *= -1.0;
                    if (releaseVelocityX >= 4.0) releaseVelocityX -= 4.0;
                    else if (releaseVelocityX <= -4.0) releaseVelocityX += 4.4;
                }
                if ((releaseVelocityX <= 1.0) && (releaseVelocityX >= -1.0))
                {
                    releaseVelocityX = 0.0;
                }
                if (newY == targetScreen.WorkingArea.Y)
                {
                    releaseVelocityY *= -1.0;
                }
                if (newY == targetScreen.WorkingArea.Y + targetScreen.WorkingArea.Height - this.Height)
                {
                    releaseVelocityY *= -1.0;
                    if (releaseVelocityY < -6.0) releaseVelocityY += 6.0;
                    if ((releaseVelocityX == 0.0) && ((releaseVelocityY <= 1.0) && (releaseVelocityY >= -1.0)))
                    {
                        drawTimer.Stop();
                        releaseVelocityY = 0.0;
                    }
                    else
                    {
                        if (releaseVelocityX >= 1.0) releaseVelocityX -= 0.5;
                        else if (releaseVelocityX <= -1.0) releaseVelocityX += 0.5;
                    }
                }
            }

            return new Point(newX, newY);
        }

        /// <summary>
        /// Manually starts the form moving timer if you are handling the form movement differently.
        /// </summary>
        public void FormMoveStart()
        {
            drawTimer.Stop();
            int xDiff = Cursor.Position.X - this.Location.X;
            int yDiff = Cursor.Position.Y - this.Location.Y;
            formToCursorDiff = new Point(xDiff, yDiff);
            currentMouseLocation = Cursor.Position;
            if (bouncingOn) moveTimer.Start();
            this.Capture = true;
            formMoving = true;
        }

        /// <summary>
        /// Manually stops the form moving timer and begins the bouncing if you are handling the form movement differently.
        /// </summary>
        public void FormMoveEnd()
        {
            moveTimer.Stop();
            formMoving = false;
            this.Capture = false;
            Point globalMouseLoc = Cursor.Position;
            Point newFormLocation = GetNewFormLocationWithinScreen(globalMouseLoc.X - formToCursorDiff.X, globalMouseLoc.Y - formToCursorDiff.Y);
            this.SetDesktopLocation(newFormLocation.X, newFormLocation.Y);
            currentMouseLocation = globalMouseLoc;
            releaseVelocityX = this.Location.X - previousFormLocation.X;
            releaseVelocityY = this.Location.Y - previousFormLocation.Y;

            if ((((releaseVelocityX > 1) || (releaseVelocityX < -1)) ||
                ((releaseVelocityY > 1) || (releaseVelocityY < -1))) && bouncingOn)
                drawTimer.Start();
        }

        /// <summary>
        /// Used to manually stop the form bouncing.
        /// </summary>
        public void StopFormBouncing()
        {
            drawTimer.Stop();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Manually moves the form so that it cannot escape the bounds of the screen.
        /// </summary>
        private void BouncingForm_MouseMove(object sender, MouseEventArgs e)
        {
            Point globalMouseLoc = Cursor.Position;

            if ((formMoving) && (currentMouseLocation != globalMouseLoc))
            {
                Point newFormLocation = GetNewFormLocationWithinScreen(globalMouseLoc.X - formToCursorDiff.X, globalMouseLoc.Y - formToCursorDiff.Y);
                this.SetDesktopLocation(newFormLocation.X, newFormLocation.Y);
                currentMouseLocation = globalMouseLoc;
            }
        }

        /// <summary>
        /// Used to keep the form within the bounds of the screen when the screen resolution is changed.
        /// </summary>
        void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            Point newFormLocation = GetNewFormLocationWithinScreen(this.Location.X, this.Location.Y);
            this.SetDesktopLocation(newFormLocation.X, newFormLocation.Y);
        }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// We are overriding the handling of Windows messages so that we can capture the ones
        /// necessary for coordinating the movements of the BouncingForm.
        /// </summary>
        /// <param name="m">Windows message passed in from the operating system.</param>
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            // This happens when the user right-clicks on the form's caption and selects "Move"
            if ((m.Msg == WM_SYSCOMMAND) && (m.WParam.ToInt32() == SC_MOVE))
            {
                FormMoveStart();
                return;
            }

            // This happens when the user clicks the left mouse button on the caption of the form
            else if ((m.Msg == WM_NCLBUTTONDOWN) && (m.WParam.ToInt32() == HTCAPTION))
            {
                FormMoveStart();
                return;
            }

            // This happens when the user releases the left mouse button
            else if ((m.Msg == WM_LBUTTONUP) && (formMoving))
            {
                FormMoveEnd();
            }

            // This happens when the form is restored
            else if ((m.Msg == WM_SYSCOMMAND) && (m.WParam.ToInt32() == SC_RESTORE))
            {
                releaseVelocityX = releaseVelocityY = 0.0;
                if (drawTimer.Enabled)
                {
                    Screen thisScreen = Screen.FromControl(this);
                    this.SetDesktopLocation(thisScreen.WorkingArea.X, thisScreen.WorkingArea.Y);
                }
            }

            // This message is sent by the OS when some of the form's border needs to be repainted
            else if (m.Msg == WM_NCPAINT)
            {
                // Repaints the entire border and caption of the form
                m.WParam = new IntPtr(1);
            }

            // Resume normal Windows message handling
            base.WndProc(ref m);
        }

        #endregion

        #region Time Event Handlers

        /// <summary>
        /// The moveTimer is used to track how fast the user is moving the mouse right before
        /// the BouncingForm is released.
        /// </summary>
        void moveTimer_Tick(object sender, EventArgs e)
        {
            previousFormLocation = this.Location;
        }

        /// <summary>
        /// The drawTimer is used to move the BouncingForm around the screen as to mimic the
        /// movement of it bouncing around and falling to the floor.
        /// </summary>
        void drawTimer_Tick(object sender, EventArgs e)
        {
            if (releaseVelocityX >= 0.01) releaseVelocityX -= 0.01;
            else if (releaseVelocityX <= -0.01) releaseVelocityX += 0.01;
            releaseVelocityY += 1.47;
            Point newFormLocation = GetNewFormLocationWithinScreen(this.Location.X + Convert.ToInt32(releaseVelocityX),
                this.Location.Y + Convert.ToInt32(releaseVelocityY));

            this.SetDesktopLocation(newFormLocation.X, newFormLocation.Y);
        }

        #endregion
    }
}
