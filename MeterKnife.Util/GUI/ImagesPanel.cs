using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using MeterKnife.Util.Events;

namespace MeterKnife.Util.GUI
{
    public sealed class ImagesPanel : FlowLayoutPanel
    {
        public enum ImageBoxSize
        {
            XLarge,
            Large,
            Medium,
            Small,
            XSmall
        }

        private readonly List<string> _Images = new List<string>();
        private string[] _CurrImages = new string[0];

        public ImagesPanel()
        {
            SetStyle(
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint, true);
            FlowDirection = FlowDirection.LeftToRight;
            AutoScroll = true;
            BackColor = SystemColors.ControlLight;

            ImageBoxLabelFont = DefaultFont;
            ImageBoxColor = Color.Brown;
            ImageBoxLabelColor = Color.Beige;
            BoxSize = ImageBoxSize.Medium;
            BoxMargin = 10;
        }

        /// <summary>
        /// 图片描述文字的字体
        /// </summary>
        [Category("定制")]
        [Description("图片描述文字的字体")]
        public Font ImageBoxLabelFont { get; set; }

        /// <summary>
        /// 图片描述文字的背景色
        /// </summary>
        [Category("定制")]
        [Description("图片描述文字的背景色")]
        public Color ImageBoxLabelColor { get; set; }

        /// <summary>
        /// 图片的背景色
        /// </summary>
        [Category("定制")]
        [Description("图片的背景色")]
        public Color ImageBoxColor { get; set; }

        /// <summary>
        /// 显示的图片的大小
        /// </summary>
        [Category("定制")]
        [Description("显示的图片的大小")]
        public ImageBoxSize BoxSize { get; set; }

        /// <summary>
        /// 图片与图片之间的间距
        /// </summary>
        [Category("定制")]
        [Description("图片与图片之间的间距")]
        public int BoxMargin { get; set; }

        /// <summary>
        /// 选中图片的描述文字
        /// </summary>
        [Category("定制")]
        [Description("选中图片的描述文字")]
        public Func<string, string> BuildLabelText { get; set; } 

        /// <summary>
        /// 被选中的图片文件
        /// </summary>
        [Browsable(false)]
        public string SelectedImageFile { get; set; }

        /// <summary>
        /// 当有图片被选中时发生
        /// </summary>
        public event EventHandler<EventArgs<string>> SelectedImage;

        private void OnSelectedImage(EventArgs<string> e)
        {
            EventHandler<EventArgs<string>> handler = SelectedImage;
            if (handler != null)
                handler(this, e);
        }

        public void FillImages(params string[] images)
        {
            _CurrImages = images;
            _Images.AddRange(images);
            var readImageThread = new Thread(ReadImage);
            readImageThread.IsBackground = true;
            readImageThread.Start();
        }

        private void ReadImage()
        {
            if (_CurrImages == null || _CurrImages.Length <= 0)
                return;
            foreach (string currImageFile in _CurrImages)
            {
                try
                {
                    byte[] bs = File.ReadAllBytes(currImageFile);
                    var mem = new MemoryStream(bs);
                    mem.Position = 0;
                    Image image = Image.FromStream(mem);

                    var imagebox = new ImageBox(ImageBoxColor, ImageBoxLabelColor, ImageBoxLabelFont);
                    var w = 80;
                    var h = 80/3*4;
                    int p = w/2 - w/20;
                    int b = 1;
                    switch (BoxSize)
                    {
                        case ImageBoxSize.XLarge:
                            b = 5;
                            break;
                        case ImageBoxSize.Large:
                            b = 4;
                            break;
                        case ImageBoxSize.Medium:
                            b = 3;
                            break;
                        case ImageBoxSize.Small:
                            b = 2;
                            break;
                        case ImageBoxSize.XSmall:
                            b = 1;
                            break;
                    }
                    w = w*b;
                    h = h*b;
                    imagebox.Size = new Size(w, h);
                    p = w / 2 - w / 10;

                    Padding = new Padding(p,0,0,0);//控制图片尽可能的居中
                    imagebox.Margin = new Padding(BoxMargin, BoxMargin, BoxMargin, BoxMargin);
                    var label = currImageFile;
                    if (BuildLabelText != null)
                    {
                        label = BuildLabelText.Invoke(currImageFile);
                    }
                    imagebox.SetImage(image, label);
                    string file = currImageFile;
                    imagebox.PictureClicked += (s, e) =>
                    {
                        if (e.Item)
                        {
                            SelectedImageFile = file;
                            OnSelectedImage(new EventArgs<string>(file));
                        }
                        else if(SelectedImageFile == file)
                        {
                            SelectedImageFile = string.Empty;
                        }
                    };
                    this.ThreadSafeInvoke(() => Controls.Add(imagebox));
                }
                catch (Exception)
                {
                    Debug.Fail(string.Format("文件无法读取:{0}", currImageFile));
                }
            }
        }

