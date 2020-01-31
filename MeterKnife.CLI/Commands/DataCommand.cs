using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.CLI.Commands
{
    [Command("d", Description = "模拟数据存储")]
    public class DataCommand : BaseCommand
    {
        private readonly IEngineeringLogic _engineeringLogic;
        private readonly IPerformStorageLogic _performStorageLogic;

        public DataCommand(IEngineeringLogic engineeringLogic, IPerformStorageLogic performStorageLogic)
        {
            _engineeringLogic = engineeringLogic;
            _performStorageLogic = performStorageLogic;
        }

        #region Overrides of BaseCommand

        /// <summary>
        /// Executes command using specified implementation of <see cref="T:CliFx.Services.IConsole" />.
        /// This method is called when the command is invoked by a user through command line interface.
        /// </summary>
        public override async ValueTask ExecuteAsync(IConsole console)
        {
            var engineering = new Engineering();
            var dut = new DUT();
            var data = new MetricalData();
            await _engineeringLogic.CreateEngineering(engineering);
            await _performStorageLogic.ProcessAsync((engineering, dut), data);
        }

        #endregion
    }
}
