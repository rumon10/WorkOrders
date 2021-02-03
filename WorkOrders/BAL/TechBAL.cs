using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WorkOrders.BAL
{
    public class TechBAL
    {
        public DataTable GetTechList()
        {
            DataTable dtCustomerList = new DataTable ();
            string query = "SELECT TechId,TechName FROM TechTable ORDER BY TechName ASC";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnector.ConnectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlcmd = new SqlCommand(query, sqlConnection);
                    sqlcmd.Parameters.Clear();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlcmd);
                    sqlDataAdapter.Fill(dtCustomerList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtCustomerList;
        }
    }
}