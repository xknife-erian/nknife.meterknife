using MeterKnife.Plots.Themes;

namespace MeterKnife.Views.Measures
{
    partial class DataSeriesSettingDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this._ExhibitsComboBox = new System.Windows.Forms.ComboBox();
            this._LineStyleComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._ThicknessNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.themeColorItem1 = new MeterKnife.Plots.Themes.ThemeColorItem();
            this.themeColorItem2 = new MeterKnife.Plots.Themes.ThemeColorItem();
            this.label5 = new System.Windows.Forms.Label();
            this.themeColorItem3 = new MeterKnife.Plots.Themes.ThemeColorItem();
            this.label6 = new System.Windows.Forms.Label();
            this.horizontalLine1 = new NKnife.ControlKnife.HorizontalLine();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._ThicknessNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.listView1.Location = new System.Drawing.Point(13, 94);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(445, 206);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 24;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "数据名称";
            this.columnHeader2.Width = 110;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "类型";
            this.columnHeader3.Width = 65;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "线宽";
            this.columnHeader4.Width = 65;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "线颜色";
            this.columnHeader5.Width = 50;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "标记框";
            this.columnHeader6.Width = 50;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "标记体";
            this.columnHeader7.Width = 50;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(354, 305);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 27);
            this.button1.TabIndex = 1;
            this.button1.Text = "确定(&C)";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // _ExhibitsComboBox
            // 
            this._ExhibitsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._ExhibitsComboBox.FormattingEnabled = true;
            this._ExhibitsComboBox.Location = new System.Drawing.Point(14, 31);
            this._ExhibitsComboBox.Name = "_ExhibitsComboBox";
            this._ExhibitsComboBox.Size = new System.Drawing.Size(121, 21);
            this._ExhibitsComboBox.TabIndex = 2;
            // 
            // _LineStyleComboBox
            // 
            this._LineStyleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._LineStyleComboBox.FormattingEnabled = true;
            this._LineStyleComboBox.Location = new System.Drawing.Point(151, 31);
            this._LineStyleComboBox.Name = "_LineStyleComboBox";
            this._LineStyleComboBox.Size = new System.Drawing.Size(63, 21);
            this._LineStyleComboBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "数据名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "类型";
            // 
            // _ThicknessNumericUpDown
            // 
            this._ThicknessNumericUpDown.DecimalPlaces = 1;
            this._ThicknessNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this._ThicknessNumericUpDown.Location = new System.Drawing.Point(219, 31);
            this._ThicknessNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._ThicknessNumericUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this._ThicknessNumericUpDown.Name = "_ThicknessNumericUpDown";
            this._ThicknessNumericUpDown.Size = new System.Drawing.Size(56, 21);
            this._ThicknessNumericUpDown.TabIndex = 8;
            this._ThicknessNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(216, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "线宽";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(276, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "颜色";
            // 
            // themeColorItem1
            // 
            this.themeColorItem1.BackColor = System.Drawing.Color.White;
            this.themeColorItem1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.themeColorItem1.Color = System.Drawing.Color.White;
            this.themeColorItem1.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.themeColorItem1.Location = new System.Drawing.Point(279, 29);
            this.themeColorItem1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.themeColorItem1.Name = "themeColorItem1";
            this.themeColorItem1.Size = new System.Drawing.Size(57, 23);
            this.themeColorItem1.TabIndex = 11;
            // 
            // themeColorItem2
            // 
            this.themeColorItem2.BackColor = System.Drawing.Color.White;
            this.themeColorItem2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.themeColorItem2.Color = System.Drawing.Color.White;
            this.themeColorItem2.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.themeColorItem2.Location = new System.Drawing.Point(340, 29);
            this.themeColorItem2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.themeColorItem2.Name = "themeColorItem2";
            this.themeColorItem2.Size = new System.Drawing.Size(57, 23);
            this.themeColorItem2.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(337, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "颜色";
            // 
            // themeColorItem3
            // 
            this.themeColorItem3.BackColor = System.Drawing.Color.White;
            this.themeColorItem3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.themeColorItem3.Color = System.Drawing.Color.White;
            this.themeColorItem3.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.themeColorItem3.Location = new System.Drawing.Point(401, 29);
            this.themeColorItem3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.themeColorItem3.Name = "themeColorItem3";
            this.themeColorItem3.Size = new System.Drawing.Size(57, 23);
            this.themeColorItem3.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(398, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "颜色";
            // 
            // horizontalLine1
            // 
            this.horizontalLine1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.horizontalLine1.BackColor = System.Drawing.Color.Transparent;
            this.horizontalLine1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.horizontalLine1.LineColor = System.Drawing.SystemColors.WindowFrame;
            this.horizontalLine1.Location = new System.Drawing.Point(14, 59);
            this.horizontalLine1.Name = "horizontalLine1";
            this.horizontalLine1.PanelAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.horizontalLine1.Size = new System.Drawing.Size(445, 29);
            this.horizontalLine1.TabIndex = 16;
            this.horizontalLine1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.horizontalLine1.TextFont = new System.Drawing.Font("Tahoma", 9F);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(384, 64);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 17;
            this.button2.Text = "移除";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(303, 64);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 18;
            this.button3.Text = "添加";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // DataSeriesSettingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 344);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.horizontalLine1);
            this.Controls.Add(this.themeColorItem3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.themeColorItem2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.themeColorItem1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._ThicknessNumericUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._LineStyleComboBox);
            this.Controls.Add(this._ExhibitsComboBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataSeriesSettingDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "数据线选择";
            ((System.ComponentModel.ISupportInitialize)(this._ThicknessNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox _ExhibitsComboBox;
        private System.Windows.Forms.ComboBox _LineStyleComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown _ThicknessNumericUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private ThemeColorItem themeColorItem1;
        private ThemeColorItem themeColorItem2;
        private ThemeColorItem themeColorItem3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private NKnife.ControlKnife.HorizontalLine horizontalLine1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}