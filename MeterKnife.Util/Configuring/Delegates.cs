using System;
using NKnife.Configuring.Common;

namespace NKnife.Configuring
{

    public delegate void OptionManagerInitializedEventHandler(EventArgs e);

    public delegate void OptionLoadingEventHandler(object sender, OptionLoadEventArgs e);

    public delegate void OptionLoadedEventHandler(object sender, OptionLoadEventArgs e);

    public delegate void OptionTableChangedEventHandler(object sender, OptionTableChangedEventArgs e);

}