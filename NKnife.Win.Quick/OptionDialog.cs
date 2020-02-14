using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.Win.Quick.Base;
using NLog;

namespace NKnife.Win.Quick
{
    public sealed partial class OptionDialog : Form
    {
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();
        private bool _isFirst = true;
        private IWorkbench _workbench;
        private readonly ImageList _imageList = new ImageList();
        private readonly Dictionary<string, Control> _panelMap = new Dictionary<string, Control>();

        public OptionDialog()
        {
            InitializeComponent();
            Text = this.Res("选项");
            _acceptButton.Text = this.Res("确定");
            _cancelButton.Text = this.Res("取消");
            ResponseToEvent();
        }

        public void InitializeOptionPanelList(IWorkbench workbench)
        {
            if (!_isFirst)
                return;
            _workbench = workbench;
            foreach (IOptionPanel optionPanel in workbench.OptionPanelList)
            {
                if (!(optionPanel is Control control))
                    return;
                var treeNode = new TreeNode(optionPanel.Name);
                optionPanel.Initialize(workbench.GetOptionValueFunc);
                control.Dock = DockStyle.Fill;
                _panelMap.Add(optionPanel.Name, control);
                if (optionPanel.Icon != null)
                {
                    _imageList.Images.Add(optionPanel.Name, optionPanel.Icon);
                    treeNode.ImageKey = optionPanel.Name;
                }

                _leftTreeView.Nodes.Add(treeNode);
            }
            _isFirst = false;
        }

        private void ResponseToEvent()
        {
            _leftTreeView.NodeMouseClick += (s, e) =>
            {
                if (e.Node != null)
                {
                    _mainPanel.Controls.Clear();
                    _mainPanel.Controls.Add(_panelMap[e.Node.Text]);
                }
            };
            _acceptButton.Click += (s, e) =>
            {
                var num = 0;
                if (_workbench != null)
                {
                    foreach (Control control in _panelMap.Values)
                    {
                        if (control is IOptionPanel optionPanel)
                        {
                            if (optionPanel.HasDataChanged)
                            {
                                foreach (var pair in optionPanel.OptionMap)
                                {
                                    _workbench.SetOptionAction(pair.Key, pair.Value);
                                    num++;
                                    _Logger.Info($"新选项值保存完成。{pair.Key}: {pair.Value}");
                                }
                            }
                        }
                    }
                }

                MessageBox.Show(this.ResF("共有{0}个选项值发生变化，已保存成功。", num), this.Res("选项保存"),
                    MessageBoxButtons.OK,MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            };
            _cancelButton.Click += (s, e) => { DialogResult = DialogResult.Cancel; };
        }
    }
}
