using System;
using System.Drawing;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.EventParameters;
using MeterKnife.Common.Properties;

namespace MeterKnife.Common.Winforms.Controls.Tree
{
    public sealed class MeterTree : TreeView
    {
        private static readonly ILog _logger = LogManager.GetLogger<MeterTree>();
        private int _MouseClicks; //��¼�����TreeView�ؼ��ϰ��µĴ���

        public MeterTree()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            ImageList = GetImageList();
            ShowLines = false;
            FullRowSelect = true;

            MouseClick += OnMouseClick;
            DoubleClick += OnMouseDoubleClick;

            //�Զ�����ƽڵ���ı���ͼ��
            //DrawMode = TreeViewDrawMode.OwnerDrawText;
            //DrawNode += Tree_DrawNode;

            //ͨ�������ֶθı�ڵ���м�࣬ʹ�ø�����һЩ
            //const int TV_FIRST = 0x1100;
            //const int TVM_SETITEMHEIGHT = TV_FIRST + 27;
            //API.User32.SendMessage(Handle, TVM_SETITEMHEIGHT, 20, 0);
            ItemHeight = 23;

            //ͨ�������ֶ�ʹ�ý��㶪ʧʱ��ѡ�еĽڵ����и�����ʾ
            Leave += TreeLeave;
            BeforeSelect += TreeBeforeSelect;

            //Microsoft��TreeView�ؼ����������ŵ�����˫���ڵ�ʱ�Զ�չ��/�۵��ڵ㡣
            //�������Զ���NodeMouseDoubleClick�¼������ǣ�ͬʱ�ֲ�ϣ���ı����չ��/�۵�״̬�����޷�ֱ�Ӵﵽ��һЧ����
            //ͨ�������ֶ����߾ȹ���
            MouseDown += (s, e) => { _MouseClicks = e.Clicks; };
            BeforeCollapse += (s, e) => { e.Cancel = _MouseClicks > 1; };
            BeforeExpand += (s, e) => { e.Cancel = _MouseClicks > 1; };

            //���еĽڵ���Ϸ����
            //AllowDrop = true;
            //ItemDrag += OnItemDrag;
            //DragEnter += OnDragEnter;
            //DragOver += OnDragOver;
            //DragDrop += OnDragDrop;

            Nodes.Add(new PCNode());
        }

        public TreeNode RootNode
        {
            get { return Nodes[0]; }
        }

        private void OnMouseClick(object s, MouseEventArgs e)
        {
            //������¼����ݵ��ӽڵ�ȥʵ��
            if (SelectedNode != null)
            {
                if (SelectedNode is InterfaceNode)
                {
                    //������ӿڽڵ�ʱ��������������жϣ�ֱ�Ӵ��ݸ��ڵ㣬�ڽڵ����н����жϴ���
                    var node = SelectedNode as InterfaceNode;
                    node.OnNodeClicked(e);
                }
            }
        }

        private void OnMouseDoubleClick(object sender, EventArgs e)
        {
            if (SelectedNode is MeterNode)
            {
                //�����˫�������ڵ�
                var node = SelectedNode as MeterNode;
                var interfaceNode = (InterfaceNode) node.Parent;
                if (interfaceNode is LanNode)
                {
                    //TODO:interfaceNode.Port.TunnelType = TunnelType.Tcpip;
                }
                _logger.Trace("���˫�������ڵ�");
                OnSelectedMeter(new InterfaceNodeClickedEventArgs(node.Meter, interfaceNode.Port));
            }
        }

        public event EventHandler<InterfaceNodeClickedEventArgs> SelectedMeter;

        private void OnSelectedMeter(InterfaceNodeClickedEventArgs e)
        {
            EventHandler<InterfaceNodeClickedEventArgs> handler = SelectedMeter;
            if (handler != null) handler(this, e);
        }

        private ImageList GetImageList()
        {
            var il = new ImageList
            {
                ColorDepth = ColorDepth.Depth32Bit, 
                ImageSize = new Size(18, 18)
            };

            il.Images.Add(MeterTreeElement.PC, GlobalResources.pc);
            il.Images.Add(MeterTreeElement.Serial, GlobalResources.serial);
            il.Images.Add(MeterTreeElement.Care, GlobalResources.care);
            il.Images.Add(MeterTreeElement.Meter, GlobalResources.meter);
            return il;
        }

        #region ������ʱ����

        private bool _IsTreeLeave;

