using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TetrisDAL.Models;

namespace TetrisDAL.DataOperations
{
    public class ResultsDAL
    {
        private readonly string _connectionString;
        private SqlConnection _sqlconnection = null;
        public ResultsDAL() : this(@"Data Source=(local);
                                    Integrated Security = true; 
                                    Initial Catalog = TetrisApp") { }
        public ResultsDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        private void OpenConnection()
        {
            _sqlconnection = new SqlConnection { ConnectionString = _connectionString };
            _sqlconnection.Open();
        }
        private void CloseConnection()
        {
            if(_sqlconnection?.State != ConnectionState.Closed)
            {
                _sqlconnection?.Close();
            }
        }

        public List<TetrisResults> GetTopFive()
        {
            OpenConnection();
            List<TetrisResults> tRes = new List<TetrisResults>();
            string sql = $"SELECT TOP 5 * FROM Results ORDER BY Points DESC";
            using (SqlCommand command = new SqlCommand(sql, _sqlconnection))
            {
                command.CommandType = CommandType.Text;
                SqlDataReader dbReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if(!dbReader.HasRows)
                {
                    return null;
                }
                while(dbReader.Read())
                {
                    tRes.Add(new TetrisResults
                    {
                        RecordID = (int)dbReader["RecordID"],
                        Name = (string)dbReader["Name"],
                        Points = (int)dbReader["Points"],
                        Lines = (int)dbReader["Lines"],
                        Time = (string)dbReader["Time"]
                    });
                }
                dbReader.Close();
            }
            return tRes;
        }
        public void InsertNewRecord(TetrisResults tr)
        {
            OpenConnection();
            string cmdInsert = "Insert Into Results" +
                "(Name, Points, Lines, Time) Values" +
                "(@Name, @Points, @Lines, @Time)";
            using (SqlCommand command = new SqlCommand(cmdInsert, _sqlconnection))
            {

                SqlParameter prm = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = tr.Name,
                    SqlDbType = SqlDbType.Char,
                    Size = 10,
                    Direction = ParameterDirection.Input
                }; command.Parameters.Add(prm);
                prm = new SqlParameter
                {
                    ParameterName = "@Points",
                    Value = tr.Points,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                }; command.Parameters.Add(prm);
                prm = new SqlParameter
                {
                    ParameterName = "@Lines",
                    Value = tr.Lines,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                }; command.Parameters.Add(prm);
                prm = new SqlParameter
                {
                    ParameterName = "@Time",
                    Value = tr.Time,
                    SqlDbType = SqlDbType.Char,
                    Size = 50,
                    Direction = ParameterDirection.Input
                }; command.Parameters.Add(prm);

                command.ExecuteNonQuery();
                CloseConnection();
            }

        }

    }
}
