using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace NKnife.GUI.WinForm.WinFormContainer
{
    public class AppFileNameEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
            //return base.GetEditStyle(context);
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var service = provider.GetService(
                typeof(IWindowsFormsEditorService)) as
                IWindowsFormsEditorService;

            if (service != null)
            {
                var fileDlg = new OpenFileDialog {Filter = "可执行程序 (*.exe)|*.exe", Multiselect = false};
                return fileDlg.ShowDialog() == DialogResult.OK ? fileDlg.FileName : string.Empty;
            }
            return value;
            //return base.EditValue(context, provider, value);
        }
    }
}
