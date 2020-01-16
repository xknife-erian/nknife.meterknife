using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace MeterKnife.Util.GUI
{
    /// <summary>
    /// Represents an horizontal or vertical line.
    /// </summary>
    public class SingleLine : Control
    {
        #region Class constructors
        private Orientation _Orientation;
        private LineStyle _LineStyle;
        private Panel _ForeLine = new Panel();
        private int _FlatLineSize = 1;

        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public SingleLine()
            : this(Orientation.Horizontal, LineStyle.Auto)
        { }

        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="orientation">The orientation of the line.</param>
        public SingleLine(Orientation orientation)
            : this(orientation, LineStyle.Auto)
        { }

        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="lineStyle">The style of the line.</param>
        public SingleLine(LineStyle lineStyle)
            : this(Orientation.Horizontal, lineStyle)
        { }

        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="orientation">The orientation of the line.</param>
        /// <param name="lineStyle">The style of the line.</param>
        public SingleLine(Orientation orientation, LineStyle lineStyle)
        {
            this.Controls.Add(_ForeLine);
            this.BackColor = SystemColors.ButtonShadow;
            this.ForeColor = SystemColors.ButtonHighlight;
            this.TabStop = false;

            this.Disposed += new EventHandler(OnComponentDisposed);
            this.Resize += new EventHandler(OnComponentResize);
            this.ForeColorChanged += new EventHandler(OnComponentColorChanged);
            this.BackColorChanged += new EventHandler(OnComponentColorChanged);

            _Orientation = orientation;
            _LineStyle = lineStyle;
            _ForeLine.BorderStyle = BorderStyle.None;
            _ForeLine.Enabled = false;

            RefreshLine();
            OnComponentColorChanged(this, null);

            try
            {
                SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(OnVisualStyleChanged);
            }
            catch
            { }
        }

        /// <summary>
        /// Cleans up any resources being used.
        /// </summary>
        ~SingleLine()
        {
            OnComponentDisposed(this, null);
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Gets or sets the orientation of the line.
        /// </summary>
        [
        Category("Appearance"),
        Description("Specifies the orientation of the line."),
        DefaultValue(Orientation.Horizontal)
        ]
        public Orientation Orientation
        {
            get { return _Orientation; }
            set
            {
                _Orientation = value;
                RefreshLine();
            }
        }

        /// <summary>
        /// Gets or sets the style of the line.
        /// </summary>
        [
        Category("Appearance"),
        Description("Specifies the style of the line."),
        DefaultValue(LineStyle.Auto)
        ]
        public LineStyle LineStyle
        {
            get { return _LineStyle; }
            set
            {
                if(_LineStyle != value)
                    base.OnStyleChanged(new EventArgs());

                _LineStyle = value;
                RefreshLine();
            }
        }

        /// <summary>
        /// Gets or sets the height or the width of the flat line.
        /// </summary>
        /// <exception cref="ArgumentException">When the value is not greater then zero.</exception>
        [
        Category("Appearance"),
        Description("Specifies the height or the width of the flat line."),
        DefaultValue(1)
        ]
        public int FlatLineSize
        {
            get { return _FlatLineSize; }
            set
            {
                if(value <= 0)
                    throw new ArgumentException("The value must be greater then zero.");

                _FlatLineSize = value;
                RefreshLine();
            }
        }

        /// <summary>
        /// Get or sets the background color of the line.
        /// </summary>
        [
        Category("Appearance"),
        Description("Specifies the background color of the line."),
        DefaultValue(typeof(Color), "ButtonShadow"),
        TypeConverter(typeof(Color))
        ]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        /// <summary>
        /// Get or sets the foreground color of the line.
        /// </summary>
        [
        Category("Appearance"),
        Description("Specifies the foreground color of the line."),
        DefaultValue(typeof(Color), "ButtonHighlight"),
        TypeConverter(typeof(Color))
        ]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }
        #endregion

        #region Events handling
        /// <summary>
        /// Event fired when this Component has been disposed.
        /// </summary>
        /// <param name="sender">This Component.</param>
        /// <param name="eventArgs">Event's arguments.</param>
        private void OnComponentDisposed(object sender, EventArgs eventArgs)
        {
            _ForeLine.Dispose();
        }

        /// <summary>
        /// Event fired when this Component changes one of its colors.
        /// </summary>
        /// <param name="sender">This Component.</param>
        /// <param name="eventArgs">Event's arguments.</param>
        private void OnComponentColorChanged(object sender, EventArgs eventArgs)
        {
            _ForeLine.BackColor = this.ForeColor;
        }

        /// <summary>
        /// Event fired when the active visual style has been changed.
        /// </summary>
        /// <param name="sender">The SystemEvents instance.</param>
        /// <param name="eventArgs">Event's arguments.</param>
        private void OnVisualStyleChanged(object sender, UserPreferenceChangedEventArgs eventArgs)
        {
            if(eventArgs.Category == UserPreferenceCategory.VisualStyle)
                RefreshLine();
        }

        /// <summary>
        /// Event fired when this Component changes size.
        /// </summary>
        /// <param name="sender">This Component.</param>
        /// <param name="eventArgs">Event's arguments.</param>
        private void OnComponentResize(object sender, EventArgs eventArgs)
        {
            RefreshLine();
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Swaps the foreground color with the background one. 
        /// </summary>
        public void SwapColors()
        {
            Color color = this.ForeColor;
            this.ForeColor = this.BackColor;
            this.BackColor = color;        
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Refreshes the displayed line.
        /// </summary>
        private void RefreshLine()
        {
            if((_LineStyle == LineStyle.Flat) || ((_LineStyle == LineStyle.Auto)))
            {
                _ForeLine.Visible = false;

                if(_Orientation == Orientation.Vertical)
                    this.Width = _FlatLineSize;
                else
                    this.Height = _FlatLineSize;
            }
            else
            {
                _ForeLine.Visible = true;

                if(_Orientation == Orientation.Vertical)
                {
                    this.Width = 2;
                    _ForeLine.Dock = DockStyle.Right;
                    _ForeLine.Width = 1;
                }
                else
                {
                    this.Height = 2;
                    _ForeLine.Dock = DockStyle.Bottom;
                    _ForeLine.Height = 1;
                }
            }
        }
        #endregion

        #region Hidden objects
        /// <summary>
        /// Gets or sets the background image displayed in the control.  
        /// </summary>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        public override Image BackgroundImage
        {
            get { return base.BackgroundImage; }
            set { base.BackgroundImage = value; }
        }

        /// <summary>
        /// Occurs when the BackgroundImage property value changes.
        /// </summary>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        public new event EventHandler BackgroundImageChanged
        {
            add { base.BackgroundImageChanged += value; }
            remove { base.BackgroundImageChanged -= value; }
        }

        /// <summary>
        /// Gets or sets the background image layout as defined in the ImageLayout enumeration.
        /// </summary>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        public override ImageLayout BackgroundImageLayout
        {
            get { return base.BackgroundImageLayout; }
            set { base.BackgroundImageLayout = value; }
        }

        /// <summary>
        /// Occurs when the BackgroundImageLayout property value changes.
        /// </summary>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        public new event EventHandler BackgroundImageLayoutChanged
        {
            add { base.BackgroundImageLayoutChanged += value; }
            remove { base.BackgroundImageLayoutChanged -= value; }
        }

        /// <summary>
        /// Gets or sets the font of the text displayed by the control. 
        /// </summary>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        public override Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        /// <summary>
        /// Occurs when the Font property value changes.
        /// </summary>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        public new event EventHandler FontChanged
        {
            add { base.FontChanged += value; }
            remove { base.FontChanged -= value; }
        }

        /// <summary>
        /// Gets or sets the text associated with this control.  
        /// </summary>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        /// <summary>
        /// Occurs when the Text property value changes.
        /// </summary>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        public new event EventHandler TextChanged
        {
            add { base.TextChanged += value; }
            remove { base.TextChanged -= value; }
        }

        /// <summary>
        /// Gets or sets the Input Method Editor (IME) mode of the control.  
        /// </summary>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        public new ImeMode ImeMode
        {
            get { return base.ImeMode; }
            set { base.ImeMode = value; }
        }
        
        /// <summary>
        /// Occurs when the ImeMode property value changes.
        /// </summary>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        public new event EventHandler ImeModeChanged
        {
            add { base.ImeModeChanged += value; }
            remove { base.ImeModeChanged -= value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control causes validation to be performed on any controls that require validation when it receives focus.  
        /// </summary>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        public new bool CausesValidation
        {
            get { return base.CausesValidation; }
            set { base.CausesValidation = value; }
        }

        /// <summary>
        /// Occurs when the CausesValidation property value changes.
        /// </summary>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        public new event EventHandler CausesValidationChanged
        {
            add { base.CausesValidationChanged += value; }
            remove { base.CausesValidationChanged -= value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.
        /// </summary>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        public new bool TabStop
        {
            get { return base.TabStop; }
            set { base.TabStop = value; }
        }

        /// <summary>
        /// Occurs when the TabStop property value changes.
        /// </summary>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        public new event EventHandler TabStopChanged
        {
            add { base.TabStopChanged += value; }
            remove { base.TabStopChanged -= value; }
        }

        /// <summary>
        /// Gets or sets the tab order of the control within its container.
        /// </summary>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        public new int TabIndex
        {
            get { return base.TabIndex; }
            set { base.TabIndex = value; }
        }

        /// <summary>
        /// Occurs when the TabIndex property value changes.
        /// </summary>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        public new event EventHandler TabIndexChanged
        {
            add { base.TabIndexChanged += value; }
            remove { base.TabIndexChanged -= value; }
        }
        #endregion
    }

    #region Public enums
    /// <summary>
    /// Determines the style of the line.
    /// </summary>
    public enum LineStyle
    {
        /// <summary>
        /// Dispalys a standard 3D line or a flat one according to the operating system.
        /// </summary>
        Auto,

        /// <summary>
        /// Displays a standard 3D line (Windows 2000 style).
        /// </summary>
        Standard,

        /// <summary>
        /// Displays a flat line (Windows XP style).
        /// </summary>
        Flat
    }
    #endregion
}