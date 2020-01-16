namespace NKnife.Socket.Interfaces
{
    public interface ISocketClientConfig : ISocketConfig
    {
        /// <summary>
        /// �����ļ��ʱ��
        /// </summary>
        int ReconnectInterval { get; set; }
    }
}