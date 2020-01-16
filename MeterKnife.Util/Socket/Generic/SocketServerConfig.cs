using MeterKnife.Util.Socket.Interfaces;

namespace MeterKnife.Util.Socket.Generic
{
    public class SocketServerConfig : SocketConfig, ISocketServerConfig
    {
        public SocketServerConfig()
        {
            _Map.Add("MaxSessionTimeout", 60); //默认session经过60秒无任何动作则server主动断开该连接，如果该值设为0则server不会主动清除
        }

        public int MaxSessionTimeout
        {
            get => int.Parse(_Map["MaxSessionTimeout"].ToString());
            set => _Map["MaxSessionTimeout"] = value;
        }
    }
}