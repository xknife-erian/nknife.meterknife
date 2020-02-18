using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NKnife.MeterKnife.ViewModels.Plots;
using OxyPlot.Axes;
using Xunit;

namespace NKnife.MeterKnife.UnitTest.ViewModels.Plots
{
    public class DUTLinearPlotTest
    {
        [Fact]
        public void UpdateRangeTest1()
        {
            var axis = new LinearAxis();
            var value = 999999;
            DUTLinearPlot.UpdateRange(value, axis,5, true);
            axis.Maximum.Should().BeGreaterThan(value);
            axis.Minimum.Should().BeLessThan(value);
        }

        [Fact]
        public void GetPrecisionTest()
        {
            double value = 0;
            var p = DUTLinearPlot.GetPrecision(value);
            p.Should().Be(0);
            
            value = 0;
            p = DUTLinearPlot.GetPrecision(value);
            p.Should().Be(0);
            value = 1;
            p = DUTLinearPlot.GetPrecision(value);
            p.Should().Be(0);
            value = 0.1;
            p = DUTLinearPlot.GetPrecision(value);
            p.Should().Be(1);
            value = 0.12;
            p = DUTLinearPlot.GetPrecision(value);
            p.Should().Be(2);
            value = 0.123;
            p = DUTLinearPlot.GetPrecision(value);
            p.Should().Be(3);
            value = 0.1234;
            p = DUTLinearPlot.GetPrecision(value);
            p.Should().Be(4);
            value = 0.12345;
            p = DUTLinearPlot.GetPrecision(value);
            p.Should().Be(5);
            value = 0.123456;
            p = DUTLinearPlot.GetPrecision(value);
            p.Should().Be(6);
            value = 0.1234567;
            p = DUTLinearPlot.GetPrecision(value);
            p.Should().Be(7);
            value = 0.12345678;
            p = DUTLinearPlot.GetPrecision(value);
            p.Should().Be(8);
            value = 0.123456789;
            p = DUTLinearPlot.GetPrecision(value);
            p.Should().Be(9);
            value = 0.1234567899;
            p = DUTLinearPlot.GetPrecision(value);
            p.Should().Be(10);
            value = 0.12345678901;
            p = DUTLinearPlot.GetPrecision(value);
            p.Should().Be(11);
        }

    }
}
