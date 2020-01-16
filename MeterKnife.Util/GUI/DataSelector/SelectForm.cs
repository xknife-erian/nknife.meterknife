using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MeterKnife.Util.GUI.DataSelector
{
    internal class SelectForm<T> : SimpleForm where T : class
    {
        private readonly Func<IQueryable<T>> _QueryFunc;

        public SelectForm(Func<IQueryable<T>> queryFunc)
        {
            _QueryFunc = queryFunc;
            InitializeComponent();
            RegistEvent();
        }

        public SingleRowCheckedListView ListView { get; private set; }

        public string EntityName { private get; set; }

        private void RegistEvent()
        {
            //搜索
            _FindBtn.Click += (sender, e) =>
                              {
                                  if (!string.IsNullOrWhiteSpace(_FindKeyTbox.Text))
                                      Find(_FindKeyTbox.Text, ListView);
                              };
            //确定
            _OkBtn.Click += (sender, e) =>
                            {
                                DialogResult = DialogResult.Cancel;
                                Close();
                            };
            //取消
            _CancelBtn.Click += (sender, e) =>
                                {
                                    DialogResult = DialogResult.Cancel;
                                    Close();
                                };
        }

        private static void Find(string text, ListView view)
        {
            List<ListViewItem> list = view.Items.Cast<ListViewItem>().Where(item => !item.SubItems[0].Text.Contains(text)).ToList();
            foreach (ListViewItem item in list)
            {
                view.Items.Remove(item);
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = string.Format("{0}列表", EntityName);
            IQueryable<T> tarray = _QueryFunc.Invoke();
            foreach (T t in tarray)
            {
                var item = new ListViewItem();
                item.SubItems.Add(t.ToString());
                item.Tag = t;
                ListView.Items.Add(item);
            }
        }

        #region 窗体设计

        private Button _CancelBtn;
        private Button _FindBtn;
        private TextBox _FindKeyTbox;
        private ColumnHeader _NameHeader;
        private Button _OkBtn;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ListView = new SingleRowCheckedListView();
            this._NameHeader = ((System.Windows.Forms.ColumnHeader) (new System.Windows.Forms.ColumnHeader()));
            this._FindBtn = new System.Windows.Forms.Button();
            this._OkBtn = new System.Windows.Forms.Button();
            this._CancelBtn = new System.Windows.Forms.Button();
            this._FindKeyTbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _View
            // 
            this.ListView.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                           | System.Windows.Forms.AnchorStyles.Left)
                                                                          | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView.CheckBoxes = true;
            this.ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[]
                                           {
                                               this._NameHeader
                                           });
            this.ListView.FullRowSelect = true;
            this.ListView.GridLines = true;
            this.ListView.Location = new System.Drawing.Point(12, 12);
            this.ListView.MultiSelect = false;
            this.ListView.Name = "_View";
            this.ListView.ShowGroups = false;
            this.ListView.Size = new System.Drawing.Size(332, 319);
            this.ListView.TabIndex = 0;
            this.ListView.UseCompatibleStateImageBehavior = false;
            this.ListView.View = System.Windows.Forms.View.Details;
            // 
            // _NameHeader
            // 
            this._NameHeader.Text = "公司名称";
            this._NameHeader.Width = 270;
            // 
            // _FindBtn
            // 
            this._FindBtn.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._FindBtn.Location = new System.Drawing.Point(350, 334);
            this._FindBtn.Name = "_FindBtn";
            this._FindBtn.Size = new System.Drawing.Size(67, 26);
            this._FindBtn.TabIndex = 1;
            this._FindBtn.Text = "搜索";
            this._FindBtn.UseVisualStyleBackColor = true;
            // 
            // _OkBtn
            // 
            this._OkBtn.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._OkBtn.Location = new System.Drawing.Point(350, 12);
            this._OkBtn.Name = "_OkBtn";
            this._OkBtn.Size = new System.Drawing.Size(67, 33);
            this._OkBtn.TabIndex = 2;
            this._OkBtn.Text = "确定";
            this._OkBtn.UseVisualStyleBackColor = true;
            // 
            // _CancelBtn
            // 
            this._CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelBtn.Location = new System.Drawing.Point(350, 48);
            this._CancelBtn.Name = "_CancelBtn";
            this._CancelBtn.Size = new System.Drawing.Size(67, 33);
            this._CancelBtn.TabIndex = 3;
            this._CancelBtn.Text = "取消";
            this._CancelBtn.UseVisualStyleBackColor = true;
            // 
            // _FindKeyTbox
            // 
            this._FindKeyTbox.Location = new System.Drawing.Point(12, 337);
            this._FindKeyTbox.Name = "_FindKeyTbox";
            this._FindKeyTbox.Size = new System.Drawing.Size(332, 21);
            this._FindKeyTbox.TabIndex = 4;
            // 
            // CompanySelectForm
            // 
            this.AcceptButton = this._OkBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelBtn;
            this.ClientSize = new System.Drawing.Size(426, 373);
            this.ControlBox = false;
            this.Controls.Add(this._FindKeyTbox);
            this.Controls.Add(this._CancelBtn);
            this.Controls.Add(this._OkBtn);
            this.Controls.Add(this._FindBtn);
            this.Controls.Add(this.ListView);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        #endregion
    }
}