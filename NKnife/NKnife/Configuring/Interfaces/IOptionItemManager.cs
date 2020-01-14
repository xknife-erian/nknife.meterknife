using System.Collections.Generic;

namespace NKnife.Configuring.Interfaces
{
    public interface IOptionItemManager
    {
        IEnumerable<IOptionControl> Initialize(IEnumerable<IOptionListItem> optionItems);
    }
}
