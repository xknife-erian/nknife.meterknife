using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

/*
 * https://github.com/AdamsLair/winforms.git
 */

namespace MeterKnife.Utils.ColorControls
{
    public partial class ColorPickerDialog : Form
    {
        public enum PrimaryAttrib
        {
            Hue,
            Saturation,
            Brightness,
            Red,
            Green,
            Blue
        }

        private bool _AlphaEnabled = true;
        private InternalColor _OldColor = new InternalColor(Color.Red);
        private PrimaryAttrib _PrimAttrib = PrimaryAttrib.Hue;
        private InternalColor _SelectedColor = new InternalColor(Color.Red);
        private bool _SuspendTextEvents;

        public ColorPickerDialog()
        {
            InitializeComponent();
        }

        public bool AlphaEnabled
        {
            get => _AlphaEnabled;
            set
            {
                _AlphaEnabled = value;
                alphaSlider.Enabled = _AlphaEnabled;
                numAlpha.Enabled = _AlphaEnabled;
            }
        }

        public Color OldColor
        {
            get => _OldColor.ToColor();
            set
            {
                _OldColor = new InternalColor(value);
                UpdateColorShowBox();
            }
        }

        public Color SelectedColor
        {
            get => _SelectedColor.ToColor();
            set
            {
                _SelectedColor = new InternalColor(value);
                UpdateColorControls();
            }
        }

        public PrimaryAttrib PrimaryAttribute
        {
            get => _PrimAttrib;
            set
            {
                _PrimAttrib = value;
                UpdateColorControls();
            }
        }

        public event EventHandler ColorEdited;

        private void UpdateColorControls()
        {
            UpdatePrimaryAttributeRadioBox();
            UpdateText();
            UpdateColorShowBox();
            UpdateColorPanelGradient();
            UpdateColorSliderGradient();
            UpdateAlphaSliderGradient();
            UpdateColorPanelValue();
            UpdateColorSliderValue();
            UpdateAlphaSliderValue();
        }

        private void UpdatePrimaryAttributeRadioBox()
        {
            switch (_PrimAttrib)
            {
                default:
                case PrimaryAttrib.Hue:
                    radioHue.Checked = true;
                    break;
                case PrimaryAttrib.Saturation:
                    radioSaturation.Checked = true;
                    break;
                case PrimaryAttrib.Brightness:
                    radioValue.Checked = true;
                    break;
                case PrimaryAttrib.Red:
                    radioRed.Checked = true;
                    break;
                case PrimaryAttrib.Green:
                    radioGreen.Checked = true;
                    break;
                case PrimaryAttrib.Blue:
                    radioBlue.Checked = true;
                    break;
            }
        }

        private void UpdateText()
        {
            var tmp = _SelectedColor.ToColor();
            _SuspendTextEvents = true;

            textBoxHex.Text = $"{tmp.ToArgb():X}";

            numRed.Value = tmp.R;
            numGreen.Value = tmp.G;
            numBlue.Value = tmp.B;
            numAlpha.Value = tmp.A;

            numHue.Value = (decimal) (_SelectedColor.h * 360.0f);
            numSaturation.Value = (decimal) (_SelectedColor.s * 100.0f);
            numValue.Value = (decimal) (_SelectedColor.v * 100.0f);

            _SuspendTextEvents = false;
        }

        private void UpdateColorShowBox()
        {
            colorShowBox.UpperColor = _AlphaEnabled ? _OldColor.ToColor() : Color.FromArgb(255, _OldColor.ToColor());
            colorShowBox.LowerColor = _AlphaEnabled ? _SelectedColor.ToColor() : Color.FromArgb(255, _SelectedColor.ToColor());
        }

