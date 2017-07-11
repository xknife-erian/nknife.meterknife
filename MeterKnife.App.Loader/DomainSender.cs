using System;
using System.Net.Sockets;
using System.Text;

namespace MeterKnife.App
{
    internal class DomainSender
    {
        /// <summary>
        ///     退出服务的协议
        /// </summary>
        public static readonly string exit_protocol = $"{EnvironmentDynamicLoader.domain_name}@exit_application";

        /// <summary>
        ///     在欢迎图上显示信息的协议
        /// </summary>
        public static readonly string splash_message_protocol = $"{EnvironmentDynamicLoader.domain_name}@splash_message";

        /// <summary>
        ///     描述应用加载完成的协议
        /// </summary>
        public static readonly string start_form_shown_protocol = $"{EnvironmentDynamicLoader.domain_name}@app_loaded";

        private static void Send(string message)
        {
            try
            {
                var tcpClient = new TcpClient();
                tcpClient.Connect("127.0.0.1", 12340);
                var data = Encoding.Default.GetBytes(message);
                var stream = tcpClient.GetStream();
                stream.Write(data, 0, data.Length);
                tcpClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("无法连接守护线程。{0}", e.Message);
            }
        }

        public static void SendExitServiceCommand()
        {
            Send(exit_protocol);
        }

        public static void SendSplashMessage(string splashMsg)
        {
            Send($"{splash_message_protocol}@{splashMsg}");
        }

        public static void SendStartFormShown()
        {
            Send(start_form_shown_protocol);
        }
    }
}