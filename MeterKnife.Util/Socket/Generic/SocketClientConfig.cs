using NKnife.Socket.Interfaces;

namespace NKnife.Socket.Generic
{
    public class SocketClientConfig : SocketConfig, ISocketClientConfig
    {
        public SocketClientConfig()
        {
            _Map.Add("ReconnectInterval", 1000 * 6); //默认自动重连间隔6秒
        }

        public int ReconnectInterval
        {
            get { return int.Parse(_Map["ReconnectInterval"].ToString()); }
            set
            {
                _Map["ReconnectInterval"] = value;
            }
        }
    }
}
