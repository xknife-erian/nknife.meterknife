using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Workbench.Properties;

namespace NKnife.MeterKnife.Workbench.Controls
{
    public class EngineeringTree : TreeView
    {
        private readonly ImageList _imageList = new ImageList();

        public EngineeringTree()
        {
            FullRowSelect = true;
            InitializeImageList();
        }

        private void InitializeImageList()
        {
            _imageList.Images.Add(nameof(EngineeringCreateTimeTreeNode), Resources.node_date);
            _imageList.Images.Add(nameof(EngineeringTreeNode), Resources.node_eng);
            _imageList.Images.Add(nameof(DUTTreeNode), Resources.node_dut1);
            _imageList.Images.Add(Resources.node_dut2);
            _imageList.Images.Add(Resources.node_dut3);
            _imageList.Images.Add(Resources.node_dut4);
            _imageList.Images.Add(Resources.node_dut5);
            ImageList = _imageList;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            SendMessage(this.Handle, TVM_SETEXTENDEDSTYLE, (IntPtr)TVS_EX_DOUBLEBUFFER, (IntPtr)TVS_EX_DOUBLEBUFFER);
            base.OnHandleCreated(e);
        }
        private const int TVM_SETEXTENDEDSTYLE = 0x1100 + 44;
        private const int TVM_GETEXTENDEDSTYLE = 0x1100 + 45;
        private const int TVS_EX_DOUBLEBUFFER = 0x0004;
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
    }

    public class EngineeringTreeNode : TreeNode
    {
        public EngineeringTreeNode(Engineering engineering)
            : this(engineering.Name)
        {
            Engineering = engineering;
            foreach (var pool in engineering.CommandPools)
            {
                foreach (var command in pool)
                {
                    if (command.DUT != null)
                    {
                        var dutNode = new DUTTreeNode(command.DUT.Name) {DUT = command.DUT};
                        this.Nodes.Add(dutNode);
                    }
                }
            }
        }

        public EngineeringTreeNode(string text) 
            : base(text)
        {
            this.ImageKey = nameof(EngineeringTreeNode);
            this.SelectedImageKey = nameof(EngineeringTreeNode);
            //this.StateImageKey = nameof(EngineeringTreeNode);
        }

        public Engineering Engineering { get; set; }
    }

    public class EngineeringCreateTimeTreeNode : TreeNode
    {
        public EngineeringCreateTimeTreeNode(DateTime createTime)
            : this(createTime.ToString("yyyy-MM"))
        {
            CreateTime = createTime;
        }

        public EngineeringCreateTimeTreeNode(string text) 
            : base(text)
        {
            this.ImageKey = nameof(EngineeringCreateTimeTreeNode);
            this.SelectedImageKey = nameof(EngineeringCreateTimeTreeNode);
            //this.StateImageKey = nameof(EngineeringCreateTimeTreeNode);
        }

        public DateTime CreateTime { get; set; }
    }

    public class DUTTreeNode : TreeNode
    {
        public DUTTreeNode(string text)
            : base(text)
        {
            this.ImageKey = nameof(DUTTreeNode);
            this.SelectedImageKey = nameof(DUTTreeNode);
            //this.StateImageKey = nameof(DUTTreeNode);
        }

        public DUT DUT { get; set; }
    }
}
