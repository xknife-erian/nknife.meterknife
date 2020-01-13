using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Extras.Moq;
using Xunit;

namespace NKnife.MeterKnife.Tests.Infrastructure
{
    public class StatementTest
    {
        
        [Fact]
        public void Test1()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var app = mock.Create<App>();

                var slot = app.Slot;
                slot.AttachToDataBus(app.DataBus);

                IStatement prepare = BuildPrepareStatementLeader();
                IStatement sustainable = new Statement();
                IStatement maintain = new Statement();
                slot.Setup(prepare, sustainable, maintain);
                slot.StartAsync();
            }
        }

        private IStatement BuildPrepareStatementLeader()
        {
            var leader = new Statement();
            leader.StatementKind = StatementKind.Setup;
            leader.CommandMode = CommandMode.Hex;
            leader.HexBody = new byte[] {0x01, 0x02, 0xAA, 0x55};
            leader.Delay = 300;
            leader.Timeout = 200;

            var second = new Statement();
            second.StatementKind = StatementKind.Ask;
            second.CommandMode = CommandMode.Hex;
            second.HexBody = new byte[] {0x01, 0x02, 0xAA, 0x55};
            second.Delay = 1000;
            second.Timeout = 500;
        
            var third = new Statement();
            third.StatementKind = StatementKind.Ask;
            third.CommandMode = CommandMode.Hex;
            third.HexBody = new byte[] {0x01, 0x02, 0xAA, 0x55};
            third.Delay = 1000;
            third.Timeout = 500;

            var fourth = new Statement();
            fourth.StatementKind = StatementKind.Ask;
            fourth.CommandMode = CommandMode.Hex;
            fourth.HexBody = new byte[] {0x01, 0x02, 0xAA, 0x55};
            fourth.Delay = 1000;
            fourth.Timeout = 500;
            fourth.NeedToLoop = true;

            var fifty = new Statement();
            fifty.StatementKind = StatementKind.Ask;
            fifty.CommandMode = CommandMode.Hex;
            fifty.HexBody = new byte[] {0x01, 0x02, 0xAA, 0x55};
            fifty.Delay = 1000;
            fifty.Timeout = 500;
            fifty.NeedToLoop = true;
            fifty.LoopCount = 30;

            leader.Add(second);
            second.Add(third);
            third.Add(fourth);
            fourth.Add(fifty);

            return leader;
        }
    }
}
