﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.Util;

namespace NKnife.MeterKnife.CLI.Commands
{
    [Command("d", Description = "模拟数据存储")]
    public class DataCommand : BaseCommand
    {
        private readonly IProjectLogic _projectLogic;
        private readonly IMeasuringLogic _measuringLogic;

        public DataCommand(IProjectLogic projectLogic, IMeasuringLogic measuringLogic)
        {
            _projectLogic = projectLogic;
            _measuringLogic = measuringLogic;
        }

        #region Overrides of BaseCommand

        /// <summary>
        /// Executes command using specified implementation of <see cref="T:CliFx.Services.IConsole" />.
        /// This method is called when the command is invoked by a user through command line interface.
        /// </summary>
        public override async ValueTask ExecuteAsync(IConsole console)
        {
            var dut1 = new DUT {Name = "100K电阻"};
            var dut2 = new DUT {Name = "10K电阻"};
            var dut3 = new DUT {Name = "1K电阻"};
            var engineering = new Project();
            var pool = new ScpiCommandPool();
            pool.AddRange(new[]
            {
                new ScpiCommand {DUT = dut1},
                new ScpiCommand {DUT = dut2},
                new ScpiCommand {DUT = dut3}
            });
            engineering.CommandPools.Add(pool);
            //创建一个工程
            await _projectLogic.CreateProjectAsync(engineering);
            //模拟处理一些数据
            for (int i = 0; i < 3 * 10000; i++)
            {
                var data = new MeasureData();
                var a = UtilRandom.Next(10000000, 99999999);
                var b = UtilRandom.Next(10000000, 99999999);
                var c = UtilRandom.Next(10000000, 99999999);
                data.Data = double.Parse($"{a}.{b}{c}");
                await _measuringLogic.ProcessAsync((engineering, dut1), data);
                console.Output.Write(".");
                data.Data = double.Parse($"{b}.{c}{a}");
                await _measuringLogic.ProcessAsync((engineering, dut2), data);
                console.Output.Write(".");
                data.Data = double.Parse($"{c}.{a}{b}");
                await _measuringLogic.ProcessAsync((engineering, dut3), data);
                console.Output.Write(".");
                if (i % 1000 == 0)
                    console.Output.Write($"/{i}/");
            }
        }

        #endregion
    }
}
