using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RealStateWebApi.DBManager
{
    public class DBHelper
    {
        //public static string StrCon =System.Configuration.ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        public static string StrCon = new RealStateEntities().Database.Connection.ConnectionString;
        public static SqlConnection con;
        public static SqlCommand cmd;
        public static SqlDataAdapter da;
        public static DataTable Dt;

        public static DataTable ReturnTable(string proc, params SqlParameter[] parameter)
        {
            DataTable DtResult = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(StrCon))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(proc, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        foreach (var param in parameter)
                        {
                            param.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(param);
                        }
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(DtResult);
                            return DtResult;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static DataTable ExecuteSQLQuery(string Query)
        {
            DataTable DtResults = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(StrCon))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(DtResults);
                            return DtResults;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static DataSet GetDataSet(string proc, params SqlParameter[] parameters)
        {
            DataSet Ds = new DataSet();
            using (SqlConnection con = new SqlConnection(StrCon))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(proc, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var param in parameters)
                    {
                        param.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param);
                    }
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(Ds);
                        return Ds;
                    }
                }
            }
        }

        public static int ExecuteNonQuery(string proc, params SqlParameter[] parameter)
        {
            int Results = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(StrCon))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(proc, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        foreach (var param in parameter)
                        {
                            param.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(param);
                        }
                        Results = cmd.ExecuteNonQuery();
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public static DataTable GetExcelDataSet(string _excelPath)
        {
            DataTable DtResults = new DataTable();
            try
            {
                string fileExtension = System.IO.Path.GetExtension(_excelPath);
                string excelConnectionString = string.Empty;
                excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _excelPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                //connection String for xls file format.
                if (fileExtension == ".xls")
                {
                    excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _excelPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    //excelConnectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\"", _excelPath);
                }
                //connection String for xlsx file format.
                else if (fileExtension == ".xlsx")
                {

                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _excelPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;\"";
                    //excelConnectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\"", _excelPath);

                }
                //Create Connection to Excel work book and add oledb namespace
                OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                excelConnection.Open();
                DataTable dt = new DataTable();

                dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dt == null)
                {
                    return null;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int t = 0;
                //excel data saves in temp file here.
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[t] = row["TABLE_NAME"].ToString();
                    t++;
                }
                OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
                string query = string.Format("Select * from [{0}]", excelSheets[0]);
                using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                {
                    dataAdapter.Fill(DtResults);
                }
                excelConnection.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {

            }
            return DtResults;
        }
        public static int ExecuteNonQueryWithOutProc(string Query)
        {
            int Results = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(StrCon))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        //cmd.CommandType = CommandType.Text;

                        Results = cmd.ExecuteNonQuery();
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}

