using MeterKnife.Util.Exceptions;

namespace MeterKnife.Util.Electronics.Exceptions
{
    public class ResistanceCollectionException : NKnifeException
    {
        protected ResistanceCollectionException(string message) :
            base(message)
        {
        }

        public static ResistanceCollectionException ForNull()
        {
            return new ResistanceCollectionException("电阻集合为空.");
        }

    }
}
