using Autofac.Extras.Moq;
using FluentAssertions;
using Xunit;

namespace NKnife.MeterKnife.Tests.Infrastructure
{
    /// <summary>
    ///     基础架构测试。本测试主要为了描述本平台的核心启动与调用情景。
    /// </summary>
    public class BaseTest
    {
        [Fact]
        public void Test1()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var app = mock.Create<App>();

                StatementQueue prepare = new StatementQueue();
                StatementQueue sustainable = new StatementQueue();
                StatementQueue maintain = new StatementQueue();

                app.Slot.AttachToDataBus(app.DataBus);
                app.Slot.Setup(prepare, sustainable, maintain);
                app.Slot.WorkNodeCompleted += (s, e) =>
                {
                    var data = app.DataBus.GetUutData(e.UutId);
                    data.Should().NotBeNullOrEmpty();
                };
                app.Slot.StartAsync();
                app.Slot.PauseAsync();
                app.Slot.StopAsync();
                Assert.NotNull(app.Slot);
            }
        }

    }
}