using System.Collections.Generic;
using FluentAssertions;
using NKnife.MeterKnife.Util.Tunnel.Filters;
using Xunit;

namespace NKnife.MeterKnife.UnitTest.Common.Tunnel
{
    public class BytesProtocolFilterTest
    {
        [Fact]
        public void ContainsCommandTest1()
        {
            var src = new List<byte[]>();
            src.Add(new byte[] { 0x01, 0x02 });
            src.Add(new byte[] { 0x02, 0x02 });
            src.Add(new byte[] { 0x03, 0x02 });
            src.Add(new byte[] { 0x04, 0x02 });
            src.Add(new byte[] { 0x05, 0x02 });
            var sub = new byte[] { 0x05, 0x02 };
            var stake = new Stake();
            stake.C(src, sub).Should().BeTrue();
        }

        [Fact]
        public void ContainsCommandTest2()
        {
            var src = new List<byte[]>();
            src.Add(new byte[] { 0x01, 0x02 });
            src.Add(new byte[] { 0x02, 0x02, 0xFF, 0x11 });
            src.Add(new byte[] { 0x03, 0x02 });
            var sub = new byte[] { 0x02, 0x02 };
            var stake = new Stake();
            stake.C(src, sub).Should().BeFalse();
        }

        [Fact]
        public void ContainsCommandTest3()
        {
            var src = new List<byte[]>();
            src.Add(new byte[] { 0x01, 0x02 });
            src.Add(new byte[] { 0x02, 0x02 });
            src.Add(new byte[] { 0x03, 0x02 });
            var sub = new byte[] { 0x02, 0x02 };
            var stake = new Stake();
            stake.C(src, sub).Should().BeTrue();
        }

        [Fact]
        public void ContainsCommandTest4()
        {
            var src = new List<byte[]>();
            src.Add(new byte[] { 0x01, 0x02 });
            src.Add(new byte[] { 0xFF, 0x02, 0x03 });
            src.Add(new byte[] { 0x03, 0x02 });
            var sub = new byte[] { 0x02, 0x03 };
            var stake = new Stake();
            stake.C(src, sub).Should().BeFalse();
        }

        [Fact]
        public void ContainsCommandTest5()
        {
            var sub = new byte[] { 0x02, 0x03 };
            var stake = new Stake();
            stake.C(null, sub).Should().BeFalse();
        }

        [Fact]
        public void ContainsCommandTest6()
        {
            var src = new List<byte[]>();
            src.Add(new byte[] { 0x01, 0x02 });
            src.Add(new byte[] { 0xFF, 0x02, 0x03 });
            src.Add(new byte[] { 0x03, 0x02 });
            var stake = new Stake();
            stake.C(src, null).Should().BeFalse();
        }

        [Fact]
        public void ContainsCommandTest7()
        {
            var src = new List<byte[]>();
            src.Add(new byte[] { 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02 });
            src.Add(new byte[] { 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02 });
            src.Add(new byte[] { 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02 });
            var sub = new byte[] { 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02 };
            var stake = new Stake();
            stake.C(src, sub).Should().BeTrue();
        }

        [Fact]
        public void ContainsCommandTest8()
        {
            var src = new List<byte[]>();
            src.Add(new byte[] { 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x00 });
            src.Add(new byte[] { 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02 });
            src.Add(new byte[] { 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x00 });
            var sub = new byte[] { 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02 };
            var stake = new Stake();
            stake.C(src, sub).Should().BeTrue();
        }

        [Fact]
        public void ContainsCommandTest9()
        {
            var src = new List<byte[]>();
            src.Add(new byte[] { 0xFF, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02 });
            src.Add(new byte[] { 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02 });
            src.Add(new byte[] { 0xFF, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02 });
            var sub = new byte[] { 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02 };
            var stake = new Stake();
            stake.C(src, sub).Should().BeTrue();
        }

        [Fact]
        public void ContainsCommandTest10()
        {
            var src = new List<byte[]>();
            src.Add(new byte[] { 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02 });
            src.Add(new byte[] { 0xFF, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02 });
            src.Add(new byte[] { 0xFF, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02 });
            var sub = new byte[] { 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02 };
            var stake = new Stake();
            stake.C(src, sub).Should().BeTrue();
        }

        [Fact]
        public void ContainsCommandTest11()
        {
            var src = new List<byte[]>();
            src.Add(new byte[] { 0xFF, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02 });
            src.Add(new byte[] { 0xFF, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02 });
            src.Add(new byte[] { 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02 });
            var sub = new byte[] { 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02 };
            var stake = new Stake();
            stake.C(src, sub).Should().BeTrue();
        }

        [Fact]
        public void ContainsCommandTest12()
        {
            var src = new List<byte[]>();
            src.Add(new byte[] { 0xFF });
            src.Add(new byte[] { 0xFF });
            src.Add(new byte[] { 0x00 });
            var sub = new byte[] { 0x00 };
            var stake = new Stake();
            stake.C(src, sub).Should().BeTrue();
        }

        public class Stake : BytesProtocolFilter
        {
            public bool C(List<byte[]> list, byte[] command)
            {
                return this.ContainsCommand(list, command);
            }
        }
    }
}
