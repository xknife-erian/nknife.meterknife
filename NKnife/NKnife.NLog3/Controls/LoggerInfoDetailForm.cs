using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NKnife.IoC;
using NKnife.ShareResources;
using NKnife.Utility;
using NLog;

namespace NKnife.NLog3.Controls
{
    /// <summary>日志详细信息展现窗体
    /// </summary>
    public sealed class LoggerInfoDetailForm : Form
    {
        public static void Show(LogEventInfo info)
        {
            var form = DI.Get<LoggerInfoDetailForm>();
            form.Size = new Size(600, 480);
            form.FillLogInfo(info);
            form.ShowDialog();
        }

        public LoggerInfoDetailForm()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Global.Culture);
            components = new Container();
            SuspendLayout();
            AutoScaleMode = AutoScaleMode.Font;
            Font = new Font("Tahoma", 8.25F);
            MinimumSize = new Size(320, 300);
            Size = new Size(640, 600);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "日志详细信息";
            ShowIcon = false;
            ShowInTaskbar = false;
            ControlBox = false;
            ResumeLayout(false);
            InitializeComponent();
            _CloseButton.Click += CloseButtonClick;
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void FillLogInfo(LogEventInfo info)
        {
            _LevelTextBox.Text = info.Level.Name;
            _TimeTextBox.Text = info.TimeStamp.ToString();
            _LogInfoTextBox.Text = info.FormattedMessage;
            _SourceTextBox.Text = info.LoggerName;
            switch (info.Level.Name)
            {
                case "Trace":
                case "Debug":
                case "Info":
                    _ExInfoPage.Text = UtilityResource.GetString(StringResource.ResourceManager, "Exception_TabName_Simple");
                    break;
                case "Warn":
                case "Error":
                case "Fatal":
                default:
                    _ExInfoPage.Text = UtilityResource.GetString(StringResource.ResourceManager, "Exception_TabName_Error");
                    break;
            }
            _LogStackTracePropertyGrid.SelectedObject = info;
            _MainTabControl.SelectedIndex = 0;
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private readonly IContainer components;

        private Button _CloseButton;
        private PropertyGrid _LogStackTracePropertyGrid;
        private TextBox _LevelTextBox;
        private TextBox _LogInfoTextBox;
        private TextBox _SourceTextBox;
        private TextBox _TimeTextBox;
        private Label _Label1;
        private Label _Label2;
        private Label _Label3;
        private Label _Label4;
        private TabControl _MainTabControl;
        private TabPage _MainPage;
        private TabPage _ExInfoPage;

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


        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoggerInfoDetailForm));
            this._Label1 = new System.Windows.Forms.Label();
            this._Label2 = new System.Windows.Forms.Label();
            this._Label3 = new System.Windows.Forms.Label();
            this._Label4 = new System.Windows.Forms.Label();
            this._LevelTextBox = new System.Windows.Forms.TextBox();
            this._TimeTextBox = new System.Windows.Forms.TextBox();
            this._SourceTextBox = new System.Windows.Forms.TextBox();
            this._LogInfoTextBox = new System.Windows.Forms.TextBox();
            this._LogStackTracePropertyGrid = new System.Windows.Forms.PropertyGrid();
            this._CloseButton = new System.Windows.Forms.Button();
            this._MainTabControl = new System.Windows.Forms.TabControl();
            this._MainPage = new System.Windows.Forms.TabPage();
            this._ExInfoPage = new System.Windows.Forms.TabPage();
            this._MainTabControl.SuspendLayout();
            this._MainPage.SuspendLayout();
            this._ExInfoPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // _Label1
            // 
            resources.ApplyResources(this._Label1, "_Label1");
            this._Label1.Name = "_Label1";
            // 
            // _Label2
            // 
            resources.ApplyResources(this._Label2, "_Label2");
            this._Label2.Name = "_Label2";
            // 
            // _Label3
            // 
            resources.ApplyResources(this._Label3, "_Label3");
            this._Label3.Name = "_Label3";
            // 
            // _Label4
            // 
            resources.ApplyResources(this._Label4, "_Label4");
            this._Label4.Name = "_Label4";
            // 
            // _LevelTextBox
            // 
            resources.ApplyResources(this._LevelTextBox, "_LevelTextBox");
            this._LevelTextBox.Name = "_LevelTextBox";
            this._LevelTextBox.ReadOnly = true;
            // 
            // _TimeTextBox
            // 
            resources.ApplyResources(this._TimeTextBox, "_TimeTextBox");
            this._TimeTextBox.Name = "_TimeTextBox";
            this._TimeTextBox.ReadOnly = true;
            // 
            // _SourceTextBox
            // 
            resources.ApplyResources(this._SourceTextBox, "_SourceTextBox");
            this._SourceTextBox.Name = "_SourceTextBox";
            this._SourceTextBox.ReadOnly = true;
            // 
            // _LogInfoTextBox
            // 
            resources.ApplyResources(this._LogInfoTextBox, "_LogInfoTextBox");
            this._LogInfoTextBox.Name = "_LogInfoTextBox";
            this._LogInfoTextBox.ReadOnly = true;
            // 
            // _LogStackTracePropertyGrid
            // 
            resources.ApplyResources(this._LogStackTracePropertyGrid, "_LogStackTracePropertyGrid");
            this._LogStackTracePropertyGrid.Name = "_LogStackTracePropertyGrid";
            // 
            // _CloseButton
            // 
            resources.ApplyResources(this._CloseButton, "_CloseButton");
            this._CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CloseButton.Name = "_CloseButton";
            this._CloseButton.UseVisualStyleBackColor = true;
            // 
            // _MainTabControl
            // 
            resources.ApplyResources(this._MainTabControl, "_MainTabControl");
            this._MainTabControl.Controls.Add(this._MainPage);
            this._MainTabControl.Controls.Add(this._ExInfoPage);
            this._MainTabControl.Name = "_MainTabControl";
            this._MainTabControl.SelectedIndex = 0;
            // 
            // _MainPage
            // 
            this._MainPage.Controls.Add(this._Label4);
            this._MainPage.Controls.Add(this._Label1);
            this._MainPage.Controls.Add(this._Label2);
            this._MainPage.Controls.Add(this._LogInfoTextBox);
            this._MainPage.Controls.Add(this._Label3);
            this._MainPage.Controls.Add(this._SourceTextBox);
            this._MainPage.Controls.Add(this._LevelTextBox);
            this._MainPage.Controls.Add(this._TimeTextBox);
            resources.ApplyResources(this._MainPage, "_MainPage");
            this._MainPage.Name = "_MainPage";
            this._MainPage.UseVisualStyleBackColor = true;
            // 
            // _ExInfoPage
            // 
            this._ExInfoPage.Controls.Add(this._LogStackTracePropertyGrid);
            resources.ApplyResources(this._ExInfoPage, "_ExInfoPage");
            this._ExInfoPage.Name = "_ExInfoPage";
            this._ExInfoPage.UseVisualStyleBackColor = true;
            // 
            // NLogDetailForm
            // 
            this.AcceptButton = this._CloseButton;
            this.CancelButton = this._CloseButton;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this._MainTabControl);
            this.Controls.Add(this._CloseButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NLogDetailForm";
            this._MainTabControl.ResumeLayout(false);
            this._MainPage.ResumeLayout(false);
            this._MainPage.PerformLayout();
            this._ExInfoPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}