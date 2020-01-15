using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace NKnife.GUI.WinForm
{
    /// <summary>一个显示XML的树
    /// </summary>
    public class XmlTreeView : TreeView
    {
        public TreeNode BindXml(string xmlstring)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xmlstring);
            var treenode = new TreeNode();
            if (doc.DocumentElement != null)
            {
                treenode.Text = GetNodeText(doc.DocumentElement);
                BindXmlDocument(doc.DocumentElement, treenode);
                Nodes.Clear();
                Nodes.Add(treenode);
                return treenode;
            }
            return null;
        }

        public void BindXmlDocument(XmlNode xmlNode, TreeNode treeNode)
        {
            if (xmlNode== null)
            {
                return;
            }
            foreach (XmlNode subnode in xmlNode.ChildNodes)
            {
                switch (subnode.NodeType)
                {
                    case XmlNodeType.Element:
                    {
                        var nodeText = GetNodeText(subnode);
                        var newtreeNode = new TreeNode(nodeText);
                        treeNode.Nodes.Add(newtreeNode);
                        if (subnode.HasChildNodes)
                        {
                            BindXmlDocument(subnode, newtreeNode);
                        }
                        break;
                    }
                    case XmlNodeType.Text:
                    {
                        if (!string.IsNullOrWhiteSpace(subnode.Value))
                        {
                            var valueNode = new TreeNode(subnode.Value);
                            treeNode.Nodes.Add(valueNode);
                        }
                        break;
                    }
                }

            }
        }

        private string GetNodeText(XmlNode node)
        {
            var sb = new StringBuilder();
            sb.Append(node.LocalName).Append(" ");
            if (node.Attributes != null && node.Attributes.Count >= 0)
            {
                foreach (XmlAttribute attribute in node.Attributes)
                {
                    sb.Append(string.Format("{0}=\"{1}\"", attribute.LocalName, attribute.Value)).Append(' ');
                }
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Insert(0, '<');
            sb.Append('>');
            return sb.ToString();
        }
    }
}