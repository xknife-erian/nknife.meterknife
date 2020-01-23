using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CliFx.Attributes;
using CliFx.Services;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.CLI.Commands
{
    [Command("d", Description = "模拟数据存储")]
    public class DataCommand : BaseCommand
    {
        private readonly IPerformStorageLogic _logic;

        public DataCommand(IPerformStorageLogic logic)
        {
            _logic = logic;
        }

        #region Overrides of BaseCommand

        /// <summary>
        /// Executes command using specified implementation of <see cref="T:CliFx.Services.IConsole" />.
        /// This method is called when the command is invoked by a user through command line interface.
        /// </summary>
        public override async Task ExecuteAsync(IConsole console)
        {
            var dut = new DUT();
            var data = new MetricalData();
            await _logic.ProcessAsync(dut, data);
        }

        #endregion
    }
}
