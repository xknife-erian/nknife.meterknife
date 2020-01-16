using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace MeterKnife.Util.GUI
{

	[ToolboxBitmap(typeof(CollapsibleSplitContainer), "Resources.collapse.bmp")]
	public class CollapsibleSplitContainer : SplitContainer, ISupportInitialize
	{
        public enum ButtonPosition { TopLeft, Center, BottomRight };
        public enum ButtonStyle { None, Image, PushButton, ScrollBar };
        public enum CollapseDistance { MinSize, Collapsed };

		#region Variables
		private bool panel1Minimized = false, panel2Minimized = false;
		private bool splitterFocusHide = false;
		private bool splitterVertical = true;
		private int splitterDistanceOriginal = 0;

		private ScrollBarArrowButtonState button1State = ScrollBarArrowButtonState.LeftNormal;
		private ScrollBarArrowButtonState button2State = ScrollBarArrowButtonState.RightNormal;

		// Used for various hit tests
		private Rectangle rectLeftDown, rectRightUp;

		// Left-oriented bitmap from which the other three directional bitmaps are derived
		private Bitmap splitterButtonBitmap = null;
		private Bitmap bitmapRight = null, bitmapUp = null, bitmapDown = null;

		// Property fields
		private ButtonPosition splitterButtonPosition = ButtonPosition.TopLeft;
		private ButtonStyle splitterButtonStyle = ButtonStyle.None;
		private CollapseDistance splitterCollapseDistance = CollapseDistance.MinSize;
		private Orientation currentOrientation = Orientation.Vertical;

		// Embedded cursor resources
	    private Cursor pointer = Cursors.Arrow;//new Cursor(typeof(CollapsibleSplitContainer), "Resources.pointer.cur");
        private Cursor pointer_no = Cursors.Arrow;//new Cursor(typeof(CollapsibleSplitContainer), "Resources.pointer_no.cur");
		#endregion

		public CollapsibleSplitContainer()
		{
			// Bug fix for SplitContainer problems with flickering and resizing
			ControlStyles cs = ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer;
			this.SetStyle(cs, true);
			object[] objArgs = new object[] { cs, true };
			MethodInfo objMethodInfo = typeof(Control).GetMethod("SetStyle", BindingFlags.NonPublic | BindingFlags.Instance);
			objMethodInfo.Invoke(this.Panel1, objArgs);
			objMethodInfo.Invoke(this.Panel2, objArgs);
		}

		#region Properties
		[Category("Collapsible"), Description("The bitmap used on the splitter pushbuttons")]
		[DefaultValue(null)]
		public Bitmap SplitterButtonBitmap
		{
			get { return splitterButtonBitmap; }
			set
			{
				if (splitterButtonBitmap != value)
				{
					splitterButtonBitmap = value;

					if (splitterButtonBitmap == null)
					{
						bitmapRight = null;
						bitmapUp = null;
						bitmapDown = null;
					}
					else
					{
						splitterButtonBitmap.MakeTransparent();

						// Create the bitmaps for the remaining directions
						bitmapRight = (Bitmap)splitterButtonBitmap.Clone();
						bitmapRight.RotateFlip(RotateFlipType.RotateNoneFlipX);
						bitmapUp = (Bitmap)splitterButtonBitmap.Clone();
						bitmapUp.RotateFlip(RotateFlipType.Rotate90FlipNone);
						bitmapDown = (Bitmap)splitterButtonBitmap.Clone();
						bitmapDown.RotateFlip(RotateFlipType.Rotate270FlipNone);

						// Reset the splitter width
						this.SplitterWidth = splitterButtonBitmap.Width;
					}

					this.Refresh();
				}
			}
		}

		[Category("Collapsible"), Description("Where the collapse buttons are located on the splitter")]
		[DefaultValue(ButtonPosition.TopLeft)]
		public ButtonPosition SplitterButtonPosition
		{
			get { return splitterButtonPosition; }
			set
			{
				if (splitterButtonPosition != value)
				{
					splitterButtonPosition = value;
					this.Refresh();
				}
			}
		}

		[Category("Collapsible"), Description("The technique used to generate the splitter buttons")]
		[DefaultValue(ButtonStyle.None)]
		public ButtonStyle SplitterButtonStyle
		{
			get { return splitterButtonStyle; }
			set
			{
				if (splitterButtonStyle != value)
				{
					splitterButtonStyle = value;
					SetButtonState();
					this.Refresh();
				}
			}
		}

		[Category("Collapsible"), Description("How completely the affected panel collapses")]
		[DefaultValue(CollapseDistance.MinSize)]
		public CollapseDistance SplitterCollapseDistance
		{
			get { return splitterCollapseDistance; }
			set
			{
				if (splitterCollapseDistance != value)
				{
					if (value == CollapseDistance.MinSize)
					{
						if (this.Panel1Collapsed)
						{
							panel1Minimized = true;
							this.SplitterDistance = this.Panel1MinSize;
						}
						else if (this.Panel2Collapsed)
						{
							panel2Minimized = true;

							// Calculate the splitter position
							int distance = -1 * (this.Panel2MinSize + this.SplitterWidth);
							if (splitterVertical) distance += this.Panel1.Width;
							else distance += this.Panel1.Height;

							this.SplitterDistance = distance;
						}

						this.Panel1Collapsed = false;
						this.Panel2Collapsed = false;
					}
					else if (value == CollapseDistance.Collapsed)
					{
						if (panel1Minimized) { this.Panel1Collapsed = true; }
						else if (panel2Minimized) { this.Panel2Collapsed = true; }

						panel1Minimized = false;
						panel2Minimized = false;
					}

					splitterCollapseDistance = value;
					this.Refresh();
				}
			}
		}

		[Category("Collapsible"), Description("Whether the focus rectangle shows on the splitter")]
		[DefaultValue(false)]
		public bool SplitterFocusHide
		{
			get { return splitterFocusHide; }
			set
			{
				if (splitterFocusHide != value)
				{
					splitterFocusHide = value;
					this.Refresh();
				}
			}
		}

		// Forces designer to refresh and reflect changes to the property
		public new bool IsSplitterFixed
		{
			get { return base.IsSplitterFixed; }
			set
			{
				if (base.IsSplitterFixed != value)
				{
					base.IsSplitterFixed = value;
					this.Refresh();
				}
			}
		}
		#endregion

		#region General Event Handlers
		protected override void OnBackgroundImageChanged(EventArgs e)
		{
			base.OnBackgroundImageChanged(e);

			// Add image transparency for bitmap background images. Base class
			// supports it for PNG and GIF but not bitmap format
			if (this.BackgroundImage != null)
			{
				((Bitmap)this.BackgroundImage).MakeTransparent();
				this.Refresh();
			}
		}

		// Redraw the buttons after the splitter is tabbed into
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			if (splitterButtonStyle == ButtonStyle.None) { return; }
			this.Invalidate();
		}

		// Force redraw of splitter after change in layout
		protected override void OnLayout(LayoutEventArgs e)
		{
			base.OnLayout(e);

			// Store current orientation value. Adjust buttons to orientation
			if (currentOrientation != this.Orientation)
			{
				currentOrientation = this.Orientation;
				splitterVertical = (currentOrientation == Orientation.Vertical) ? true : false;

				// Reset cursor and buttons
				SetButtonState();
			}

			this.Invalidate();
		}

		// Paint splitter and, if enabled, the buttons
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (this.IsSplitterFixed) { return; }

			// Do nothing if buttons are disabled. If FocusHide is enabled,
			// redraw the splitter background to hide the focus rectangle
			if (splitterButtonStyle == ButtonStyle.None || (splitterButtonStyle == ButtonStyle.Image && splitterButtonBitmap == null))
			{
				if (splitterFocusHide) { DrawSplitterBackground(e.Graphics); }
				return;
			}

			if (this.Panel1Collapsed || this.Panel2Collapsed) { return; }

			SetButtonDimensions();

			// Draw the splitter surface and buttons
			DrawSplitterBackground(e.Graphics);
			DrawSplitterFocus(e.Graphics);
			DrawSplitterButtons(e.Graphics);
		}
		#endregion

		#region Mouse Event Handlers
		// Change the button state to Pressed. Default OnMouseDown processing activates
		// the halftone hatch pattern that is shown when the splitter is dragged. There
		// is no way to disable the hatch without a major rewrite of the base class
		protected override void OnMouseDown(MouseEventArgs e)
		{
			// Do default processing and return if the buttons are disabled
			if (splitterButtonStyle == ButtonStyle.None)
			{
				base.OnMouseDown(e);
				return;
			}

			// When mouse is over a button, change the state. Otherwise, do default processing
			if (IsHotLeftDown())
			{
				if (splitterVertical) { button1State = ScrollBarArrowButtonState.LeftPressed; }
				else { button2State = ScrollBarArrowButtonState.DownPressed; }
				this.Invalidate();
			}
			else if (IsHotRightUp())
			{
				if (splitterVertical) { button2State = ScrollBarArrowButtonState.RightPressed; }
				else { button1State = ScrollBarArrowButtonState.UpPressed; }
				this.Invalidate();
			}
			else base.OnMouseDown(e);
		}

		// Set a new splitter location based on which button was pushed and the current ButtonStyle
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (splitterButtonStyle == ButtonStyle.None) { return; }

			if (splitterCollapseDistance == CollapseDistance.Collapsed)
			{
				// Hide the panel associated with the clicked button
				if (IsHotLeftDown())
				{
					if (splitterVertical) { this.Panel1Collapsed = true; }
					else { this.Panel2Collapsed = true; }
				}
				else if (IsHotRightUp())
				{
					if (splitterVertical) { this.Panel2Collapsed = true; }
					else { this.Panel1Collapsed = true; }
				}
			}
			else if (splitterCollapseDistance == CollapseDistance.MinSize)
			{
				// If the panel for the clicked button is already minimized, do nothing
				// Otherwise, have the panel shrink to or return from the minimum size
				if ((splitterVertical && IsHotLeftDown()) || (!splitterVertical && IsHotRightUp()))
				{
					if (panel1Minimized) { return; }
					else if (panel2Minimized) // Panel 2
					{
						this.SplitterDistance = splitterDistanceOriginal;
						panel2Minimized = false;
					}
					else // Panel 1
					{
						splitterDistanceOriginal = this.SplitterDistance;
						this.SplitterDistance = this.Panel1MinSize;
						panel1Minimized = true;
					}
				}
				else if ( (!splitterVertical && IsHotLeftDown()) || (splitterVertical && IsHotRightUp()) )
				{
					if (panel2Minimized) { return; }
					else if (panel1Minimized) // Panel 1
					{
						this.SplitterDistance = splitterDistanceOriginal;
						panel1Minimized = false;
					}
					else // Panel 2
					{
						splitterDistanceOriginal = this.SplitterDistance;

						// When the splitter is vertical, set the location of the splitter to
						// the splitcontainer control width minus the minimum size of panel 2.
						// For horizontal, set it to height minus panel 2 minimum size
						if (splitterVertical) { this.SplitterDistance = this.Width - this.Panel2MinSize; }
						else { this.SplitterDistance = this.Height - this.Panel2MinSize; }

						panel2Minimized = true;
					}
				}
			}

			this.Refresh();
		}

		// Set the cursor and button state when the mouse is over a button
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			SetButtonState();
			SetCursor();
		}

		// Set the cursor and button state when the mouse enters the button region
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			SetButtonState();
		}

		// Reset the cursor and button state when the mouse leaves
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			SetButtonState();
			Cursor = Cursors.Default;
		}
		#endregion

		#region Drawing Routines
		// Fill the splitter background with the background color and image
		private void DrawSplitterBackground(Graphics g)
		{
			Color backcolor = this.BackColor;
			if (backcolor == Color.Transparent)
			{
				// Find the base color that underlies transparency
				Control parent = this.Parent;
				while (parent.BackColor == Color.Transparent)
				{
					parent = parent.Parent;
				}
				backcolor = parent.BackColor;
			}

			// Paint the background with the underlying background color
			using (SolidBrush brush = new SolidBrush(backcolor))
			{
				g.FillRectangle(brush, this.SplitterRectangle);
			}

			// Draw the background image if present
			if (this.BackgroundImage != null)
			{
				// Use a texture brush to replicate base class tiling
				using (TextureBrush brush = new TextureBrush(this.BackgroundImage))
				{
					g.FillRectangle(brush, this.SplitterRectangle);
				}
			}
		}

		// Draw the modified focus rectangle if focus is not hidden
		private void DrawSplitterFocus(Graphics g)
		{
			if (splitterButtonStyle == ButtonStyle.None) return;

			if (this.Focused && !splitterFocusHide)
			{
				Rectangle focus = new Rectangle(this.SplitterRectangle.Location, this.SplitterRectangle.Size);

				// Draw the focus rectangle to the left/top of the buttons
				if (splitterVertical) { focus.Height = rectLeftDown.Top; }
				else { focus.Width = rectLeftDown.Left; }
				focus.Inflate(-1, -1);
				ControlPaint.DrawFocusRectangle(g, focus, this.ForeColor, this.BackColor);

				// Draw the focus rectangle to the right/bottom of the buttons
				if (splitterVertical)
				{
					focus.Location = new Point(rectRightUp.Left, rectRightUp.Bottom);
					focus.Size = new Size(rectRightUp.Width, this.SplitterRectangle.Bottom - rectRightUp.Bottom);
				}
				else
				{
					focus.Location = new Point(rectRightUp.Right + 1, rectRightUp.Top);
					focus.Size = new Size(this.SplitterRectangle.Right - rectRightUp.Right - 1, rectRightUp.Height);
				}
				focus.Inflate(-1, -1);
				ControlPaint.DrawFocusRectangle(g, focus, this.ForeColor, this.BackColor);
			}
		}

		// Render the splitter buttons based on system capability and button style
		private void DrawSplitterButtons(Graphics g)
		{
			if (splitterButtonStyle == ButtonStyle.Image)
			{
				if (!panel1Minimized)
				{
					if (splitterVertical) { g.DrawImage(splitterButtonBitmap, rectLeftDown); }
					else { g.DrawImage(bitmapUp, rectRightUp); }
				}

				if (!panel2Minimized)
				{
					if (splitterVertical) { g.DrawImage(bitmapRight, rectRightUp); }
					else { g.DrawImage(bitmapDown, rectLeftDown); }
				}
			}
			else if (splitterButtonStyle == ButtonStyle.PushButton)
			{
				// Map ScrollBarArrowButtonStates to PushButtonStates
				PushButtonState pbs1 = (PushButtonState)((int)button1State & 3);
				PushButtonState pbs2 = (PushButtonState)((int)button2State & 3);

				if (!panel1Minimized)
				{
					if (splitterVertical) { ButtonRenderer.DrawButton(g, rectLeftDown, pbs1); }
					else { ButtonRenderer.DrawButton(g, rectRightUp, pbs1); }

					if (splitterButtonBitmap != null)
					{
						if (splitterVertical) { g.DrawImage(splitterButtonBitmap, rectLeftDown); }
						else { g.DrawImage(bitmapUp, rectRightUp); }
					}
				}

				if (!panel2Minimized)
				{
					if (splitterVertical) { ButtonRenderer.DrawButton(g, rectRightUp, pbs2); }
					else { ButtonRenderer.DrawButton(g, rectLeftDown, pbs2); }

					if (splitterButtonBitmap != null)
					{
						if (splitterVertical) { g.DrawImage(bitmapRight, rectRightUp); }
						else { g.DrawImage(bitmapDown, rectLeftDown); }
					}
				}
			}
			else if (ScrollBarRenderer.IsSupported && splitterButtonStyle == ButtonStyle.ScrollBar)
			{
				if (!panel1Minimized)
				{
					if (splitterVertical) { ScrollBarRenderer.DrawArrowButton(g, rectLeftDown, button1State); }
					else{  ScrollBarRenderer.DrawArrowButton(g, rectRightUp, button1State); }
				}

				if (!panel2Minimized)
				{
					if (splitterVertical) { ScrollBarRenderer.DrawArrowButton(g, rectRightUp, button2State); }
					else { ScrollBarRenderer.DrawArrowButton(g, rectLeftDown, button2State); }
				}
			}
		}
		#endregion

		#region Miscellaneous Helpers
		// Tests for button hot state
		private bool IsHotLeftDown() { return rectLeftDown.Contains(PointToClient(MousePosition)); }
		private bool IsHotRightUp() { return rectRightUp.Contains(PointToClient(MousePosition)); }

		// Set the button state to hot or normal
		private void SetButtonState()
		{
			if (SplitterButtonStyle == ButtonStyle.None) { return; }

			// Set button states for Left and Down buttons
			if (IsHotLeftDown())
			{
				if (splitterVertical) { button1State = ScrollBarArrowButtonState.LeftHot; }
				else { button2State = ScrollBarArrowButtonState.DownHot; }
			}
			else
			{
				if (splitterVertical) { button1State = ScrollBarArrowButtonState.LeftNormal; }
				else { button2State = ScrollBarArrowButtonState.DownNormal; }
			}

			// Set button states for Right and Up buttons
			if (IsHotRightUp())
			{
				if (splitterVertical) { button2State = ScrollBarArrowButtonState.RightHot; }
				else { button1State = ScrollBarArrowButtonState.UpHot; }
			}
			else
			{
				if (splitterVertical) { button2State = ScrollBarArrowButtonState.RightNormal; }
				else { button1State = ScrollBarArrowButtonState.UpNormal; }
			}

			this.Invalidate();
		}

		// Dimension the button rectangles and call the button location method
		private void SetButtonDimensions()
		{
			int width = 0, height = 0;

			if (splitterButtonStyle == ButtonStyle.ScrollBar)
			{
				width = this.SplitterWidth;
				height = width;
			}
			else
			{
				if (splitterButtonBitmap == null)
				{
					if (splitterButtonStyle == ButtonStyle.Image)
					{
						width = 0;
						height = 0;
					}
					else
					{
						width = this.SplitterWidth;
						height = width;
					}
				}
				else
				{
					int h = splitterButtonBitmap.Height;
					int w = splitterButtonBitmap.Width;

					// Swap the width and height if one is larger than the
					// other and the splitter orientation is Horizontal
					if (h == w || splitterVertical)
					{
						width = w; // Normal
						height = h;
					}
					else
					{
						width = h; // Swapped
						height = w;
					}
				}
			}

			// Call the method that calculates the button region
			SetButtonLocation(new Rectangle(this.SplitterRectangle.Location, new Size(width, height)));
		}

		// Adjust the button locations based on orientation and position
		private void SetButtonLocation(Rectangle rect)
		{
			rectLeftDown = rect;
			rectRightUp = rect;

			int offset = 0, position = 0;

			if (splitterVertical)
			{
				offset = rect.Height;
				position = rect.Top;

				if (splitterButtonPosition == ButtonPosition.Center) { position = (this.SplitterRectangle.Height / 2) - (offset * 2); }
				else if (splitterButtonPosition == ButtonPosition.BottomRight) { position = (this.SplitterRectangle.Bottom) - (offset * 2); }

				rectLeftDown.Offset(0, position);
				rectRightUp.Offset(0, offset + position);
			}
			else
			{
				offset = rect.Width;
				position = rect.Left;

				if (splitterButtonPosition == ButtonPosition.Center) { position = (this.SplitterRectangle.Width / 2) - (offset * 2); }
				else if (splitterButtonPosition == ButtonPosition.BottomRight) { position = (this.SplitterRectangle.Right) - (offset * 2); }

				rectLeftDown.Offset(position, 0);
				rectRightUp.Offset(offset + position, 0);
			}
		}

		// Change the cursor when over the button region
		private void SetCursor()
		{
			if (splitterButtonStyle == ButtonStyle.None) { return; }

			bool leftHot = IsHotLeftDown(), rightHot = IsHotRightUp();

			if (leftHot)
			{
				if ((splitterVertical && panel1Minimized) || (!splitterVertical && panel2Minimized)) { this.Cursor = pointer_no; }
				else { this.Cursor = pointer; }
			}

			if (rightHot)
			{
				if ((!splitterVertical && panel1Minimized) || (splitterVertical && panel2Minimized)) { this.Cursor = pointer_no; }
				else { this.Cursor = pointer; }
			}

			if (!leftHot && !rightHot) { this.Cursor = Cursors.Default; }
		}

		// Load an image to use on the splitter
		public void LoadImage()
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "Images (*.bmp;*.png;*.gif;*.ico)|*.bmp;*.png;*.gif;*.ico|All files (*.*)|*.*";
			ofd.FilterIndex = 1;
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				this.SplitterButtonBitmap = new Bitmap(ofd.FileName);
			}
		}
		#endregion

		// ISupportInitialize methods. Unneeded for .Net 4 and higher
		public new void BeginInit() { }
        public new void EndInit() { }
	}
}