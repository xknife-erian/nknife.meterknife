namespace MeterKnife.Views.InstrumentsDiscovery
{
    partial class InstrumentsDiscoveryView
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("LAN(TCPIP0)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("USB(USB0)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("LAN(Care1)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Serial10(Care1)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("34401");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("K2000");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("K2100");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("6871");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("6551");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstrumentsDiscoveryView));
            this._SplitContainer = new System.Windows.Forms.SplitContainer();
            this._ListView = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this._SplitContainer)).BeginInit();
            this._SplitContainer.Panel1.SuspendLayout();
            this._SplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // _SplitContainer
            // 
            this._SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._SplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this._SplitContainer.Location = new System.Drawing.Point(0, 0);
            this._SplitContainer.Name = "_SplitContainer";
            // 
            // _SplitContainer.Panel1
            // 
            this._SplitContainer.Panel1.Controls.Add(this._ListView);
            this._SplitContainer.Size = new System.Drawing.Size(851, 505);
            this._SplitContainer.SplitterDistance = 360;
            this._SplitContainer.SplitterWidth = 6;
            this._SplitContainer.TabIndex = 0;
            // 
            // _ListView
            // 
            this._ListView.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup1.Header = "LAN(TCPIP0)";
            listViewGroup1.Name = "_LanGroup";
            listViewGroup2.Header = "USB(USB0)";
            listViewGroup2.Name = "_UsbGroup";
            listViewGroup3.Header = "LAN(Care1)";
            listViewGroup3.Name = "_LanCareOneGroup";
            listViewGroup4.Header = "Serial10(Care1)";
            listViewGroup4.Name = "_SerialCareGroup";
            this._ListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4});
            listViewItem1.Group = listViewGroup1;
            listViewItem2.Group = listViewGroup3;
            listViewItem3.Group = listViewGroup2;
            listViewItem4.Group = listViewGroup4;
            listViewItem5.Group = listViewGroup1;
            this._ListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5});
            this._ListView.Location = new System.Drawing.Point(0, 0);
            this._ListView.Name = "_ListView";
            this._ListView.Size = new System.Drawing.Size(360, 505);
            this._ListView.TabIndex = 0;
            this._ListView.UseCompatibleStateImageBehavior = false;
            // 
            // InstrumentsDiscoveryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 505);
            this.Controls.Add(this._SplitContainer);
            this.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "InstrumentsDiscoveryView";
            this.Text = "仪器管理";
            this._SplitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._SplitContainer)).EndInit();
            this._SplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer _SplitContainer;
        private System.Windows.Forms.ListView _ListView;
    }
}