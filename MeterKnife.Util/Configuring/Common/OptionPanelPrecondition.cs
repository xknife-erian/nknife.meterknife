using NKnife.Configuring.Interfaces;

namespace NKnife.Configuring.Common
{
    public class OptionPanelPrecondition : IOptionPanelPrecondition
    {
        #region Implementation of IOptionPanelPrecondition

        public bool Check()
        {
            return true;
        }

        #endregion
    }
}
