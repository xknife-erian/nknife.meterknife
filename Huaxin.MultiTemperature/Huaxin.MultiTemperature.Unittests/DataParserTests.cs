using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Huaxin.MultiTemperature.Common;
using NKnife.Collections;

namespace Huaxin.MultiTemperature.Tests
{
    [TestClass()]
    public class DataParserTests
    {
        [TestMethod()]
        public void DataArrivedTest1()
        {
            SyncQueue<byte> queue = new SyncQueue<byte>();
            var dataParser = new DataParser();
            dataParser.DataArrivedThreadStarted = true;
            dataParser.IsDebug = true;

            var bs = new byte[]
            {
                0x68, 0x78, 0x79, 0x66,
                /*地址*/0x01,
                /*长度*/0x08,
                /*方法*/0x01,
                /*通道*/0x08,
                /*数据*/0xAA, 0xBB, 0x56, 0x78,
                /*CRC*/0xCC, 0xDD
            };
            foreach (var b in bs)
                queue.Enqueue(b);
            dataParser.Parser(queue);
            var ac = new byte[8 - 2]; //去掉CRC
            Buffer.BlockCopy(bs, 6, ac, 0, ac.Length);
            CollectionAssert.AreEqual(ac, dataParser.Bag.Content);
            Assert.AreEqual(0, queue.Count);
        }
    }
}