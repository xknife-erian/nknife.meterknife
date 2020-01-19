using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using CliFx.Services;

namespace NKnife.MeterKnife.CLI.Commands
{
    [Command("cc", Description = "连接Care，进行配置读取与写入。")]
    public class CareConfigCliCommand : BaseCommand
    {
        public override async Task ExecuteAsync(IConsole console)
        {
            throw new NotImplementedException();
        }
    }
}
