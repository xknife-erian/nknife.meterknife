
using System;

namespace NKnife.GUI.WinForm.IPAddressControl
{
   public class FieldChangedEventArgs : EventArgs
   {
      private int _fieldIndex;
      private String _text;

      public int FieldIndex
      {
         get { return _fieldIndex; }
         set { _fieldIndex = value; }
      }

      public String Text
      {
         get { return _text; }
         set { _text = value; }
      }
   }
}
