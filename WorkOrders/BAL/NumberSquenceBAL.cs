using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WorkOrders.BAL
{
    public class NumberSquenceBAL
    {
        public static Int32 GetNextCode(string numberType)
        {
            string query = "";
            DataTable datatable = new DataTable();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnector.ConnectionString))
                {
                    string updatequery = "UPDATE NumberSquenceTable SET NextNumber=NextNumber+1,LastEdit=@lastEdit,LastEditdate=GETDATE() WHERE TypeId=@typeId";
                    query = "SELECT NextNumber FROM NumberSquenceTable WHERE TypeId=@typeId";
                    string InsertQery = "INSERT INTO NumberSquenceTable (TypeId,NextNumber,LastEditdate,LastEdit) VALUES (@typeId,@nextNumber,GETDATE(),@lastEdit)";
                    sqlConnection.Open();
                    SqlCommand SqlCmd = new SqlCommand(query, sqlConnection);
                    SqlCmd.Parameters.Clear();
                    SqlCmd.Parameters.Add("TypeId", SqlDbType.VarChar, 20).Value = numberType;
                    DataSet ds = new DataSet();
                    SqlDataAdapter sqlDataapader = new SqlDataAdapter(SqlCmd);
                    sqlDataapader.Fill(ds);
                    datatable = ds.Tables[0];
                    if (datatable.Rows.Count == 0)
                    {
                        SqlCmd = new SqlCommand(InsertQery, sqlConnection);
                        SqlCmd.Parameters.Clear();
                        SqlCmd.Parameters.Add("@typeId", SqlDbType.VarChar, 20).Value = numberType;
                        SqlCmd.Parameters.Add("@nextNumber", SqlDbType.BigInt).Value = 1000;
                        SqlCmd.Parameters.Add("@lastEdit", SqlDbType.VarChar).Value = "";
                        int rowaffeted = SqlCmd.ExecuteNonQuery();
                        if (rowaffeted == 1)
                        {
                            return 1065;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        SqlCmd = new SqlCommand(updatequery, sqlConnection);
                        SqlCmd.Parameters.Clear();
                        SqlCmd.Parameters.Add("@lastEdit", SqlDbType.VarChar).Value = "";
                        SqlCmd.Parameters.Add("@typeId", SqlDbType.VarChar, 20).Value = numberType;
                        int rowaffeted = SqlCmd.ExecuteNonQuery();
                        if (rowaffeted == 1)
                        {
                            SqlCmd = new SqlCommand(query, sqlConnection);
                            SqlCmd.Parameters.Clear();
                            SqlCmd.Parameters.Add("typeId", SqlDbType.VarChar, 20).Value = numberType;
                            ds = new DataSet();
                            sqlDataapader = new SqlDataAdapter(SqlCmd);
                            sqlDataapader.Fill(ds);
                            datatable = ds.Tables[0];
                            if (datatable.Rows.Count == 1 && datatable.Rows[0]["NextNumber"] != DBNull.Value)
                            {
                                return Int32.Parse(datatable.Rows[0]["NextNumber"].ToString());
                            }
                            else
                            {
                                return 0;
                            }
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return 0;
        }
    }
}