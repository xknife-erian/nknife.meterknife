using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NKnife.GUI.WinForm;

namespace NKnife.Configuring.Interfaces
{
    public interface IOption
    {
        string Category { get; set; }
        StringBuilder AsXml { get; }
        bool IsModified { get; set; }
        void Update();
        void WriteXml(string filefullname, XmlWriteMode writeSchema);
    }
}
