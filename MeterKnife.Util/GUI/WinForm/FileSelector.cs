using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NKnife.GUI.WinForm.Common;

namespace NKnife.GUI.WinForm
{
    /// <summary>
    /// 文件选择器控件
    /// </summary>
    public class FileSelector : UserControl
    {
        #region 构造函数

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private IContainer components;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FileSelector()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        private Control _FileNamesControl;
        private Button _FileSelectorButton;

        #region 属性

        private bool _ReadOnly;
        private FileSelectControlStyle _Style = FileSelectControlStyle.TextBoxAndTextButton;

        /// <summary>
        /// 获取或设置控件的显示样式
        /// </summary>
        public FileSelectControlStyle Style
        {
            get { return _Style; }
            set
            {
                _Style = value;
                SetViewBoxStyle();
            }
        }

        /// <summary>
        /// 设置需打开的文件类型过滤器
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 返回的单个文件文件名
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// 返回的一组文件文件名
        /// </summary>
        public string[] FileNames { get; private set; }

        /// <summary>
        /// 获取或设置打开文件对话框是否可以多选文件
        /// </summary>
        public bool MultiSelect { get; set; }

        /// <summary>
        /// 获取或设置打开文件对话框的初始目录
        /// </summary>
        public string InitialDirectory { get; set; }

        /// <summary>
        /// 获取或设置打开文件对话框的标题
        /// </summary>
        public string DialogTitle { get; set; }

        /// <summary>
        /// 获取或设置ViewBox的宽度
        /// </summary>
        public int ViewBoxWidth
        {
            get { return _FileNamesControl.Width; }
            set
            {
                if (_FileNamesControl != null)
                {
                    _FileNamesControl.Width = value;
                    _FileSelectorButton.Location = new Point(_FileNamesControl.Width + 2, 0);
                    SetSize();
                }
            }
        }

        /// <summary>
        /// 获取或设置文字Button上显示的文字
        /// </summary>
        public string ButtonText
        {
            get { return _FileSelectorButton.Text; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    value = "&Browser";
                }
                _FileSelectorButton.Text = value;
                _FileSelectorButton.Width =
                    (int) (CreateGraphics().MeasureString(value, Font).Width) + 20;
                SetSize();
            }
        }

        /// <summary>
        /// 获取或设置Button上显示的图标
        /// </summary>
        public Image ButtonImage
        {
            get { return _FileSelectorButton.Image; }
            set
            {
                if (value != null)
                {
                    _FileSelectorButton.Text = string.Empty;
                    _FileSelectorButton.Image = value;
                    _FileSelectorButton.Width = (value.Width) + 20;
                    SetSize();
                }
            }
        }

        /// <summary>
        /// 获取或设置文件选择的历史记录
        /// </summary>
        public string[] SelectHistory { get; set; }

        /// <summary>
        /// 是否保存历史记录
        /// </summary>
        public bool IsSaveHistory { get; set; }

        /// <summary>
        /// 是否是选择文件夹
        /// </summary>
        public bool IsSelectFolder { get; set; }

        /// <summary>
        /// 控件是否只读
        /// </summary>
        public bool ReadOnly
        {
            get { return _ReadOnly; }
            set
            {
                _ReadOnly = value;
                if (_FileNamesControl is TextBox)
                {
                    ((TextBox) _FileNamesControl).ReadOnly = value;
                }
            }
        }

        #endregion

        /// <summary>
        /// 当文件被选择后发生的事件
        /// </summary>
        public event EventHandler FileSelected;

        protected void OnFileSelected(EventArgs e)
        {
            if (FileSelected != null)
            {
                FileSelected(this, e);
            }
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitializeComponent()
        {
            components = new Container();
            SuspendLayout();

            _Style = FileSelectControlStyle.TextBoxAndTextButton;
            SetViewBoxStyle();

            InitialDirectory = string.Empty;
            DialogTitle = string.Empty;
            FileName = InitialDirectory;
            FileNames = null;
            ButtonImage = null;

            _FileNamesControl.Location = new Point(0, 1);
            _FileNamesControl.Size = new Size(150, 23);
            Controls.Add(_FileNamesControl);

            _FileSelectorButton = new Button();
            _FileSelectorButton.Location = new Point(_FileNamesControl.Width + 2, 0);
            _FileSelectorButton.Size = new Size(23, 23);
            _FileSelectorButton.Click += _FileSelectorButton_Click;
            Controls.Add(_FileSelectorButton);

            SetSize();

            ResumeLayout(false);
            PerformLayout();
        }

        /// <summary>
        /// 设置控件本身的宽度
        /// </summary>
        private void SetSize()
        {
            int width = _FileNamesControl.Width + 2 + _FileSelectorButton.Width;
            Size = new Size(width, 23);
        }

        /// <summary>
        /// 设置控件中的ViewBoxStyle
        /// </summary>
        private void SetViewBoxStyle()
        {
            switch (_Style) //根据样式控制Box控件是TextBox还是CommoBox
            {
                    #region 根据控件的样式设定控件的显示

                case FileSelectControlStyle.None:
                case FileSelectControlStyle.TextBoxAndTextButton:
                case FileSelectControlStyle.TextBoxAndImageButton:
                case FileSelectControlStyle.TextBoxAndTextImageButton:

                    #region

                    {
                        _FileNamesControl = new TextBox();
                        _FileNamesControl.Name = "View_TextBox";
                        break;
                    }

                    #endregion

                case FileSelectControlStyle.ComboBoxAndTextButton:
                case FileSelectControlStyle.ComboBoxAndImageButton:
                case FileSelectControlStyle.ComboBoxAndTextImageButton:

                    #region

                    {
                        _FileNamesControl = new ComboBox();
                        _FileNamesControl.Name = "View_ComboBox";
                        break;
                    }

                    #endregion

                case FileSelectControlStyle.OnlyTextButton:
                case FileSelectControlStyle.OnlyImageButton:
                case FileSelectControlStyle.OnlyTextImageButton:
                default:

                    #region

                    {
                        _FileNamesControl = null;
                        break;
                    }

                    #endregion

                    #endregion
            }
        }

        private void _FileSelectorButton_Click(object sender, EventArgs e)
        {
            if (IsSelectFolder) //选择目录的方式
            {
                var dialog = new FolderBrowserDialog();
                dialog.SelectedPath = Text;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    FileName = dialog.SelectedPath;
                    if (Controls.Contains(_FileNamesControl))
                    {
                        _FileNamesControl.Text = FileName;
                    }
                    OnFileSelected(EventArgs.Empty);
                }
            }
            else //选择文件的方式
            {
                var dialog = new OpenFileDialog();
                dialog.Multiselect = MultiSelect;
                dialog.Title = DialogTitle;
                dialog.InitialDirectory = InitialDirectory;
                dialog.Filter = Filter;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (MultiSelect) //判断是否可以多选
                    {
                        FileNames = dialog.FileNames;
                        if (_FileNamesControl != null)
                        {
                            if (FileNames.Length > 1)
                            {
                                _FileNamesControl.Text = FileNames[0] + ", ...";
                            }
                            else
                            {
                                _FileNamesControl.Text = FileNames[0];
                            }
                        }
                    }
                    else
                    {
                        FileName = dialog.FileName;
                        if (_FileNamesControl != null)
                        {
                            if (_FileNamesControl is TextBox)
                            {
                                (_FileNamesControl).Text = FileName;
                            }
                        }
                    }
                    OnFileSelected(EventArgs.Empty);
                }
            }
            base.OnClick(e);
        }
    }
}