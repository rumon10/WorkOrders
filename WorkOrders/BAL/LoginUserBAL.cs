using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WorkOrders.BO;

namespace WorkOrders.BAL
{
    public class LoginUserBAL
    {
        public bool userLogin(string userName, string password,out UserBO userBO, out string messsage)
        {
            userBO = new UserBO ();
            messsage = "";
            string query = "SELECT [UserId],[UserName],[Password],[FullName],[Email],[UserType],[IsEnabled],[LastEdit] FROM Users WHERE [UserName]=@userName AND [Password]=@password";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnector.ConnectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlcmd = new SqlCommand(query, sqlConnection);
                    sqlcmd.Parameters.Clear();
                    sqlcmd.Parameters.Add("@userName", SqlDbType.VarChar).Value = userName;
                    sqlcmd .Parameters.Add ("@password",SqlDbType.VarChar).Value = password;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlcmd);
                    DataTable data = new DataTable();
                    sqlDataAdapter.Fill(data);

                    if (data.Rows.Count > 0)
                    {
                        messsage = string.Empty;
                        userBO.Email = (data.Rows[0]["Email"] == DBNull.Value ? "" : data.Rows[0]["Email"].ToString());
                        userBO.FullName = (data.Rows[0]["FullName"] == DBNull.Value ? "" : data.Rows[0]["FullName"].ToString());
                        userBO.IsEnable = (data.Rows[0]["IsEnabled"] == DBNull.Value ? false : Convert.ToBoolean(data.Rows[0]["IsEnabled"].ToString()));
                        userBO.LastEdit = (data.Rows[0]["LastEdit"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(data.Rows[0]["LastEdit"].ToString()));
                        userBO.Password = (data.Rows[0]["Password"] == DBNull.Value ? "" : data.Rows[0]["Password"].ToString());
                        userBO.UserId = (data.Rows[0]["UserId"] == DBNull.Value ? 0 : Convert.ToInt16(data.Rows[0]["UserId"].ToString()));
                        userBO.UserName = (data.Rows[0]["UserName"] == DBNull.Value ? "" : data.Rows[0]["UserName"].ToString());
                        userBO.UserType  = (data.Rows[0]["UserType"] == DBNull.Value ? "User" : data.Rows[0]["UserType"].ToString ());

                        if (userBO.IsEnable == false)
                            messsage = "User " + userBO.UserName + " is now disabled. Please contract administrator to enable it.";
                    }
                    else
                    {
                        messsage = "Incorrect Username or Password. Please enter the valid Username and secure Password.";
                        userBO = null;
                    }
                }
            }
            catch (Exception ex)
            {
                messsage = ex.Message;
                throw ex;
            }

            return (userBO == null ? false : true);
        }
    }
}