        private void UpdateColorPanelGradient()
        {
            Color tmp;
            switch (_PrimAttrib)
            {
                default:
                case PrimaryAttrib.Hue:
                    colorPanel.SetupXYGradient(
                        Color.White,
                        ExtMethodsColor.ColorFromHSV(_SelectedColor.h, 1.0f, 1.0f),
                        Color.Black,
                        Color.Transparent);
                    break;
                case PrimaryAttrib.Saturation:
                    colorPanel.SetupHueBrightnessGradient(_SelectedColor.s);
                    break;
                case PrimaryAttrib.Brightness:
                    colorPanel.SetupHueSaturationGradient(_SelectedColor.v);
                    break;
                case PrimaryAttrib.Red:
                    tmp = _SelectedColor.ToColor();
                    colorPanel.SetupGradient(
                        Color.FromArgb(255, tmp.R, 255, 0),
                        Color.FromArgb(255, tmp.R, 255, 255),
                        Color.FromArgb(255, tmp.R, 0, 0),
                        Color.FromArgb(255, tmp.R, 0, 255),
                        32);
                    break;
                case PrimaryAttrib.Green:
                    tmp = _SelectedColor.ToColor();
                    colorPanel.SetupGradient(
                        Color.FromArgb(255, 255, tmp.G, 0),
                        Color.FromArgb(255, 255, tmp.G, 255),
                        Color.FromArgb(255, 0, tmp.G, 0),
                        Color.FromArgb(255, 0, tmp.G, 255),
                        32);
                    break;
                case PrimaryAttrib.Blue:
                    tmp = _SelectedColor.ToColor();
                    colorPanel.SetupGradient(
                        Color.FromArgb(255, 255, 0, tmp.B),
                        Color.FromArgb(255, 255, 255, tmp.B),
                        Color.FromArgb(255, 0, 0, tmp.B),
                        Color.FromArgb(255, 0, 255, tmp.B),
                        32);
                    break;
            }
        }

        private void UpdateColorPanelValue()
        {
            Color tmp;
            switch (_PrimAttrib)
            {
                default:
                case PrimaryAttrib.Hue:
                    colorPanel.ValuePercentual = new PointF(
                        _SelectedColor.s,
                        _SelectedColor.v);
                    break;
                case PrimaryAttrib.Saturation:
                    colorPanel.ValuePercentual = new PointF(
                        _SelectedColor.h,
                        _SelectedColor.v);
                    break;
                case PrimaryAttrib.Brightness:
                    colorPanel.ValuePercentual = new PointF(
                        _SelectedColor.h,
                        _SelectedColor.s);
                    break;
                case PrimaryAttrib.Red:
                    tmp = _SelectedColor.ToColor();
                    colorPanel.ValuePercentual = new PointF(
                        tmp.B / 255.0f,
                        tmp.G / 255.0f);
                    break;
                case PrimaryAttrib.Green:
                    tmp = _SelectedColor.ToColor();
                    colorPanel.ValuePercentual = new PointF(
                        tmp.B / 255.0f,
                        tmp.R / 255.0f);
                    break;
                case PrimaryAttrib.Blue:
                    tmp = _SelectedColor.ToColor();
                    colorPanel.ValuePercentual = new PointF(
                        tmp.G / 255.0f,
                        tmp.R / 255.0f);
                    break;
            }
        }

