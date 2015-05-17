using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using Common.Logging;
using MeterKnife.Common.EventParameters;
using MeterKnife.Common.Interfaces;
using NKnife.IoC;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace MeterKnife.Common.DataModels
{
    public class FiguredData : ICollectSource
    {
        #region 分析内容

        [Category("数据分析"), DisplayName("最大值")]
        public double Max { get; private set; }

        [Category("数据分析")]
        [DisplayName("最大值")]
        public double Min { get; private set; }

        [Category("数据分析")]
        [DisplayName("均方根")]
        public double RootMeanSquare { get; private set; }

        [Category("数据分析"), DisplayName("算术平均")]
        public double ArithmeticMean { get; private set; }

        [Category("数据分析"), DisplayName("总采样数")]
        public uint Count
        {
            get { return (uint) _DataSet.Tables[1].Rows.Count; }
        }

        [Category("温度"), DisplayName("最大值")]
        public double MaxTemperature { get; private set; }

        [Category("温度"), DisplayName("最大值")]
        public double MinTemperature { get; private set; }

        [Category("温度"), DisplayName("均方根")]
        public double TemperatureRootMeanSquare { get; private set; }

        [Category("温度"), DisplayName("算术平均")]
        public double TemperatureArithmeticMean { get; private set; }

        [Category("偏差"), DisplayName("阿仑方差")]
        public double AllanVariance { get; private set; }

        [Category("偏差"), DisplayName("均方根差")]
        public double RootMeanSquareDeviation { get; private set; }

        #endregion

        private static readonly ILog _logger = LogManager.GetLogger<FiguredData>();
        private double _CurrentTemperature;
        protected DataSet _DataSet = new DataSet();

        private double _NominalValue;

        private double _RmsData;
        private double _RmsTemperatureData;
        private double _SumData;
        private double _SumTemperatureData;

        public FiguredData()
        {
            var baseTable = new DataTable("BaseInfomation");
            baseTable.Columns.Add(new DataColumn("Key", typeof (string)));
            baseTable.Columns.Add(new DataColumn("Value", typeof (string)));
            _DataSet.Tables.Add(baseTable);
            var collectTable = new DataTable("CollectData");
            collectTable.Columns.Add(new DataColumn("datetime", typeof (DateTime)));
            collectTable.Columns.Add(new DataColumn("value", typeof (double)));
            collectTable.Columns.Add(new DataColumn("temperature", typeof (double)));
            _DataSet.Tables.Add(collectTable);
        }

        [Browsable(false)]
        public IMeter Meter { get; set; }

        public bool Save(string fileFullName)
        {
            return DI.Get<IMeterDataService>().Save(fileFullName, DataSet);
        }

        public event EventHandler<CollectEventArgs> ReceviedCollectData;

        [Browsable(false)]
        public DataSet DataSet
        {
            get { return _DataSet; }
        }

        public void SetNominalValue(double nominalValue)
        {
            _NominalValue = nominalValue;
        }

        public bool Export(string fileFullName)
        {
            var hssfworkbook = new HSSFWorkbook();

            ISheet sheet1 = hssfworkbook.CreateSheet("测量数据");

            DataRowCollection tableRows = DataSet.Tables[1].Rows;
            for (int i = 0; i < DataSet.Tables[1].Rows.Count; i++)
            {
                IRow row = sheet1.CreateRow(i);
                object[] array = tableRows[i].ItemArray;
                for (int j = 0; j < array.Length; j++)
                {
                    row.CreateCell(j).SetCellValue(array[j].ToString());
                }
                sheet1.AutoSizeColumn(0);
                sheet1.AutoSizeColumn(1);
                sheet1.AutoSizeColumn(2);
            }
            using (var file = new FileStream(fileFullName, FileMode.Create))
            {
                hssfworkbook.Write(file);
                file.Close();
            }
            return true;
        }

        public void Add(double value)
        {
            _DataSet.Tables[1].Rows.Add(DateTime.Now, value, _CurrentTemperature);
            int count = _DataSet.Tables[1].Rows.Count;
            if (count <= 1)
            {
                Max = value;
                Min = value;
            }
            else
            {
                if (value > Max) Max = value; //最大值
                else if (value < Min) Min = value; //最小值
            }
            _SumData += value;
            ArithmeticMean = _SumData/count;

            //计算均方根
            _RmsData += value*value;
            RootMeanSquare = Math.Sqrt(_RmsData/count);

            if (Math.Abs(_NominalValue) > 0)
            {
                RootMeanSquareDeviation = RootMeanSquare - _NominalValue;
            }

            //触发数据源发生变化
            OnReceviedCollectData(new CollectEventArgs(Meter, CollectData.Build(DateTime.Now, value, _CurrentTemperature)));
        }

        public void AddTemperature(double value)
        {
            _CurrentTemperature = value;
            int count = _DataSet.Tables[1].Rows.Count;
            if (count <= 1)
            {
                MaxTemperature = value;
                MinTemperature = value;
            }
            else
            {
                if (value > MaxTemperature) MaxTemperature = value;
                else if (value < MinTemperature) MinTemperature = value;
            }
            _SumTemperatureData += value;
            TemperatureArithmeticMean = Math.Round(_SumTemperatureData/count, 4);

            //计算均方根
            _RmsTemperatureData += value*value;
            TemperatureRootMeanSquare = Math.Round(Math.Sqrt(_RmsTemperatureData/count), 4);
        }

        protected virtual void OnReceviedCollectData(CollectEventArgs e)
        {
            EventHandler<CollectEventArgs> handler = ReceviedCollectData;
            if (handler != null)
                handler(this, e);
        }
    }
}