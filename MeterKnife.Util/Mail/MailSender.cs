using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Timers;

namespace NKnife.Mail
{
    /// <summary>
    /// 邮件发送工具，支持认证发送
    /// </summary>
    public class MailSender
    {
        #region constructor

        public MailSender(string smtp, int port, string username, string pwd)
        {
            _smtpServer = smtp;
            _serverPort = port;
            _userName = username;
            _password = pwd;
        }

        #endregion

        #region private fields

        private string _smtpServer;
        private int _serverPort;
        private string _userName;
        private string _password;
        private Timer _timer;
        private Queue<MailMessage> _msgQueue;
        private object _lockObject = new object();

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();

            int msgCount = _msgQueue.Count;
            if (msgCount > 0)
            {
                SmtpClient smtp = new SmtpClient(_smtpServer, _serverPort);
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(_userName, _password);
                while (msgCount-- > 0)
                {
                    MailMessage message = null;
                    try
                    {
                        message = _msgQueue.Dequeue();
                        if (message != null)
                        {
                            smtp.Send(message);
                        }
                    }
                    catch (Exception ex)
                    {
                        string title = null;
                        string to = null;
                        if (message != null)
                        {
                            title = message.Subject;
                            to = message.To.ToString();
                        }
                        Console.WriteLine("Mail Send Failed\r\nTitle: {0}\r\nTo: {1}\r\nException:\r\n{2}", title, to, ex);
                    }
                }
            }

            _timer.Start();
        }

        #endregion

        #region public methods

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="message">MailMessage实体</param>
        public void Send(MailMessage message)
        {
            Send(message, true);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="message">MailMessage实体</param>
        /// <param name="cached">是否缓存邮件（提高程序响应速度）</param>
        public void Send(MailMessage message, bool cached)
        {
            if (cached)
            {
                lock (_lockObject)
                {
                    if (_msgQueue == null)
                    {
                        _msgQueue = new Queue<MailMessage>();
                    }
                    if (_timer == null)
                    {
                        _timer = new Timer(1000);
                        _timer.Enabled = false;
                        _timer.AutoReset = false;
                        _timer.Elapsed += new ElapsedEventHandler(TimerOnElapsed);
                    }
                    if (!_timer.Enabled)
                    {
                        _timer.Enabled = true;
                    }
                }
                lock (_msgQueue)
                {
                    _msgQueue.Enqueue(message);
                }
            }
            else
            {
                SmtpClient smtp = new SmtpClient(_smtpServer, _serverPort);
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(_userName, _password);
                smtp.Send(message);
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="from">发送者地址</param>
        /// <param name="to">接收者地址（可填多个地址，用英文分号“;”分割）</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="messageText">邮件内容</param>
        public void Send(string from, string to, string subject, string messageText)
        {
            Send(from, to, subject, messageText, true);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="from">发送者地址</param>
        /// <param name="to">接收者地址（可填多个地址，用英文分号“;”分割）</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="messageText">邮件内容</param>
        /// <param name="cached">是否缓存邮件（提高程序响应速度）</param>
        public void Send(string from, string to, string subject, string messageText, bool cached)
        {
            MailMessage message = new MailMessage(from, to, subject, messageText);
            Send(message, cached);
        }

        #endregion

        #region public properties

        /// <summary>
        /// SMTP服务器地址
        /// </summary>
        public string SmtpServer
        {
            get { return _smtpServer; }
            //set { _smtpServer = value; }
        }

        /// <summary>
        /// 服务器侦听端口
        /// </summary>
        public int ServerPort
        {
            get { return _serverPort; }
            //set { _serverPort = value; }
        }

        /// <summary>
        /// 认证用户名
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            //set { _userName = value; }
        }

        /// <summary>
        /// 认证用户密码
        /// </summary>
        public string Password
        {
            get { return _password; }
            //set { _password = value; }
        }

        #endregion
    }
}
