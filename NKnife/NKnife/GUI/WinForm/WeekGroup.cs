using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NKnife.GUI.WinForm
{
    public class WeekGroup : UserControl
    {
        public WeekGroup()
        {
            InitializeComponent();
        }

        public DayOfWeek[] GetWeeks()
        {
            var list = new List<DayOfWeek>(7);
            if (_CheckBox1.Checked)
                list.Add(DayOfWeek.Monday);
            if (_CheckBox2.Checked)
                list.Add(DayOfWeek.Tuesday);
            if (_CheckBox3.Checked)
                list.Add(DayOfWeek.Wednesday);
            if (_CheckBox4.Checked)
                list.Add(DayOfWeek.Thursday);
            if (_CheckBox5.Checked)
                list.Add(DayOfWeek.Friday);
            if (_CheckBox6.Checked)
                list.Add(DayOfWeek.Saturday);
            if (_CheckBox7.Checked)
                list.Add(DayOfWeek.Sunday);
            return list.ToArray();
        }

        public void SetWeek(params DayOfWeek[] week)
        {
            foreach (DayOfWeek dayOfWeek in week)
            {
                switch (dayOfWeek)
                {
                    case DayOfWeek.Monday:
                        _CheckBox1.Checked = true;
                        break;
                    case DayOfWeek.Tuesday:
                        _CheckBox2.Checked = true;
                        break;
                    case DayOfWeek.Wednesday:
                        _CheckBox3.Checked = true;
                        break;
                    case DayOfWeek.Thursday:
                        _CheckBox4.Checked = true;
                        break;
                    case DayOfWeek.Friday:
                        _CheckBox5.Checked = true;
                        break;
                    case DayOfWeek.Saturday:
                        _CheckBox6.Checked = true;
                        break;
                    case DayOfWeek.Sunday:
                        _CheckBox7.Checked = true;
                        break;
                }
            }
        }

        private FlowLayoutPanel _Panel;
        private CheckBox _CheckBox1;
        private CheckBox _CheckBox2;
        private CheckBox _CheckBox3;
        private CheckBox _CheckBox4;
        private CheckBox _CheckBox5;
        private CheckBox _CheckBox6;
        private CheckBox _CheckBox7;
    
        private void InitializeComponent()
        {
            this._Panel = new System.Windows.Forms.FlowLayoutPanel();
            this._CheckBox1 = new System.Windows.Forms.CheckBox();
            this._CheckBox2 = new System.Windows.Forms.CheckBox();
            this._CheckBox3 = new System.Windows.Forms.CheckBox();
            this._CheckBox4 = new System.Windows.Forms.CheckBox();
            this._CheckBox5 = new System.Windows.Forms.CheckBox();
            this._CheckBox6 = new System.Windows.Forms.CheckBox();
            this._CheckBox7 = new System.Windows.Forms.CheckBox();
            this._Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _Panel
            // 
            this._Panel.Controls.Add(this._CheckBox1);
            this._Panel.Controls.Add(this._CheckBox2);
            this._Panel.Controls.Add(this._CheckBox3);
            this._Panel.Controls.Add(this._CheckBox4);
            this._Panel.Controls.Add(this._CheckBox5);
            this._Panel.Controls.Add(this._CheckBox6);
            this._Panel.Controls.Add(this._CheckBox7);
            this._Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Panel.Location = new System.Drawing.Point(0, 0);
            this._Panel.Name = "_Panel";
            this._Panel.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this._Panel.Size = new System.Drawing.Size(300, 57);
            this._Panel.TabIndex = 0;
            // 
            // _CheckBox1
            // 
            this._CheckBox1.AutoSize = true;
            this._CheckBox1.Location = new System.Drawing.Point(13, 8);
            this._CheckBox1.Name = "_CheckBox1";
            this._CheckBox1.Size = new System.Drawing.Size(62, 17);
            this._CheckBox1.TabIndex = 0;
            this._CheckBox1.Text = "星期一";
            this._CheckBox1.UseVisualStyleBackColor = true;
            // 
            // _CheckBox2
            // 
            this._CheckBox2.AutoSize = true;
            this._CheckBox2.Location = new System.Drawing.Point(81, 8);
            this._CheckBox2.Name = "_CheckBox2";
            this._CheckBox2.Size = new System.Drawing.Size(62, 17);
            this._CheckBox2.TabIndex = 1;
            this._CheckBox2.Text = "星期二";
            this._CheckBox2.UseVisualStyleBackColor = true;
            // 
            // _CheckBox3
            // 
            this._CheckBox3.AutoSize = true;
            this._CheckBox3.Location = new System.Drawing.Point(149, 8);
            this._CheckBox3.Name = "_CheckBox3";
            this._CheckBox3.Size = new System.Drawing.Size(62, 17);
            this._CheckBox3.TabIndex = 2;
            this._CheckBox3.Text = "星期三";
            this._CheckBox3.UseVisualStyleBackColor = true;
            // 
            // _CheckBox4
            // 
            this._CheckBox4.AutoSize = true;
            this._CheckBox4.Location = new System.Drawing.Point(217, 8);
            this._CheckBox4.Name = "_CheckBox4";
            this._CheckBox4.Size = new System.Drawing.Size(62, 17);
            this._CheckBox4.TabIndex = 3;
            this._CheckBox4.Text = "星期四";
            this._CheckBox4.UseVisualStyleBackColor = true;
            // 
            // _CheckBox5
            // 
            this._CheckBox5.AutoSize = true;
            this._CheckBox5.Location = new System.Drawing.Point(13, 31);
            this._CheckBox5.Name = "_CheckBox5";
            this._CheckBox5.Size = new System.Drawing.Size(62, 17);
            this._CheckBox5.TabIndex = 4;
            this._CheckBox5.Text = "星期五";
            this._CheckBox5.UseVisualStyleBackColor = true;
            // 
            // _CheckBox6
            // 
            this._CheckBox6.AutoSize = true;
            this._CheckBox6.Location = new System.Drawing.Point(81, 31);
            this._CheckBox6.Name = "_CheckBox6";
            this._CheckBox6.Size = new System.Drawing.Size(62, 17);
            this._CheckBox6.TabIndex = 5;
            this._CheckBox6.Text = "星期六";
            this._CheckBox6.UseVisualStyleBackColor = true;
            // 
            // _CheckBox7
            // 
            this._CheckBox7.AutoSize = true;
            this._CheckBox7.Location = new System.Drawing.Point(149, 31);
            this._CheckBox7.Name = "_CheckBox7";
            this._CheckBox7.Size = new System.Drawing.Size(62, 17);
            this._CheckBox7.TabIndex = 6;
            this._CheckBox7.Text = "星期日";
            this._CheckBox7.UseVisualStyleBackColor = true;
            // 
            // WeekGroup
            // 
            this.Controls.Add(this._Panel);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "WeekGroup";
            this.Size = new System.Drawing.Size(300, 57);
            this._Panel.ResumeLayout(false);
            this._Panel.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
