using MeterKnife.Electronics;
using MeterKnife.Electronics.Helper;
using NUnit.Framework;

namespace MeterKnife.Unittests.Electronics
{
    [TestFixture]
    public class ResistanceValueResolveTest
    {
        public const string RESS =
            "1,2,3,10,12,15,18,20,22,24,25,27,30,33,36,39,40,43,47,50,51,56,62,68,75,82,91,100,110,120,130,140,150,160,180,200,220,240,250,270,300,330,360,390,400,430,470,500,510,560,620,680,750,806,820,900,910,1000,1100,1200,1300,1400,1500,1600,1800,2000,2200,2400,2700,3000,3300,3600,3900,4000,4300,4700,5000,5100,5600,6200,6800,7500,10000,11000,12000,13000,14000,15000,16000,18000,20000,22000,24000,25000,27000,30000,33000,36000,39000,40000,43000,47000,50000,51000,56000,62000,68000,75000,80600,82000,90000,91000,100000,110000,120000,130000,140000,150000,160000,180000,200000,220000,226000,240000,270000,300000,330000,360000,390000,400000,430000,470000,499000,500000,510000,560000,620000,680000,750000,820000,900000,909000,910000,1000000,1500000,2000000,5000000,10000000";

        private readonly long[] _Ress;

        public ResistanceValueResolveTest()
        {
            var s = RESS.Split(',');
            _Ress = new long[s.Length];
            for (int i = 0; i < s.Length; i++)
                _Ress[i] = long.Parse(s[i]);
        }

        /// <summary>基础测试
        /// </summary>
        [Test]
        public void TryGetResolveResultTestMethod1()
        {
            var solve = new ResistanceValueResolve();
            solve.Existing = new Resistances();
            solve.Existing.Add(new Resistance() { Value = 100000 });
            solve.Existing.Add(new Resistance() { Value = 10000 });
            solve.Existing.Add(new Resistance() { Value = 1000 });
            solve.Existing.Add(new Resistance() { Value = 100 });
            solve.Existing.Add(new Resistance() { Value = 10 });
            solve.Existing.Add(new Resistance() { Value = 1 });

            Resistances[] results;
            solve.TryGetResolveResult(out results, ResistanceValueResolveModel.SeriesBigValuePriority, 330);

            Assert.AreEqual(1, results.Length);

            var result = results[0];

            Assert.AreEqual(100, result[0].Value);
            Assert.AreEqual(100, result[1].Value);
            Assert.AreEqual(100, result[2].Value);
            Assert.AreEqual(10, result[3].Value);
            Assert.AreEqual(10, result[4].Value);
            Assert.AreEqual(10, result[5].Value);
        }

        /// <summary>复杂值测试(无余数)
        /// </summary>
        [Test]
        public void TryGetResolveResultTestMethod2()
        {
            var solve = new ResistanceValueResolve();
            solve.Existing = new Resistances();
            solve.Existing.Add(new Resistance() {Value = 1000000});
            solve.Existing.Add(new Resistance() {Value = 100000});
            solve.Existing.Add(new Resistance() {Value = 10000});
            solve.Existing.Add(new Resistance() {Value = 1000});
            solve.Existing.Add(new Resistance() {Value = 100});
            solve.Existing.Add(new Resistance() {Value = 10});
            solve.Existing.Add(new Resistance() {Value = 1});

            Resistances[] results;
            solve.TryGetResolveResult(out results, ResistanceValueResolveModel.SeriesBigValuePriority, 1234567);

            Assert.AreEqual(1, results.Length);

            var result = results[0];

            Assert.AreEqual(28, result.Count);
            Assert.AreEqual(1000000, result[0].Value);//1
            Assert.AreEqual(100000, result[1].Value);//2
            Assert.AreEqual(100000, result[2].Value);
            Assert.AreEqual(10000, result[3].Value);//3
            Assert.AreEqual(10000, result[5].Value);
            Assert.AreEqual(1000, result[6].Value);//4
            Assert.AreEqual(1000, result[9].Value);
            Assert.AreEqual(100, result[10].Value);//5
            Assert.AreEqual(100, result[14].Value);
            Assert.AreEqual(10, result[15].Value);//6
            Assert.AreEqual(10, result[20].Value);
            Assert.AreEqual(1, result[21].Value);//7
            Assert.AreEqual(1, result[27].Value);
        }