        private void UpdateColorSliderGradient()
        {
            Color tmp;
            switch (_PrimAttrib)
            {
                default:
                case PrimaryAttrib.Hue:
                    colorSlider.SetupHueGradient( /*this.selColor.GetHSVSaturation(), this.selColor.GetHSVBrightness()*/);
                    break;
                case PrimaryAttrib.Saturation:
                    colorSlider.SetupGradient(
                        ExtMethodsColor.ColorFromHSV(_SelectedColor.h, 0.0f, _SelectedColor.v),
                        ExtMethodsColor.ColorFromHSV(_SelectedColor.h, 1.0f, _SelectedColor.v));
                    break;
                case PrimaryAttrib.Brightness:
                    colorSlider.SetupGradient(
                        ExtMethodsColor.ColorFromHSV(_SelectedColor.h, _SelectedColor.s, 0.0f),
                        ExtMethodsColor.ColorFromHSV(_SelectedColor.h, _SelectedColor.s, 1.0f));
                    break;
                case PrimaryAttrib.Red:
                    tmp = _SelectedColor.ToColor();
                    colorSlider.SetupGradient(
                        Color.FromArgb(255, 0, tmp.G, tmp.B),
                        Color.FromArgb(255, 255, tmp.G, tmp.B));
                    break;
                case PrimaryAttrib.Green:
                    tmp = _SelectedColor.ToColor();
                    colorSlider.SetupGradient(
                        Color.FromArgb(255, tmp.R, 0, tmp.B),
                        Color.FromArgb(255, tmp.R, 255, tmp.B));
                    break;
                case PrimaryAttrib.Blue:
                    tmp = _SelectedColor.ToColor();
                    colorSlider.SetupGradient(
                        Color.FromArgb(255, tmp.R, tmp.G, 0),
                        Color.FromArgb(255, tmp.R, tmp.G, 255));
                    break;
            }
        }

        private void UpdateColorSliderValue()
        {
            Color tmp;
            switch (_PrimAttrib)
            {
                default:
                case PrimaryAttrib.Hue:
                    colorSlider.ValuePercentual = _SelectedColor.h;
                    break;
                case PrimaryAttrib.Saturation:
                    colorSlider.ValuePercentual = _SelectedColor.s;
                    break;
                case PrimaryAttrib.Brightness:
                    colorSlider.ValuePercentual = _SelectedColor.v;
                    break;
                case PrimaryAttrib.Red:
                    tmp = _SelectedColor.ToColor();
                    colorSlider.ValuePercentual = tmp.R / 255.0f;
                    break;
                case PrimaryAttrib.Green:
                    tmp = _SelectedColor.ToColor();
                    colorSlider.ValuePercentual = tmp.G / 255.0f;
                    break;
                case PrimaryAttrib.Blue:
                    tmp = _SelectedColor.ToColor();
                    colorSlider.ValuePercentual = tmp.B / 255.0f;
                    break;
            }
        }

        private void UpdateAlphaSliderGradient()
        {
            alphaSlider.SetupGradient(Color.Transparent, Color.FromArgb(255, _SelectedColor.ToColor()));
        }

        private void UpdateAlphaSliderValue()
        {
            alphaSlider.ValuePercentual = _SelectedColor.a;
        }

        private void UpdateSelectedColorFromSliderValue()
        {
            Color tmp;
            switch (_PrimAttrib)
            {
                default:
                case PrimaryAttrib.Hue:
                    _SelectedColor.h = colorSlider.ValuePercentual;
                    break;
                case PrimaryAttrib.Saturation:
                    _SelectedColor.s = colorSlider.ValuePercentual;
                    break;
                case PrimaryAttrib.Brightness:
                    _SelectedColor.v = colorSlider.ValuePercentual;
                    break;
                case PrimaryAttrib.Red:
                    tmp = _SelectedColor.ToColor();
                    _SelectedColor = new InternalColor(Color.FromArgb(
                        tmp.A,
                        (int) Math.Round(colorSlider.ValuePercentual * 255.0f),
                        tmp.G,
                        tmp.B));
                    break;
                case PrimaryAttrib.Green:
                    tmp = _SelectedColor.ToColor();
                    _SelectedColor = new InternalColor(Color.FromArgb(
                        tmp.A,
                        tmp.R,
                        (int) Math.Round(colorSlider.ValuePercentual * 255.0f),
                        tmp.B));
                    break;
                case PrimaryAttrib.Blue:
                    tmp = _SelectedColor.ToColor();
                    _SelectedColor = new InternalColor(Color.FromArgb(
                        tmp.A,
                        tmp.R,
                        tmp.G,
                        (int) Math.Round(colorSlider.ValuePercentual * 255.0f)));
                    break;
            }
            OnColorEdited();
        }

