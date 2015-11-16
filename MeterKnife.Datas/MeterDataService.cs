using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Common.Logging;
using MeterKnife.Common.Interfaces;
using Simple.Data;

namespace MeterKnife.Datas
{
    public class MeterDataService : IMeterDataService
    {
        private static readonly ILog _logger = LogManager.GetLogger<MeterDataService>();

        public bool Save(string fileFullName, DataSet dataSet)
        {
            string connectionString = string.Format("Data Source={0};Version=3", fileFullName);
            var conn = new SQLiteConnection(connectionString);
            conn.Open();

            using (var cmd = new SQLiteCommand())
            {
                cmd.Connection = conn;
                var helper = new SqliteHelper(cmd);

                var tb = new SqliteTable("meter");

                tb.Columns.Add(new SqliteColumn("property_name"));
                tb.Columns.Add(new SqliteColumn("property_value"));

                helper.CreateTable(tb);
                _logger.Info("Table:meter is created!");

                tb = new SqliteTable("collectdata");

                tb.Columns.Add(new SqliteColumn("datetime", SqliteColumnType.DateTime));
                tb.Columns.Add(new SqliteColumn("temperature", SqliteColumnType.Decimal));
                tb.Columns.Add(new SqliteColumn("value", SqliteColumnType.Decimal));

                helper.CreateTable(tb);
                _logger.Info("Table:collect_data is created!");
            }

            var db = Database.OpenConnection(connectionString);
            db.UseSharedConnection(conn);

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                db.meter.Insert(property_name: row[0], property_value: row[1]);
            }
            foreach (DataRow row in dataSet.Tables[1].Rows)
            {
                db.collectdata.Insert(datetime: row[0], temperature: row[2], value: row[1]);
            }

            db.StopUsingSharedConnection();
            conn.Close();

            return true;
        }
    }
}
