using System.Collections;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;

namespace NKnife.GUI.WinForm.IPAddressControl
{
   class IPAddressControlDesigner : ControlDesigner
   {
      public override SelectionRules SelectionRules
      {
         get
         {
            var control = (IpAddressControl)Control;

            if ( control.AutoHeight )
            {
               return SelectionRules.Moveable | SelectionRules.Visible | SelectionRules.LeftSizeable | SelectionRules.RightSizeable;
            }
            else
            {
               return SelectionRules.AllSizeable | SelectionRules.Moveable | SelectionRules.Visible;
            }
         }
      }

      public override IList SnapLines
      {
         get
         {
            NKnife.GUI.WinForm.IPAddressControl.IpAddressControl control = (NKnife.GUI.WinForm.IPAddressControl.IpAddressControl)Control;

            IList snapLines = base.SnapLines;

            snapLines.Add( new SnapLine( SnapLineType.Baseline, control.Baseline ) );

            return snapLines;
         }
      }
   }
}