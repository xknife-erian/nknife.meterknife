using System.Drawing;
using System.Windows.Forms;
using NKnife.Interface;

namespace NKnife.GUI.WinForm.CloudsControl
{
    public class CloudLabel : Control
    {
        internal int LineNumber { get; private set; }
        public int Number { get; private set; }
        public IGenerator Cloud { get; set; }
        public string Text
        {
            get
            {
                if (this.Cloud == null)
                {
                    return "";
                }
                return this.Cloud.Generator();
            }
        }

        public CloudLabel(IGenerator cloud, int number, int width, int height) :
            this(cloud, number, new SizeF(width, height)) { }

        public CloudLabel(IGenerator cloud, int number, SizeF sizeF)
        {
            this.Cloud = cloud;
            this.Number = number;
            this.Size = Size.Ceiling(sizeF);
        }

        public CloudLabel(IGenerator cloud, int number) :
            this(cloud, number, SizeF.Empty)
        {
        }
    }
}