        private sealed class ImageBox : Control
        {
            private readonly Label _Label = new Label();
            private readonly Control _PictureBox = new Control();
            private bool _IsSelected;

            public ImageBox(Color imageBoxColor, Color imageBoxLabelColor, Font imageBoxLabelFont)
            {
                SuspendLayout();

                _Label.Dock = DockStyle.Bottom;
                _Label.BackColor = imageBoxLabelColor;
                _Label.Font = imageBoxLabelFont;
                _Label.TextAlign = ContentAlignment.MiddleCenter;

                _PictureBox.Location = new Point(0, 0);
                _PictureBox.BackColor = imageBoxColor;
                _PictureBox.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

                Controls.Add(_PictureBox);
                Controls.Add(_Label);
                ResumeLayout(false);
                PerformLayout();
                SetStyle(
                    ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
                    ControlStyles.AllPaintingInWmPaint, true);

                _PictureBox.MouseHover += _PictureBox_MouseHover;
                _PictureBox.MouseLeave += _PictureBox_MouseLeave;
                _PictureBox.MouseClick += _PictureBox_MouseClick;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                if (_IsSelected == false)
                    return;
                Graphics g = e.Graphics;
                g.DrawRectangle(new Pen(Color.Red, 3), new Rectangle(0, 0, Width - 1, Height - 1));
            }

            private void _PictureBox_MouseLeave(object sender, EventArgs e)
            {
                _Label.Font = new Font(DefaultFont.FontFamily, DefaultFont.Size, FontStyle.Regular);
            }

            private void _PictureBox_MouseHover(object sender, EventArgs e)
            {
                _Label.Font = new Font(DefaultFont.FontFamily, DefaultFont.Size, FontStyle.Bold);
            }

            private void _PictureBox_MouseClick(object sender, MouseEventArgs e)
            {
                _Label.Font = new Font(DefaultFont.FontFamily, DefaultFont.Size, FontStyle.Bold);
                _IsSelected = !_IsSelected;
                Invalidate();
                OnPictureClicked(new EventArgs<bool>(_IsSelected));
            }

            public event EventHandler<EventArgs<bool>> PictureClicked;

            private void OnPictureClicked(EventArgs<bool> e)
            {
                EventHandler<EventArgs<bool>> handler = PictureClicked;
                if (handler != null) 
                    handler(this, e);
            }

            protected override void OnAutoSizeChanged(EventArgs e)
            {
                base.OnAutoSizeChanged(e);
                SetOwnControls();
            }

            protected override void OnSizeChanged(EventArgs e)
            {
                base.OnSizeChanged(e);
                SetOwnControls();
            }

            private void SetOwnControls()
            {
                _PictureBox.Height = Height - _Label.Height;
            }

            public void SetImage(Image currImage, string label)
            {
                _PictureBox.BackgroundImageLayout = ImageLayout.Zoom;
                _PictureBox.BackgroundImage = currImage;
                _Label.Text = label;
            }
        }
    }
}