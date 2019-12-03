using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TetrisDAL.Models;
using System.Data.SQLite;
using System.IO;

namespace TetrisDAL.DataOperations
{

    // Запись и чтение результатов, заносимых в БД.
    public class ResultsDAL
    {
        
        private readonly string _connectionString;
        private SQLiteConnection _sqlconnection = null;
        public ResultsDAL() : this("Data Source=TetrisDB.db") { }
        public ResultsDAL(string connectionString)
        {
            if (!File.Exists("TetrisDB.db"))
            {
                SQLiteConnection.CreateFile("TetrisDB.db");
                if (connectionString == "")
                {
                    _sqlconnection = new SQLiteConnection("Data Source = TetrisDB.db");
                }
                else
                {
                    _sqlconnection = new SQLiteConnection(connectionString);
                }

                _sqlconnection.Open();
                string sqlQ = "CREATE TABLE IF NOT EXISTS Results" +
                    "([Name] NVARCHAR(10) NULL," +
                    "[Points] INT NULL," +
                    "[Lines] INT NULL," +
                    "[Time] NVARCHAR(50) NULL);";

                using (SQLiteCommand cmd = new SQLiteCommand(sqlQ, _sqlconnection))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                _sqlconnection.Close();
            }
            else _connectionString = connectionString;
        }

        private void OpenConnection()
        {
            _sqlconnection = new SQLiteConnection { ConnectionString = _connectionString };
            _sqlconnection.Open();
        }
        private void CloseConnection()
        {
            if (_sqlconnection?.State != ConnectionState.Closed)
            {
                _sqlconnection?.Close();
            }
        }

        /// <summary>
        /// Выводим пять лучших результатов.
        /// </summary>
        /// <returns></returns>
        public List<TetrisResults> GetTopFive()
        {
            OpenConnection();
            List<TetrisResults> tRes = new List<TetrisResults>();
            string sql = $"SELECT Name, Points, Lines, Time, rowid FROM Results ORDER BY Points DESC LIMIT 5";
            using (SQLiteCommand command = new SQLiteCommand(sql, _sqlconnection))
            {
                command.CommandType = CommandType.Text;
                using (SQLiteDataReader dbReader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (!dbReader.HasRows)
                    {
                        return null;
                    }
                    while (dbReader.Read())
                    {
                        var c0 = dbReader["Name"].ToString();
                        var c1 = dbReader["Points"].ToString();
                        var c2 = dbReader["Lines"].ToString();
                        var c3 = dbReader["Time"].ToString();
                        var c4 = dbReader[4].ToString();
                        tRes.Add(new TetrisResults
                        {
                            Name = c0,
                            Points = int.Parse(c1),
                            Lines = int.Parse(c2),
                            Time = c3,
                            RecordID = int.Parse(c4)
                        });
                    }
                    dbReader.Close();
                }
            }
            return tRes;
        }

        /// <summary>
        /// Вносим данные в БД.
        /// </summary>
        /// <param name="tr"></param>
        public void InsertNewRecord(TetrisResults tr)
        {
            OpenConnection();
            string cmdInsert = "Insert Into Results" +
                "(Name, Points, Lines, Time) Values" +
                "(@Name, @Points, @Lines, @Time)";
            using (SQLiteCommand command = new SQLiteCommand(cmdInsert, _sqlconnection))
            {

                SQLiteParameter prm = new SQLiteParameter
                {
                    ParameterName = "@Name",
                    Value = tr.Name,
                    DbType = DbType.String,
                    Size = 10,
                    Direction = ParameterDirection.Input
                }; command.Parameters.Add(prm);
                prm = new SQLiteParameter
                {
                    ParameterName = "@Points",
                    Value = tr.Points,
                    DbType = DbType.Int32,
                    Direction = ParameterDirection.Input
                }; command.Parameters.Add(prm);
                prm = new SQLiteParameter
                {
                    ParameterName = "@Lines",
                    Value = tr.Lines,
                    DbType = DbType.Int32,
                    Direction = ParameterDirection.Input
                }; command.Parameters.Add(prm);
                prm = new SQLiteParameter
                {
                    ParameterName = "@Time",
                    Value = tr.Time,
                    DbType = DbType.String,
                    Size = 50,
                    Direction = ParameterDirection.Input
                }; command.Parameters.Add(prm);

                command.ExecuteNonQuery();
                CloseConnection();
            }

        }

    }
}
