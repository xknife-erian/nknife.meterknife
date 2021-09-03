using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;

namespace NKnife.MeterKnife.CLI.Commands
{
    public abstract class BaseCommand : ICommand
    {
        [CommandOption("port", 'p', IsRequired = true, Description = "串口的数字编号")]
        public ushort Port { get; set; }

        public abstract ValueTask ExecuteAsync(IConsole console);
    }
}