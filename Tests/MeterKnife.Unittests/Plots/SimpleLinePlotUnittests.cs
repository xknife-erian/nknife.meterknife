using FluentAssertions;
using MeterKnife.Plots;
using NKnife.Base;
using NUnit.Framework;

namespace MeterKnife.Unittests.Plots
{
    [TestFixture]
    public class SimpleLinePlotUnittests
    {
        [OneTimeTearDown]
        public void CleanUp()
        {
        }

        [OneTimeSetUp]
        public void Setup()
        {
        }

        public class SimpleLinePlotShip : SimpleLinePlot
        {
            public SimpleLinePlotShip(string title)
                : base(title)
            {
            }

            public static Pair<float, float> UpdateRangeMethod(float value, ref bool isFirst, ref float max, ref float min)
            {
                return UpdateRange(value, ref isFirst, ref max, ref min);
            }
        }

        [Test]
        public void UpdateRangeTest1()
        {
            var isFirst = true;
            float max = 0, min = 0;
            var pair = SimpleLinePlotShip.UpdateRangeMethod(1, ref isFirst, ref max, ref min);
            isFirst.Should().Be(false);
            max.Should().Be(1.1F);
            min.Should().Be(0.9F);
            pair.Should().Be(Pair<float, float>.Build(0.9F, 1.1F));
        }
    }
}