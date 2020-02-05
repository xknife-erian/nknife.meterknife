using System;
using System.Configuration;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using NKnife.MeterKnife.App.Properties;
using NKnife.Win.Forms.Forms;
using NLog;

namespace NKnife.MeterKnife.App
{
    internal sealed class SplashForm : Form, ISplashForm
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        public SplashForm()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            Bitmap bitmap = Resources.MK_Welcome;

            SuspendLayout();

            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false;

            Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((0)));
            Width = bitmap.Width;
            Height = bitmap.Height;
            BackgroundImage = bitmap;

            var logoLabel = new Label();
            logoLabel.AutoSize = false;
            logoLabel.BackColor = Color.Transparent;
            logoLabel.ForeColor = Color.White;
            logoLabel.Location = new Point(30, 60);
            logoLabel.TextAlign = ContentAlignment.MiddleLeft;
            logoLabel.Font = new Font("Century", 35F, FontStyle.Bold, GraphicsUnit.Point, ((0)));
            logoLabel.Size = new Size(500, 60);
            logoLabel.TabIndex = 0;
            logoLabel.Text = $"MeterKnife Ant";

            var mainVersionLabel = new Label();
            mainVersionLabel.AutoSize = false;
            mainVersionLabel.BackColor = Color.Transparent;
            mainVersionLabel.ForeColor = Color.White;
            mainVersionLabel.Location = new Point(Width - 193, Height - 42);
            mainVersionLabel.TextAlign = ContentAlignment.MiddleRight;
            mainVersionLabel.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, ((0)));
            mainVersionLabel.Size = new Size(160, 18);
            mainVersionLabel.TabIndex = 0;
            mainVersionLabel.Text = $"版本: {AssemblyVersion}";

            string projectName = string.Empty;
            try
            {
                var reader = new AppSettingsReader();
                projectName = reader.GetValue("projectName", typeof (string)).ToString();
            }
            catch (Exception)
            {
                _Logger.Warn("读取产品所属项目名称失败");
            }
            if (!string.IsNullOrWhiteSpace(projectName))
            {
                var projectNameLabel = new Label();
                projectNameLabel.AutoSize = false;
                projectNameLabel.BackColor = Color.Transparent;
                projectNameLabel.ForeColor = Color.White;
                projectNameLabel.Location = new Point(Width - 291, Height - 62);
                projectNameLabel.TextAlign = ContentAlignment.MiddleRight;
                projectNameLabel.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, ((0)));
                projectNameLabel.Size = new Size(260, 18);
                projectNameLabel.TabIndex = 0;
                string projectLabelText = string.Empty;
                if (projectName != "预览版" && projectName != "正式版")
                    projectLabelText = "项目: ";
                projectNameLabel.Text = $"{projectLabelText}{projectName}";
                Controls.Add(projectNameLabel);
            }

            _statusInfoLabel = new Label();
            _statusInfoLabel.AutoSize = true;
            _statusInfoLabel.BackColor = Color.Transparent;
            _statusInfoLabel.ForeColor = Color.White;
            _statusInfoLabel.Location = new Point(30, Height - 40);
            _statusInfoLabel.Size = new Size(180, 18);
            _statusInfoLabel.TabIndex = 0;
            _statusInfoLabel.Text = "开始启动主控程序...";

            Controls.Add(logoLabel);
            Controls.Add(mainVersionLabel);
            Controls.Add(_statusInfoLabel);

            ResumeLayout(false);
            PerformLayout();
            Update();
        }

        public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        #region ISplashForm

        private readonly Label _statusInfoLabel;

        void ISplashForm.SetStatusInfo(string newStatusInfo)
        {
            try
            {
                _statusInfoLabel.Text = newStatusInfo;
                _statusInfoLabel.Refresh();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion
    }
}