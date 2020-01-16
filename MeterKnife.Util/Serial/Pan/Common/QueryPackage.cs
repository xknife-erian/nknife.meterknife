namespace MeterKnife.Util.Serial.Pan.Common
{
    /// <summary>轮询数据包
    /// </summary>
    public class QueryPackage : PackageBase
    {
        public QueryPackage(ushort port, byte[] dataToSend, SendInterval sendInterval)
            : base(port, dataToSend, sendInterval)
        {
        }
    }
}