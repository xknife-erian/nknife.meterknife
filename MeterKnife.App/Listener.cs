using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using NKnife.ControlKnife;

namespace MeterKnife.App
{
    public class Listener
    {
        private const string MAIN_SERVICE = "Pansoft.CQMS.Queue.Environments.AppDomain";
        private readonly TcpListener _TcpListener;
        private bool _IsListen = true;

        public Listener()
        {
            var ipEndPoint = new IPEndPoint(IPAddress.Any, 12340);
            _TcpListener = new TcpListener(ipEndPoint);
        }

        public void Initialize(Action<string> action = null)
        {
            _TcpListener.Start(8);
            while (_IsListen)
            {
                if (_TcpListener.Pending()) //判断是否有挂起的请求
                {
                    TcpClient tcpClient = null; //绑定在客户端上
                    try
                    {
                        tcpClient = _TcpListener.AcceptTcpClient(); //绑定在客户端上
                        var stream = tcpClient.GetStream();
                        string cmd = string.Empty;
                        if (stream.CanRead)
                        {
                            var bytes = new byte[512];
                            int i = stream.Read(bytes, 0, bytes.Length);
                            cmd = Encoding.Default.GetString(bytes, 0, i).Trim();
                        }
                        tcpClient.Close();
                        if (!string.IsNullOrWhiteSpace(cmd))
                        {
                            Console.WriteLine("主域监听到命令:" + cmd);
                            if (action == null)
                                ProcessCommand(cmd);
                            else//该情况出现是当单元测试时
                                action.Invoke(cmd);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    finally
                    {
                        tcpClient?.Close();
                    }
                }
                Thread.Sleep(100);
            }
        }

        public void StopListen()
        {
            _IsListen = false;
        }

        private void ProcessCommand(string command)
        {
            string[] c = command.Split('@');
            switch (c[0])
            {
                case MAIN_SERVICE:
                    CallMainService(c);
                    break;
            }
        }

        private void CallMainService(string[] commands)
        {
            switch (commands[1].ToLower())
            {
                case "exit_application": //关闭程序
                {
                    _IsListen = false;
                    _TcpListener.Stop();
                    Program.MainService.UnLoad();
                    Program.AutoResetEvent.Set();
                    Environment.Exit(0);
                    break;
                }
                case "restart_appdomain": //重启程序
                {
                    Program.MainService.UnLoad();
                    Thread.Sleep(500);
                    Program.MainService.Load(null);
                    break;
                }
                case "splash_message": //程序启动时的动作或信息
                {
                    SplashShow(commands[2]);
                    break;
                }
                case "app_loaded"://描述应用加载完成的协议
                    {
                    Splasher.Close();
                    break;
                }
            }
        }

        private void SplashShow(string msg)
        {
            Splasher.Status = msg;
        }
    }
}