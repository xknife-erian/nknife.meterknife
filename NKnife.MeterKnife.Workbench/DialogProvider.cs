using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using NKnife.Interface;
using NKnife.MeterKnife.Holistic;
using NKnife.MeterKnife.Workbench.Base;

namespace NKnife.MeterKnife.Workbench
{
    public class DialogProvider : IDialogProvider
    {
        public T New<T>()
        {
            return Kernel.Container.Resolve<T>();
        }
    }
}
