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
                var app = mock.Create<AppMock>();

                var slot = app.Slot;
                slot.AttachToDataBus(app.DataBus);

                StatementQueue prepare = BuildPrepareStatementLeader();
                StatementQueue sustainable = BuildSustainableStatementLeader();
                StatementQueue maintain = BuildMaintainStatementLeader();
                slot.Setup(prepare, sustainable, maintain);
                slot.StartAsync();
            }
        }

        private StatementQueue BuildMaintainStatementLeader()
        {
            throw new NotImplementedException();
        }

        private StatementQueue BuildSustainableStatementLeader()
        {
            throw new NotImplementedException();
        }

        private StatementQueue BuildPrepareStatementLeader()
        {
            var queue = new StatementQueue();

            var leader = new Statement();
            leader.StatementKind = StatementKind.Setup;
            leader.CommandMode = CommandMode.Hex;
            leader.Body = "hello volcano.";
            leader.Delay = 300;
            leader.Timeout = 200;

            var second = new Statement();
            second.StatementKind = StatementKind.Ask;
            second.CommandMode = CommandMode.Hex;
            second.Body = "hello volcano.";
            second.Delay = 1000;
            second.Timeout = 500;

            var third = new Statement();
            third.StatementKind = StatementKind.Ask;
            third.CommandMode = CommandMode.Hex;
            third.Body = "hello volcano.";
            third.Delay = 1000;
            third.Timeout = 500;

            var fourth = new Statement();
            fourth.StatementKind = StatementKind.Ask;
            fourth.CommandMode = CommandMode.Hex;
            fourth.Body = "hello volcano.";
            fourth.Delay = 1000;
            fourth.Timeout = 500;
            fourth.NeedToLoop = true;

            var fifty = new Statement();
            fifty.StatementKind = StatementKind.Ask;
            fifty.CommandMode = CommandMode.Hex;
            fifty.Body = "hello volcano.";
            fifty.Delay = 1000;
            fifty.Timeout = 500;
            fifty.NeedToLoop = true;
            fifty.LoopCount = 30;

            queue.AddRange(new[] {leader, second, third, fourth, fifty});

            return queue;
        }
    }
}
