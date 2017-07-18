using System;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using LiteDB;
using MeterKnife.Datas.Dpi;
using MeterKnife.Models;
using MeterKnife.Models.Exhibits;
using NKnife.Channels.Channels.Base;
using NKnife.Channels.Interfaces;
using NKnife.Channels.Interfaces.Channels;
using NKnife.DataLite;
using NKnife.Utility;

namespace MeterKnife.Datas
{
    public class Program
    {
        private static readonly UtilityRandom _random = new UtilityRandom();

        public static void Main(string[] args)
        {
            //新建采集数据列表数据库
            ExhibitListRepository elr = new ExhibitListRepository();

            //模拟
            var exhibitId = Guid.NewGuid().ToString("N").ToUpper();
            var exhibit = new DemoExhibit(exhibitId);

            var er = new ExhibitRepository<double>();

            Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} === 生成数据...");
            var answers = GetAnswer(exhibit, 5 * 1000);
            Console.WriteLine();
            Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} === 20路温度数据开始");
            int i = 0;
            foreach (var answer in answers)
            {
                var exh = new ExhibitData<double> {Values = new double[2]};
                exh.Values[0] = double.Parse(answer.Data.ToString(Encoding.ASCII));
                exh.Values[1] = double.Parse(answer.Data.ToString(Encoding.ASCII));
                exh.Time = DateTime.Now;
                er.Save(exh);
                if (i % 100 == 0)
                    Console.Write('.');
                i++;
            }
            Console.WriteLine();
            Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} === 20路温度数据完成");

            var pageable = new Pageable<ExhibitData<double>>(1, 5, null, data => data.Id > 50 && data.Id < 70);
            var list = er.FindMulti(pageable);

            foreach (var exh in list.Content)
            {
                Console.WriteLine(exh);
            }
            Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} === 查询20条温度分页数据完成");

            Console.ReadKey();
        }

        private static DemoAnswer[] GetAnswer(IExhibit exhibit, int count)
        {
            var channel = new DemoChannel();
            var device = new Device("Huaxin", "MultiTemperature", "HXM");

            var answers = new DemoAnswer[count];
            for (int i = 0; i < count; i++)
            {
                var v = Encoding.ASCII.GetBytes($"9.{_random.Next(99977777, 99999999)}");
                var answer = new DemoAnswer(channel, device, exhibit, v);
                answers[i] = answer;
            }
            return answers;
        }

        class DemoAnswer : AnswerBase<byte[]>
        {
            public DemoAnswer(IChannel<byte[]> channel, IDevice device, IExhibit exhibit, byte[] data)
                : base(channel, device, exhibit, data)
            {
            }
        }

        class DemoChannel : ChannelBase<byte[]>
        {
            #region Overrides of ChannelBase<byte[]>

            /// <summary>
            ///     打开采集通道
            /// </summary>
            /// <returns></returns>
            public override bool Open()
            {
                return true;
            }

            /// <summary>
            ///     关闭采集通道
            /// </summary>
            /// <returns></returns>
            public override bool Close()
            {
                return false;
            }

            /// <summary>
            ///     更新即将发送的数据
            /// </summary>
            /// <param name="questionGroup">即将发送的数据</param>
            public override void UpdateQuestionGroup(IQuestionGroup<byte[]> questionGroup)
            {
            }

            /// <summary>
            ///     发送数据并同步等待数据返回
            /// </summary>
            /// <param name="sendAction">当发送完成时</param>
            /// <param name="receivedFunc">当采集到数据(返回的数据)的处理方法,当返回true时，表示接收数据是完整的，返回flase时，表示接收数据不完整，还需要继续接收</param>
            public override void SendReceiving(Action<IQuestion<byte[]>> sendAction, Func<IAnswer<byte[]>, bool> receivedFunc)
            {
            }

            /// <summary>
            ///     自动发送数据
            /// </summary>
            public override void AutoSend(Action<IQuestion<byte[]>> sendAction)
            {
            }

            /// <summary>
            ///     当自动发送模式时，中断正在不断进行的自动模式
            /// </summary>
            public override void Break()
            {
            }

            #endregion
        }

        class DemoExhibit : NKnife.Electronics.Resistance, IExhibit
        {
            public DemoExhibit(string id)
            {
                Id = id;
                Detail = Guid.NewGuid().ToString();
                CreatedTime = DateTime.Now;
            }

            #region Implementation of IExhibit

            /// <summary>
            /// 观察点的ID
            /// </summary>
            public string Id { get; set; }

            /// <summary>
            /// 关于本观察点的描述
            /// </summary>
            public string Detail { get; set; }

            /// <summary>
            /// 创建本观察点对象的时间(非物理的制造时间，一般来讲描述的是采集数据的开始时间)
            /// </summary>
            public DateTime CreatedTime { get; set; }

            #endregion
        }
    }
}