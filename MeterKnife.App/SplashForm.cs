using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using MeterKnife.Common.Properties;
using NKnife.Win.Forms.Forms;

namespace MeterKnife.App
{
    internal sealed class SplashForm : Form, ISplashForm
    {
        public SplashForm()
        {
            Bitmap bitmap = GlobalResources.MeterKnife2015_Welcome;

            SuspendLayout();

            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false;
            Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((0)));
            Width = bitmap.Width;
            Height = bitmap.Height;
            BackgroundImage = bitmap;

            var mainVersionLabel = new Label();
            mainVersionLabel.AutoSize = false;
            mainVersionLabel.BackColor = Color.Transparent;
            mainVersionLabel.ForeColor = Color.White;
            mainVersionLabel.Location = new Point(Width - 193, Height - 42);
            mainVersionLabel.TextAlign = ContentAlignment.MiddleRight;
            mainVersionLabel.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, ((0)));
            mainVersionLabel.Size = new Size(160, 18);
            mainVersionLabel.TabIndex = 0;
            mainVersionLabel.Text = string.Format("版本: {0}", AssemblyVersion);
            Controls.Add(mainVersionLabel);

            _StatusInfoLabel = new Label();
            _StatusInfoLabel.AutoSize = true;
            _StatusInfoLabel.BackColor = Color.Transparent;
            _StatusInfoLabel.ForeColor = Color.White;
            _StatusInfoLabel.Location = new Point(30, Height - 40);
            _StatusInfoLabel.Size = new Size(180, 13);
            _StatusInfoLabel.TabIndex = 0;
            _StatusInfoLabel.Text = "开始启动MeterKnife管理主控程序...";
            Controls.Add(_StatusInfoLabel);

            var updaterVersionLabel = new Label();
            updaterVersionLabel.BackColor = Color.Transparent;
            updaterVersionLabel.ForeColor = Color.White;
            updaterVersionLabel.Size = new Size(220, 13);
            updaterVersionLabel.Location = new Point(30, _StatusInfoLabel.Location.Y - 6 - updaterVersionLabel.Height);
            Controls.Add(updaterVersionLabel);

            var callerVersionLabel = new Label();
            callerVersionLabel.BackColor = Color.Transparent;
            callerVersionLabel.ForeColor = Color.White;
            callerVersionLabel.Size = new Size(220, 13);
            callerVersionLabel.Location = new Point(30, updaterVersionLabel.Location.Y - 4 - callerVersionLabel.Height);
            Controls.Add(callerVersionLabel);

            ResumeLayout(false);
            PerformLayout();
        }

        public string AssemblyVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        #region ISplashForm

        private readonly Label _StatusInfoLabel;

        void ISplashForm.SetStatusInfo(string newStatusInfo)
        {
            try
            {
                _StatusInfoLabel.Text = newStatusInfo;
                _StatusInfoLabel.Refresh();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion
    }
}