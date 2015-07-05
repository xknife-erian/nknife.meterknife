using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using Common.Logging;
using MeterKnife.Common.Algorithms;
using MeterKnife.Common.Algorithms.Difference;
using MeterKnife.Common.EventParameters;
using MeterKnife.Common.Interfaces;
using NKnife.IoC;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace MeterKnife.Common.DataModels
{
    public class FiguredData : ICollectSource
    {
        private static readonly ILog _logger = LogManager.GetLogger<FiguredData>();

        #region 分析内容

        [Category("数据分析"), DisplayName("最大值")]
        public Max Max { get; private set; }

        [Category("数据分析"),DisplayName("最小值")]
        public Min Min { get; private set; }

        [Category("数据分析"),DisplayName("均方根")]
        public RootMeanSquare RootMeanSquare { get; private set; }

        [Category("数据分析"), DisplayName("算术平均")]
        public ArithmeticMean ArithmeticMean { get; private set; }

        [Category("温度"), DisplayName("最大值")]
        public TemperatureMax TemperatureMax { get; private set; }

        [Category("温度"), DisplayName("最小值")]
        public TemperatureMin TemperatureMin { get; private set; }

        [Category("温度"), DisplayName("均方根")]
        public TemperatureRootMeanSquare TemperatureRootMeanSquare { get; private set; }

        [Category("温度"), DisplayName("算术平均")]
        public TemperatureArithmeticMean TemperatureArithmeticMean { get; private set; }

        [Category("偏差"), DisplayName("标准差")]
        public StandardDeviation StandardDeviation { get; private set; }

        [Category("数据分析"), DisplayName("峰峰值")]
        public string Ppvalue { get; private set; }

        [Category("数据分析"), DisplayName("总采样数")]
        public uint Count
        {
            get { return (uint)_DataSet.Tables[1].Rows.Count; }
        }

        #endregion

        private double _CurrentTemperature;
        protected DataSet _DataSet = new DataSet();

        public FiguredData()
        {
            Max = new Max();
            Min = new Min();
            ArithmeticMean = new ArithmeticMean();
            RootMeanSquare = new RootMeanSquare();

            TemperatureMax = new TemperatureMax();
            TemperatureMin = new TemperatureMin();
            TemperatureArithmeticMean = new TemperatureArithmeticMean();
            TemperatureRootMeanSquare = new TemperatureRootMeanSquare();

            StandardDeviation = new StandardDeviation {ValueOfComparison = ArithmeticMean};

            var baseTable = new DataTable("BaseInfomation");
            baseTable.Columns.Add(new DataColumn("Key", typeof (string)));
            baseTable.Columns.Add(new DataColumn("Value", typeof (string)));
            _DataSet.Tables.Add(baseTable);

            var collectTable = new DataTable("CollectData");
            collectTable.Columns.Add(new DataColumn("datetime", typeof (DateTime)));
            collectTable.Columns.Add(new DataColumn("value", typeof (double)));
            collectTable.Columns.Add(new DataColumn("temperature", typeof (double)));
            collectTable.Columns.Add(new DataColumn("standard_deviation", typeof(double)));
            _DataSet.Tables.Add(collectTable);
        }
        
        [Browsable(false)]
        public IMeter Meter { get; set; }

        public bool Save(string fileFullName)
        {
            return DI.Get<IMeterDataService>().Save(fileFullName, DataSet);
        }

        public event EventHandler<CollectDataEventArgs> ReceviedCollectData;

        [Browsable(false)]
        public DataSet DataSet
        {
            get { return _DataSet; }
        }

        [Browsable(false)]
        public bool HasData
        {
            get { return _DataSet.Tables[1].Rows.Count > 0; }
        }

        public bool Export(string fileFullName, Action<int> rowCount)
        {
            var book = new HSSFWorkbook();

            ISheet sheet1 = book.CreateSheet("测量数据");

            ICellStyle dateStyle = book.CreateCellStyle();
            dateStyle.Alignment = HorizontalAlignment.Left;
            IDataFormat format = book.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy/m/d HH:MM:ss");

            DataRowCollection tableRows = DataSet.Tables[1].Rows;
            for (int i = 0; i < tableRows.Count; i++)
            {
                IRow row = sheet1.CreateRow(i);
                object[] array = tableRows[i].ItemArray;
                for (int j = 0; j < array.Length; j++)
                {
                    if (array[j] is DateTime)
                    {
                        var datetime = (DateTime) array[j];
                        ICell cell = row.CreateCell(j);
                        cell.CellStyle = dateStyle;
                        cell.SetCellValue(datetime);
                    }
                    else if (array[j] is double)
                    {
                        var d = (double) array[j];
                        row.CreateCell(j).SetCellValue(d);
                    }
                    else if (array[j] is int)
                    {
                        var d = (int) array[j];
                        row.CreateCell(j).SetCellValue(d);
                    }
                    else
                    {
                        row.CreateCell(j).SetCellValue(array[j].ToString());
                    }
                }
                rowCount.Invoke(i);
            }
            sheet1.AutoSizeColumn(0);
            sheet1.AutoSizeColumn(1);
            sheet1.AutoSizeColumn(2);
            using (var file = new FileStream(fileFullName, FileMode.Create))
            {
                book.Write(file);
                file.Flush();
                file.Close();
            }

            return true;
        }

        public void Clear()
        {
            _DataSet.Tables[1].Rows.Clear();
            _DataSet.AcceptChanges();

            _CurrentTemperature = 0;

            Max.Clear();
            Min.Clear();
            RootMeanSquare.Clear();
            ArithmeticMean.Clear();

            TemperatureMax.Clear();
            TemperatureMin.Clear();
            TemperatureRootMeanSquare.Clear();
            TemperatureArithmeticMean.Clear();

            StandardDeviation.Clear();

            Ppvalue = 0.ToString();
        }

        public void Add(double value)
        {
            string s = value.ToString();
            int n = s.Length - s.IndexOf('.') - 1;

            Max.Input(value);
            Min.Input(value);
            ArithmeticMean.Input(value);
            RootMeanSquare.Input(value);

            string t = new StringBuilder("{0:N").Append(n).Append("}").ToString();
            Ppvalue = String.Format(t, Math.Abs(Max.Output - Min.Output)); //峰峰值
            StandardDeviation.Input(value);

            if (Math.Abs(_CurrentTemperature) <= 0 && _DataSet.Tables[1].Rows.Count == 0)
                return;
            _DataSet.Tables[1].Rows.Add(DateTime.Now, value, _CurrentTemperature, StandardDeviation.Output);
            //触发数据源发生变化
            OnReceviedCollectData(new CollectDataEventArgs(Meter, CollectData.Build(DateTime.Now, value, _CurrentTemperature)));
        }

        public void AddTemperature(double value)
        {
            _CurrentTemperature = value;
            TemperatureArithmeticMean.Input(value);
            TemperatureRootMeanSquare.Input(value);
            TemperatureMax.Input(value);
            TemperatureMin.Input(value);
        }

        protected virtual void OnReceviedCollectData(CollectDataEventArgs e)
        {
            EventHandler<CollectDataEventArgs> handler = ReceviedCollectData;
            if (handler != null)
                handler(this, e);
        }
    }
}