        private void UpdateSelectedColorFromPanelValue()
        {
            Color tmp;
            switch (_PrimAttrib)
            {
                default:
                case PrimaryAttrib.Hue:
                    _SelectedColor.s = colorPanel.ValuePercentual.X;
                    _SelectedColor.v = colorPanel.ValuePercentual.Y;
                    break;
                case PrimaryAttrib.Saturation:
                    _SelectedColor.h = colorPanel.ValuePercentual.X;
                    _SelectedColor.v = colorPanel.ValuePercentual.Y;
                    break;
                case PrimaryAttrib.Brightness:
                    _SelectedColor.h = colorPanel.ValuePercentual.X;
                    _SelectedColor.s = colorPanel.ValuePercentual.Y;
                    break;
                case PrimaryAttrib.Red:
                    tmp = _SelectedColor.ToColor();
                    _SelectedColor = new InternalColor(Color.FromArgb(
                        tmp.A,
                        tmp.R,
                        (int) Math.Round(colorPanel.ValuePercentual.Y * 255.0f),
                        (int) Math.Round(colorPanel.ValuePercentual.X * 255.0f)));
                    break;
                case PrimaryAttrib.Green:
                    tmp = _SelectedColor.ToColor();
                    _SelectedColor = new InternalColor(Color.FromArgb(
                        tmp.A,
                        (int) Math.Round(colorPanel.ValuePercentual.Y * 255.0f),
                        tmp.G,
                        (int) Math.Round(colorPanel.ValuePercentual.X * 255.0f)));
                    break;
                case PrimaryAttrib.Blue:
                    tmp = _SelectedColor.ToColor();
                    _SelectedColor = new InternalColor(Color.FromArgb(
                        tmp.A,
                        (int) Math.Round(colorPanel.ValuePercentual.Y * 255.0f),
                        (int) Math.Round(colorPanel.ValuePercentual.X * 255.0f),
                        tmp.B));
                    break;
            }
            OnColorEdited();
        }

        private void UpdateSelectedColorFromAlphaValue()
        {
            _SelectedColor.a = alphaSlider.ValuePercentual;
            OnColorEdited();
        }

        private void OnColorEdited()
        {
            ColorEdited?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            _SelectedColor = _OldColor;
            UpdateColorControls();
        }

        private void radioHue_CheckedChanged(object sender, EventArgs e)
        {
            if (radioHue.Checked) PrimaryAttribute = PrimaryAttrib.Hue;
        }

