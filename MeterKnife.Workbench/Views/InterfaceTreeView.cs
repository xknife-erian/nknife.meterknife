using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Common.Base;
using NKnife.IoC;
using NKnife.NLog3.Controls;
using NKnife.Wrapper;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Workbench.Views
{
    public partial class InterfaceTreeView : DockContent
    {
        public InterfaceTreeView()
        {
            InitializeComponent();
            var comlist = PcInterfaces.GetSerialList();
            foreach (var com in comlist)
            {
                var treenode = new TreeNode(com);
                _Tree.Nodes.Add(treenode);
            }
            var careComm = DI.Get<BaseCareCommunicationService>();
            careComm.SerialInitialized += (s, e) =>
            {
                
            };
        }
    }
}
