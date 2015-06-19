using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
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
        [DisplayName("最小值")]
        public double Min { get; private set; }

        [Category("数据分析")]
        [DisplayName("峰峰值")]
        public string Ppvalue { get; private set; }

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

        [Category("温度"), DisplayName("最小值")]
        public double MinTemperature { get; private set; }

        [Category("温度"), DisplayName("均方根")]
        public double TemperatureRootMeanSquare { get; private set; }

        [Category("温度"), DisplayName("算术平均")]
        public double TemperatureArithmeticMean { get; private set; }

        [Browsable(false)]
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

        public bool Export(string fileFullName)
        {
            var book = new HSSFWorkbook();

            ISheet sheet1 = book.CreateSheet("测量数据");

            var dateStyle = book.CreateCellStyle();
            dateStyle.Alignment = HorizontalAlignment.Left;
            var format = book.CreateDataFormat();
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
                        var cell = row.CreateCell(j);
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

        public void SetNominalValue(double nominalValue)
        {
            _NominalValue = nominalValue;
        }

        public void Add(double value)
        {
            var s = value.ToString();
            var n = s.Length - s.IndexOf('.') - 1;

            _DataSet.Tables[1].Rows.Add(DateTime.Now, value, _CurrentTemperature);
            int count = _DataSet.Tables[1].Rows.Count;
            if (count <= 1)
            {
                Max = value;
                Min = value;
            }
            else
            {
                if (value > Max) 
                    Max = value; //最大值
                else if (value < Min)
                    Min = value; //最小值
            }
            var t = new StringBuilder("{0:N").Append(n).Append("}").ToString();
            Ppvalue = String.Format(t, Math.Abs(Max - Min));//峰峰值
            _SumData += value;
            ArithmeticMean = Math.Round(_SumData / count, n);//算术平均

            //计算均方根
            _RmsData += value*value;
            RootMeanSquare = Math.Round(Math.Sqrt(_RmsData/count), n);

            if (Math.Abs(_NominalValue) > 0)
            {
                RootMeanSquareDeviation = RootMeanSquare - _NominalValue;
            }

            //触发数据源发生变化
            OnReceviedCollectData(new CollectDataEventArgs(Meter, CollectData.Build(DateTime.Now, value, _CurrentTemperature)));
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

        protected virtual void OnReceviedCollectData(CollectDataEventArgs e)
        {
            EventHandler<CollectDataEventArgs> handler = ReceviedCollectData;
            if (handler != null)
                handler(this, e);
        }

        public void Clear()
        {
            _DataSet.Tables[1].Rows.Clear();
            _DataSet.AcceptChanges();

            Max = 0;
            Min = 0;
            Ppvalue = 0.ToString();
            RootMeanSquare = 0;
            ArithmeticMean = 0;

            MaxTemperature = 0;
            MinTemperature = 0;
            TemperatureRootMeanSquare = 0;
            TemperatureArithmeticMean = 0;

            AllanVariance = 0;
            RootMeanSquareDeviation = 0;
        }
    }
}