        private void radioSaturation_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSaturation.Checked) PrimaryAttribute = PrimaryAttrib.Saturation;
        }

        private void radioValue_CheckedChanged(object sender, EventArgs e)
        {
            if (radioValue.Checked) PrimaryAttribute = PrimaryAttrib.Brightness;
        }

        private void radioRed_CheckedChanged(object sender, EventArgs e)
        {
            if (radioRed.Checked) PrimaryAttribute = PrimaryAttrib.Red;
        }

        private void radioGreen_CheckedChanged(object sender, EventArgs e)
        {
            if (radioGreen.Checked) PrimaryAttribute = PrimaryAttrib.Green;
        }

        private void radioBlue_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBlue.Checked) PrimaryAttribute = PrimaryAttrib.Blue;
        }

        private void colorPanel_PercentualValueChanged(object sender, EventArgs e)
        {
            if (ContainsFocus) UpdateSelectedColorFromPanelValue();
            UpdateColorSliderGradient();
            UpdateAlphaSliderGradient();
            UpdateColorShowBox();
            UpdateText();
        }

        private void colorSlider_PercentualValueChanged(object sender, EventArgs e)
        {
            if (ContainsFocus) UpdateSelectedColorFromSliderValue();
            UpdateColorPanelGradient();
            UpdateAlphaSliderGradient();
            UpdateColorShowBox();
            UpdateText();
        }

        private void alphaSlider_PercentualValueChanged(object sender, EventArgs e)
        {
            if (ContainsFocus) UpdateSelectedColorFromAlphaValue();
            UpdateColorSliderGradient();
            UpdateColorPanelGradient();
            UpdateColorShowBox();
            UpdateText();
        }

        private void numHue_ValueChanged(object sender, EventArgs e)
        {
            if (_SuspendTextEvents) return;
            _SelectedColor.h = (float) numHue.Value / 360.0f;
            UpdateColorControls();
        }

        private void numSaturation_ValueChanged(object sender, EventArgs e)
        {
            if (_SuspendTextEvents) return;
            _SelectedColor.s = (float) numSaturation.Value / 100.0f;
            UpdateColorControls();
        }

        private void numValue_ValueChanged(object sender, EventArgs e)
        {
            if (_SuspendTextEvents) return;
            _SelectedColor.v = (float) numValue.Value / 100.0f;
            UpdateColorControls();
        }

        private void numRed_ValueChanged(object sender, EventArgs e)
        {
            if (_SuspendTextEvents) return;
            var tmp = _SelectedColor.ToColor();
            _SelectedColor = new InternalColor(Color.FromArgb(
                tmp.A,
                (byte) numRed.Value,
                tmp.G,
                tmp.B));
            UpdateColorControls();
        }

        private void numGreen_ValueChanged(object sender, EventArgs e)
        {
            if (_SuspendTextEvents) return;
            var tmp = _SelectedColor.ToColor();
            _SelectedColor = new InternalColor(Color.FromArgb(
                tmp.A,
                tmp.R,
                (byte) numGreen.Value,
                tmp.B));
            UpdateColorControls();
        }

        private void numBlue_ValueChanged(object sender, EventArgs e)
        {
            if (_SuspendTextEvents) return;
            var tmp = _SelectedColor.ToColor();
            _SelectedColor = new InternalColor(Color.FromArgb(
                tmp.A,
                tmp.R,
                tmp.G,
                (byte) numBlue.Value));
            UpdateColorControls();
        }

        private void numAlpha_ValueChanged(object sender, EventArgs e)
        {
            if (_SuspendTextEvents) return;
            var tmp = _SelectedColor.ToColor();
            _SelectedColor = new InternalColor(Color.FromArgb(
                (byte) numAlpha.Value,
                tmp.R,
                tmp.G,
                tmp.B));
            UpdateColorControls();
        }

        private void textBoxHex_TextChanged(object sender, EventArgs e)
        {
            if (_SuspendTextEvents) return;
            int argb;
            if (int.TryParse(textBoxHex.Text, NumberStyles.HexNumber, CultureInfo.CurrentUICulture, out argb))
            {
                var tmp = Color.FromArgb(argb);
                _SelectedColor = new InternalColor(tmp);
                UpdateColorControls();
            }
        }

        private void colorShowBox_UpperClick(object sender, EventArgs e)
        {
            _SelectedColor = _OldColor;
            UpdateColorControls();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Hide();
        }

        private void ColorPickerDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DialogResult == DialogResult.Cancel)
                SelectedColor = OldColor;
            else
                OldColor = SelectedColor;
        }

        private struct InternalColor
        {
            public float h;
            public float s;
            public float v;
            public float a;

            public InternalColor(float h, float s, float v, float a)
            {
                this.h = h;
                this.s = s;
                this.v = v;
                this.a = a;
            }

            public InternalColor(Color c)
            {
                h = c.GetHSVHue();
                s = c.GetHSVSaturation();
                v = c.GetHSVBrightness();
                a = c.A / 255.0f;
            }

            public Color ToColor()
            {
                return Color.FromArgb((int) Math.Round(a * 255.0f), ExtMethodsColor.ColorFromHSV(h, s, v));
            }
        }
    }
}