using MeterKnife.Util.Serial.Common;

namespace MeterKnife.Util.Serial.Interfaces
{
    /// <summary>串口操作类接口
    /// </summary>
    public interface ISerialPortWrapper
    {
        /// <summary>串口是否打开标记
        /// </summary>
        bool IsOpen { get; set; }

        /// <summary>初始化操作器通讯串口
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        bool Initialize(string portName, SerialConfig config);

        /// <summary> 关闭串口
        /// </summary>
        /// <returns></returns>
        bool Close();

        /// <summary>设置串口读取超时
        /// </summary>
        /// <param name="timeout"></param>
        void SetTimeOut(int timeout);

        /// <summary>发送数据即刻读取回复数据
        /// </summary>
        /// <param name="cmd">待发送的数据</param>
        /// <param name="recv">回复的数据</param>
        /// <returns>回复的数据的长度</returns>
        int SendReceived(byte[] cmd, out byte[] recv);
    }
}