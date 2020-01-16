using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MeterKnife.Util.GUI
{
    public class SimpleForm : Form
    {
        public SimpleForm()
        {
            this._Components = new System.ComponentModel.Container();

            this.SuspendLayout();
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Font = new Font("Tahoma", 8.25F);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(120, 140);
            this.Size = new Size(640, 480);
            this.Text = this.GetType().FullName;

            this.ResumeLayout(false);
        }

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private readonly IContainer _Components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (_Components != null))
            {
                _Components.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
