using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WorkOrders.BO;

namespace WorkOrders.BAL
{
    public class CustomerBAL
    {
        public DataTable GetCustomerList()
        {
            DataTable dtCustomerList = new DataTable ();
            string query = "SELECT CustomerId,CustomerName FROM CustTable ORDER BY CustomerName ASC";
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

        public CustomerBO GetCustomerInformation(int customerId)
        {
            CustomerBO customerBO = new CustomerBO();
            string query = "SELECT CustomerId,CustomerName,ContactNo,EmailAddress,[Address],[City],[State],[ZipCode],[Status] FROM CustTable WHERE CustomerId=@customerId";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnector.ConnectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlcmd = new SqlCommand(query, sqlConnection);
                    sqlcmd.Parameters.Clear();
                    sqlcmd.Parameters.Add("@customerId",SqlDbType.Int).Value = customerId;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlcmd);
                    DataTable customerdata = new DataTable();
                    sqlDataAdapter.Fill(customerdata);

                    if (customerdata.Rows.Count == 0)
                    {
                        customerBO = null;
                    }
                    else
                    {
                        customerBO.Address = (customerdata.Rows[0]["Address"] == DBNull.Value ? string.Empty : customerdata.Rows[0]["Address"].ToString ());
                        customerBO.City  = (customerdata.Rows[0]["City"] == DBNull.Value ? string.Empty : customerdata.Rows[0]["City"].ToString());
                        customerBO.ContactNo  = (customerdata.Rows[0]["ContactNo"] == DBNull.Value ? string.Empty : customerdata.Rows[0]["ContactNo"].ToString());
                        customerBO.CustomerId  = (customerdata.Rows[0]["CustomerId"] == DBNull.Value ? 0 : Convert.ToInt16 (customerdata.Rows[0]["CustomerId"].ToString()));
                        customerBO.CustomerName  = (customerdata.Rows[0]["CustomerName"] == DBNull.Value ? string.Empty : customerdata.Rows[0]["CustomerName"].ToString());
                        customerBO.EmailAddress  = (customerdata.Rows[0]["EmailAddress"] == DBNull.Value ? string.Empty : customerdata.Rows[0]["EmailAddress"].ToString());
                        customerBO.State  = (customerdata.Rows[0]["State"] == DBNull.Value ? string.Empty : customerdata.Rows[0]["State"].ToString());
                        customerBO.Status  = (customerdata.Rows[0]["Status"] == DBNull.Value ? false  : Convert.ToBoolean (customerdata.Rows[0]["Status"].ToString()));
                        customerBO.ZipCode  = (customerdata.Rows[0]["ZipCode"] == DBNull.Value ? string.Empty : customerdata.Rows[0]["ZipCode"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return customerBO;
        }
    }
}