        /// <summary>复杂值测试(有余数)
        /// </summary>
        [Test]
        public void TryGetResolveResultTestMethod3()
        {
            var solve = new ResistanceValueResolve();
            solve.Existing = new Resistances();
            solve.Existing.Add(new Resistance() { Value = 1000000 });
            solve.Existing.Add(new Resistance() { Value = 100000 });
            solve.Existing.Add(new Resistance() { Value = 10000 });
            solve.Existing.Add(new Resistance() { Value = 1000 });
            solve.Existing.Add(new Resistance() { Value = 100 });
            solve.Existing.Add(new Resistance() { Value = 10 });

            Resistances[] results;
            var reminder = solve.TryGetResolveResult(out results, ResistanceValueResolveModel.SeriesBigValuePriority, 1234567);

            Assert.AreEqual(1, results.Length);

            var result = results[0];

            Assert.AreEqual(21, result.Count);
            Assert.AreEqual(1000000, result[0].Value);//1
            Assert.AreEqual(100000, result[1].Value);//2
            Assert.AreEqual(100000, result[2].Value);
            Assert.AreEqual(10000, result[3].Value);//3
            Assert.AreEqual(10000, result[5].Value);
            Assert.AreEqual(1000, result[6].Value);//4
            Assert.AreEqual(1000, result[9].Value);
            Assert.AreEqual(100, result[10].Value);//5
            Assert.AreEqual(100, result[14].Value);
            Assert.AreEqual(10, result[15].Value);//6
            Assert.AreEqual(10, result[20].Value);

            Assert.AreEqual(7, reminder);
        }

        /// <summary>复杂值测试(有余数)
        /// </summary>
        [Test]
        public void TryGetResolveResultTestMethod4()
        {
            var solve = new ResistanceValueResolve();
            solve.Existing = new Resistances();
            foreach (var resValue in _Ress)
            {
                solve.Existing.Add(new Resistance() {Value = resValue});
            }

            Resistances[] results;
            const ResistanceValueResolveModel model = ResistanceValueResolveModel.SeriesBigValuePriority;
            solve.TryGetResolveResult(out results, model, 1012341234);
            Assert.IsTrue(results[0].Count > 0);
        }

        /// <summary>无法分解
        /// </summary>
        [Test]
        public void TryGetResolveResultTestMethod5()
        {
            var solve = new ResistanceValueResolve();
            solve.Existing = new Resistances();
            solve.Existing.Add(new Resistance() { Value = 1000000 });
            solve.Existing.Add(new Resistance() { Value = 100000 });

            Resistances[] results;
            var actual = solve.TryGetResolveResult(out results, ResistanceValueResolveModel.SeriesBigValuePriority, 200);
            Assert.AreEqual(1, results.Length);

            var result = results[0];
            Assert.AreEqual(0, result.Count);
            Assert.AreEqual(200, actual);
        }

        /// <summary>无法分解
        /// </summary>
        [Test]
        public void TryGetResolveResultTestMethod6()
        {
            var solve = new ResistanceValueResolve();
            solve.Existing = new Resistances();
            solve.Existing.Add(new Resistance() { Value = 1000000 });
            solve.Existing.Add(new Resistance() { Value = 100000 });
            solve.Existing.Add(new Resistance() { Value = 10000 });
            solve.Existing.Add(new Resistance() { Value = 1000 });
            solve.Existing.Add(new Resistance() { Value = 100 });
            solve.Existing.Add(new Resistance() { Value = 10 });
            solve.Existing.Add(new Resistance() { Value = 1 });

            Resistances[] results;
            var model = ResistanceValueResolveModel.SeriesSmallValuePriority;
            var actual = solve.TryGetResolveResult(out results, model, 12345);
            Assert.AreEqual(1, results.Length);
        }

    }
}
