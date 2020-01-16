using System;
using System.Drawing;
using System.Windows.Forms;

namespace MeterKnife.Util.GUI
{
    public class TouchNumberBox : TextBox
    {
        private TouchNumberBoxForm _PopupForm;

        public TouchNumberBox()
        {
            if (_PopupForm == null || _PopupForm.IsDisposed)
            {
                _PopupForm = new TouchNumberBoxForm();
                _PopupForm.AddEvent += FormAddEvent;
                _PopupForm.SubtractEvent += FormSubtractEvent;
            }

            Step = 1;
            Direction = PopupDirection.Right;
            Value = null;

            Leave += TouchNumberBoxLeave;
        }

        public int Step { get; set; }

        public PopupDirection Direction { get; set; }

        public int? Value
        {
            get
            {
                int result;
                if (int.TryParse(Text, out result))
                    return result;
                return null;
            }
            set { Text = value.HasValue ? value.Value.ToString() : string.Empty; }
        }

        private void TouchNumberBoxLeave(object sender, EventArgs e)
        {
            _PopupForm.Hide();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (!Char.IsDigit(e.KeyChar) && (Keys) e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            Leave -= TouchNumberBoxLeave;

            if (_PopupForm == null || _PopupForm.IsDisposed)
            {
                _PopupForm = new TouchNumberBoxForm();
                _PopupForm.AddEvent += FormAddEvent;
                _PopupForm.SubtractEvent += FormSubtractEvent;
            }

            SetPopupLocation();

            if (!_PopupForm.Visible)
            {
                _PopupForm.Show(this);
            }

            Focus();

            Leave += TouchNumberBoxLeave;
        }

        private void SetPopupLocation()
        {
            switch (Direction)
            {
                case PopupDirection.Top:
                    _PopupForm.Location = new Point(Parent.PointToScreen(Location).X,
                                                    Parent.PointToScreen(Location).Y - _PopupForm.Height);
                    break;
                case PopupDirection.Bottom:
                    _PopupForm.Location = new Point(Parent.PointToScreen(Location).X,
                                                    Parent.PointToScreen(Location).Y + Height);
                    break;
                case PopupDirection.Left:
                    _PopupForm.Location = new Point(Parent.PointToScreen(Location).X - _PopupForm.Width,
                                                    Parent.PointToScreen(Location).Y + Height - _PopupForm.Height);
                    break;
                case PopupDirection.Right:
                    _PopupForm.Location = new Point(Parent.PointToScreen(Location).X + Width,
                                                    Parent.PointToScreen(Location).Y + Height - _PopupForm.Height);
                    break;
            }
        }

        private void FormAddEvent(object sender, EventArgs e)
        {
            if (!Value.HasValue)
                Value = 0;
            Value += Step;
        }

        private void FormSubtractEvent(object sender, EventArgs e)
        {
            if (!Value.HasValue)
                Value = 0;
            if (Value >= Step)
                Value -= Step;
        }
    }

    public enum PopupDirection
    {
        Top,
        Bottom,
        Left,
        Right
    }
}