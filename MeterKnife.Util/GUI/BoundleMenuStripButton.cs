using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MeterKnife.Util.GUI
{
    public class BoundleMenuStripButton : ToolStripMenuItem
    {
        private readonly ToolStripButton _ToolItem;
        private bool _EnableBoundle = true;

        public BoundleMenuStripButton()
        {
            _ToolItem = new ToolStripButton("", null, OnButtonItemClick);
            _ToolItem.ImageTransparentColor = Color.Fuchsia;
        }

        public BoundleMenuStripButton(string text)
            : base(text)
        {
            _ToolItem = new ToolStripButton(text, null, OnButtonItemClick);
            _ToolItem.ImageTransparentColor = Color.Fuchsia;

        }

        public BoundleMenuStripButton(string text, Image image, EventHandler onClick, string name)
            : base(text, image, onClick, name)
        {
            _ToolItem = new ToolStripButton(text, image, OnButtonItemClick, name);
            _ToolItem.ImageTransparentColor = Color.Fuchsia;
        }

        public override Image Image
        {
            get { return base.Image; }
            set
            {
                base.Image = value;
                _ToolItem.Image = value;
            }
        }

        public ToolStripButton BuddyToolStripItem
        {
            get { return _ToolItem; }
        }

        /// <summary>是否绑定到工具栏
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable boundle]; otherwise, <c>false</c>.
        /// </value>
        [Category("行为")]
        public bool EnableBoundle
        {
            get { return _EnableBoundle; }
            set
            {
                _EnableBoundle = value;
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            _ToolItem.Enabled = Enabled;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            _ToolItem.Visible = Visible;
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);
            _ToolItem.Checked = Checked;
        }

        protected override void OnCheckStateChanged(EventArgs e)
        {
            base.OnCheckStateChanged(e);
            _ToolItem.CheckState = CheckState;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            var tooltext = Text;
            int i = Text.IndexOf('(');
            if (i > 0)
                tooltext = Text.Substring(0, i);
            _ToolItem.Text = tooltext;
        }

        protected void OnButtonItemClick(object sender, EventArgs e)
        {
            OnClick(e);
        }

        /// <summary>获取指定菜单栏所有绑定项
        /// </summary>
        /// <param name="menu">The menu.</param>
        /// <returns></returns>
        public static IEnumerable<ToolStrip> GetBingStrips(ToolStrip menu)
        {
            var toolStripList = new List<ToolStrip>();
            foreach (ToolStripMenuItem stripItem in menu.Items)
            {
                var toolStrip = new ToolStrip();
                for (int i = 0; i < stripItem.DropDownItems.Count; i++)
                {
                    var item = stripItem.DropDownItems[i];
                    if (item is BoundleMenuStripButton)
                    {
                        var barItem = ((BoundleMenuStripButton)item).BuddyToolStripItem;
                        if (!((BoundleMenuStripButton)item).EnableBoundle)
                            continue;
                        if (barItem.Image != null)
                            barItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
                        else
                            barItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
                        toolStrip.Items.Add(barItem);
                    }
//                    else if(item is ToolStripSeparator)
//                        toolStrip.Items.Add(item);
                }
                if (toolStrip.Items.Count > 0)
                    toolStripList.Add(toolStrip);
            }
            var array = toolStripList.ToArray();
            Array.Reverse(array);
            return array;
        }
    }
}