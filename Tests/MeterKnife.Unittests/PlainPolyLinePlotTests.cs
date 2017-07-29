using FluentAssertions;
using MeterKnife.Utils.Plots;
using NKnife.Base;
using NUnit.Framework;

namespace MeterKnife.Unittests
{
    [TestFixture]
    public class PlainPolyLinePlotTests
    {
        [OneTimeTearDown]
        public void CleanUp()
        {
        }

        [OneTimeSetUp]
        public void Setup()
        {
        }

        public class PlainPolyLinePlotShip : PlainPolyLinePlot
        {
            public PlainPolyLinePlotShip()
                : base(new PlotTheme())
            {
            }

            public static Pair<double, double> UpdateRangeMethod(double value, ref bool isFirst, ref double max, ref double min)
            {
                return UpdateRange(value, ref isFirst, ref max, ref min);
            }
        }

        [Test]
        public void UpdateRangeTest1()
        {
            var isFirst = true;
            double max = 0, min = 0;
            var pair = PlainPolyLinePlotShip.UpdateRangeMethod(1, ref isFirst, ref max, ref min);
            isFirst.Should().Be(false);
            max.Should().Be(1.1F);
            min.Should().Be(0.9F);
            pair.Should().Be(Pair<double, double>.Build(0.9F, 1.1F));
        }
    }
}