        private void TreeBeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            //�ڿ�ʼѡ���µĽڵ�ǰ�����ϴ�ѡ��Ľڵ㱳����Ĭ��״̬
            if (SelectedNode != null && _IsTreeLeave)
            {
                SelectedNode.BackColor = Color.White;
                _IsTreeLeave = false;
            }
        }

        private void TreeLeave(object sender, EventArgs e)
        {
            //�����㶪ʧʱ����ѡ�еĽڵ����и�����ʾ
            if (SelectedNode != null)
            {
                SelectedNode.BackColor = Color.Gainsboro;
                _IsTreeLeave = true;
            }
        }

        #endregion

        #region �Ի��ƽڵ�

        private void TreeDrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            Rectangle nodeRect = e.Node.Bounds; //�ڵ�����

            var drawPt = new Point(nodeRect.Location.X - 18, nodeRect.Location.Y); //����ͼ�����ʼλ��
            var imgSize = new Size(12, 12); //ͼƬ��С
            var imgRect = new Rectangle(drawPt, imgSize);

            //--------����ͼƬ: �жϽڵ����ͣ������ݸ��ڵ�����ͻ��Ʋ�ͬ��ͼƬ--------------------
            if (e.Node is BaseTreeNode)
            {
                //this.LegendIcon.Draw(e.Graphics, drawPt, 0);
            }

            //-----------------------�����ı� -------------------------------
            Font nodeFont = e.Node.NodeFont ?? ((TreeView) sender).Font;
            Brush textBrush = SystemBrushes.WindowText;
            //��ɫͻ����ʾ
            if ((e.State & TreeNodeStates.Focused) != 0)
                textBrush = SystemBrushes.Window;
            //���޶��ı��������������ʱ���ı�����ȡ----edited by: Vivi 2009/11/19
            e.Graphics.DrawString(e.Node.Text, nodeFont, textBrush, Rectangle.Inflate(nodeRect, -5, -5));
        }

        #endregion

        #region �ڵ���Ϸ����

        private TreeNode _LastNodeOnDrag; //����ǰһ��������Ľڵ�

        private void OnItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item != null)
                //�����Ϸ�����Ϊ�ƶ�
                DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            //��ȡ���϶��Ľڵ�
            TreeNode draggingNode = FindDragNode(e.Data);
            //�޸��������Ŀ��ڵ�ı���ɫ����ԭ��һ���ڵ�ı���ɫ
            TreeNode targetNode = GetNodeAt(PointToClient(new Point(e.X, e.Y)));
            if ((targetNode != null) && (targetNode != _LastNodeOnDrag))
            {
                targetNode.BackColor = Color.PaleTurquoise;
                _LastNodeOnDrag.BackColor = Color.White;
                _LastNodeOnDrag = targetNode;
                e.Effect = (CanDropNode(draggingNode, targetNode)) ? DragDropEffects.Move : DragDropEffects.None;
            }
        }

        /// <summary>
        ///     �ж�Ŀ��ڵ��Ƿ���Խ��ձ��϶��Ľڵ�
        /// </summary>
        /// <param name="draggingNode">���϶��Ľڵ�</param>
        /// <param name="targetNode">Ŀ��ڵ�</param>
        /// <returns></returns>
        private bool CanDropNode(TreeNode draggingNode, TreeNode targetNode)
        {
            return true;
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            //��ȡ���϶��Ľڵ�
            TreeNode draggingNode = FindDragNode(e.Data);
            //����ڵ������ݣ��Ϸ�Ŀ�������ƶ�
            e.Effect = (draggingNode != null) ? DragDropEffects.Move : DragDropEffects.None;
            TreeNode targetNode = GetNodeAt(PointToClient(new Point(e.X, e.Y)));
            if (targetNode != null)
            {
                //�ı����ڵ�ı���ɫ
                targetNode.BackColor = Color.Khaki;
                //����˽ڵ㣬������һ��ʱ��ԭ����ɫ
                _LastNodeOnDrag = targetNode;
            }
        }

        private static TreeNode FindDragNode(IDataObject dataObject)
        {
            string[] types = dataObject.GetFormats(false);
            foreach (string type in types)
            {
                if (!type.Contains("Node"))
                {
                    continue;
                }
                object obj = dataObject.GetData(type);
                var node = obj as TreeNode;
                if (node != null)
                {
                    return node;
                }
            }
            return null;
        }

        private void OnDragDrop(object sender, DragEventArgs e)
        {
            //���ϷŽ���ʱ
            TreeNode node = FindDragNode(e.Data);
            //�õ���ǰ������Ľڵ�
            TreeNode targetNode = GetNodeAt(PointToClient(new Point(e.X, e.Y)));
            if (targetNode != null)
            {
                //ɾ���ϷŵĽڵ�
                node.Remove();
                //���ӵ�Ŀ��ڵ�
                targetNode.Nodes.Add(node);
                targetNode.BackColor = Color.White;
                SelectedNode = targetNode;
            }
        }

        #endregion
    }
}