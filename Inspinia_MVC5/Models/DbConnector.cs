using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Kisumu.Reports.Models
{
    public class DbConnector
    {
        private static SqlConnection Con = null;

        public static string DBConStr = System.Configuration.ConfigurationManager.ConnectionStrings["ReportsConnection"].ToString();

        private SqlConnection sqlConn = null;
        public DbConnector()
        {
            sqlConn = new SqlConnection(DBConStr);
        }
        public SqlConnection GetConnection
        {
            get { return sqlConn; }
        }

        public DbDataReader GetDBResults(ref String errMsg, string StoredProcedure, params object[] ProcedureParameters)
        {
            int i = 0;
            string SQLStatement = "";
            string DataProviderName = "System.Data.SqlClient";
            DbProviderFactory dpf = DbProviderFactories.GetFactory(DataProviderName);
            DbConnection Conn = dpf.CreateConnection();
            Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ReportsConnection"].ToString();

            try
            {
                Conn.Open();
            }
            catch (System.InvalidOperationException ex)
            {
                errMsg = ex.Message.ToString();
                DbDataReader rs = null;
                return rs;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                errMsg = ex.Message.ToString();
                DbDataReader rs = null;
                return rs;
            }
            catch (System.Exception ex)
            {
                errMsg = ex.Message.ToString();
                DbDataReader rs = null;
                return rs;
            }
            DbCommand Command = dpf.CreateCommand();
            Command.Connection = Conn;
            Command.CommandTimeout = 120;
            //SqlTransaction transaction = new SqlTransaction()
            //transaction.Connection=Conn;
            //transaction.Commit

            foreach (object o in ProcedureParameters)
            {
                if (i % 2 == 0)
                    SQLStatement = SQLStatement + (string)o + "=";
                else
                {
                    if (o.GetType() == typeof(string))
                        SQLStatement = SQLStatement + " '" + (string)o + "',";
                    else if (o.GetType() == typeof(DateTime))
                        SQLStatement = SQLStatement + " '" + o.ToString() + "',";
                    else if (o.GetType() == typeof(Int32))
                        SQLStatement = SQLStatement + o.ToString() + ",";
                    else if (o.GetType() == typeof(Double))
                        SQLStatement = SQLStatement + o.ToString() + ",";
                    else if (o.GetType() == typeof(Decimal))
                        SQLStatement = SQLStatement + o.ToString() + ",";
                    else if (o.GetType() == typeof(Int32))
                        SQLStatement = SQLStatement + o.ToString() + ",";
                    else if (o.GetType() == typeof(Boolean))
                    {
                        if ((bool)o == true)
                            SQLStatement = SQLStatement + " 1" + ",";
                        else
                            SQLStatement = SQLStatement + " 0" + ",";
                    }
                    else
                        SQLStatement = SQLStatement + " '" + (string)o + "',";
                }
                i = i + 1;
            }
            if (SQLStatement.Length > 1)
                SQLStatement = SQLStatement.Substring(0, SQLStatement.Length - 1);

            StoredProcedure = "EXEC " + StoredProcedure + " " + SQLStatement;

            Command.CommandType = CommandType.Text;
            Command.CommandText = StoredProcedure;
            DbDataReader ResultSet = null;
            try
            {
                ResultSet = Command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                errMsg = ex.Message.ToString();
                DbDataReader rs = null;
                return rs;
            }
            return ResultSet;
        }
        public static void ExecuteSQL(string Sql)
        {
            try
            {
                if (Con == null)
                {
                    Con = new SqlConnection(DBConStr);
                    Con.Open();
                }
                SqlCommand Cmd = new SqlCommand(Sql, Con);
                Cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }
        }
        public static SqlDataReader GetSqlReader(string Sql)
        {
            try
            {
                if (Con == null)
                {
                    Con = new SqlConnection(DBConStr);
                    Con.Open();
                }
                SqlCommand Cmd = new SqlCommand(Sql, Con);
                return Cmd.ExecuteReader();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}