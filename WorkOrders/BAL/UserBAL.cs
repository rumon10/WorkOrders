using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WorkOrders.BO;

namespace WorkOrders.BAL
{
    public class UserBAL
    {
        public static  bool ExistsUserName(string userName)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT * FROM Users WHERE [UserName]=@userName";

                using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnector.ConnectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCmd = new SqlCommand(query, sqlConnection);
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.Add("@userName", SqlDbType.VarChar).Value = userName;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCmd);
                    sqlDataAdapter.Fill(dt);
                    sqlCmd.Dispose();
                    sqlDataAdapter.Dispose();
                }

                if (dt == null)
                {
                    return false;
                }
                else
                {
                    //Check datatable contains record that means code exist into database
                    if (dt.Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool addNewUser(UserBO obj)
        {
            try
            {
                int rowAffected = 0;

                using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnector.ConnectionString))
                {
                    string query = "INSERT INTO [Users] ([UserName],[Password],[FullName],[Email],[UserType],[IsEnabled],[LastEdit]) " +
                                                  "VALUES (@userName,@password,@fullName,@email,@userType,@isEnabled,GETDATE())";

                    SqlCommand sqlCmd = new SqlCommand();
                    sqlConnection.Open();
                    sqlCmd.Connection = sqlConnection;
                    sqlCmd.CommandText = query;
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.Add("@userName", SqlDbType.VarChar).Value = obj.UserName;
                    sqlCmd.Parameters.Add("@password", SqlDbType.VarChar).Value = obj.Password;
                    sqlCmd.Parameters.Add("@fullName", SqlDbType.VarChar).Value = obj.FullName;
                    sqlCmd.Parameters.Add("@email", SqlDbType.VarChar).Value = obj.Email;
                    sqlCmd.Parameters.Add("@userType", SqlDbType.VarChar).Value = obj.UserType;
                    sqlCmd.Parameters.Add("@isEnabled", SqlDbType.Bit).Value = obj.IsEnable;
                    rowAffected = sqlCmd.ExecuteNonQuery();
                }

                return (rowAffected > 0 ? true : false);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool editUser(UserBO obj)
        {
            try
            {
                int rowAffected = 0;

                using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnector.ConnectionString))
                {
                    string query = "UPDATE [Users] SET [FullName]=@fullName,[Email]=@email,[UserType]=@userType,[IsEnabled]=@isEnabled,[LastEdit]=GETDATE() WHERE [UserName]=@userName";

                    SqlCommand sqlCmd = new SqlCommand();
                    sqlConnection.Open();
                    sqlCmd.Connection = sqlConnection;
                    sqlCmd.CommandText = query;
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.Add("@fullName", SqlDbType.VarChar).Value = obj.FullName;
                    sqlCmd.Parameters.Add("@email", SqlDbType.VarChar).Value = obj.Email;
                    sqlCmd.Parameters.Add("@userType", SqlDbType.VarChar).Value = obj.UserType;
                    sqlCmd.Parameters.Add("@isEnabled", SqlDbType.Bit).Value = obj.IsEnable;
                    sqlCmd.Parameters.Add("@userName", SqlDbType.VarChar).Value = obj.UserName;
                    rowAffected = sqlCmd.ExecuteNonQuery();
                }

                return (rowAffected > 0 ? true : false);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool deleteUser(string userName)
        {
            try
            {
                int rowAffected = 0;

                using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnector.ConnectionString))
                {
                    string query = "DELETE FROM [Users]  WHERE [UserName]=@userName";

                    SqlCommand sqlCmd = new SqlCommand();
                    sqlConnection.Open();
                    sqlCmd.Connection = sqlConnection;
                    sqlCmd.CommandText = query;
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.Add("@userName", SqlDbType.VarChar).Value = userName;
                    rowAffected = sqlCmd.ExecuteNonQuery();
                }

                return (rowAffected > 0 ? true : false);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable  UserInfo()
        {
            DataTable data = new DataTable();
            string query = "SELECT [UserId],[UserName],[Password],[FullName],[Email],[UserType],[IsEnabled],[LastEdit] FROM Users ORDER BY UserId ASC";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnector.ConnectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlcmd = new SqlCommand(query, sqlConnection);
                    sqlcmd.Parameters.Clear();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlcmd);
                    sqlDataAdapter.Fill(data);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return data;
        }

        public bool changePassword(string username, string newpassword)
        {
            try
            {
                int rowAffected = 0;

                using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnector.ConnectionString))
                {
                    string query = "UPDATE [Users] SET [Password]=@password, [LastEdit]=GETDATE() WHERE [UserName]=@userName";

                    SqlCommand sqlCmd = new SqlCommand();
                    sqlConnection.Open();
                    sqlCmd.Connection = sqlConnection;
                    sqlCmd.CommandText = query;
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.Add("@password", SqlDbType.VarChar).Value = newpassword;
                    sqlCmd.Parameters.Add("@userName", SqlDbType.VarChar).Value = username;
                    rowAffected = sqlCmd.ExecuteNonQuery();
                }

                return (rowAffected > 0 ? true : false);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserBO userLogin(string userName)
        {
            UserBO userBO = new UserBO();
            string query = "SELECT [UserId],[UserName],[Password],[FullName],[Email],[UserType],[IsEnabled],[LastEdit] FROM Users WHERE [UserName]=@userName";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnector.ConnectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlcmd = new SqlCommand(query, sqlConnection);
                    sqlcmd.Parameters.Clear();
                    sqlcmd.Parameters.Add("@userName", SqlDbType.VarChar).Value = userName;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlcmd);
                    DataTable data = new DataTable();
                    sqlDataAdapter.Fill(data);

                    if (data.Rows.Count > 0)
                    {
                        userBO.Email = (data.Rows[0]["Email"] == DBNull.Value ? "" : data.Rows[0]["Email"].ToString());
                        userBO.FullName = (data.Rows[0]["FullName"] == DBNull.Value ? "" : data.Rows[0]["FullName"].ToString());
                        userBO.IsEnable = (data.Rows[0]["IsEnabled"] == DBNull.Value ? false : Convert.ToBoolean(data.Rows[0]["IsEnabled"].ToString()));
                        userBO.LastEdit = (data.Rows[0]["LastEdit"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(data.Rows[0]["LastEdit"].ToString()));
                        userBO.Password = (data.Rows[0]["Password"] == DBNull.Value ? "" : data.Rows[0]["Password"].ToString());
                        userBO.UserId = (data.Rows[0]["UserId"] == DBNull.Value ? 0 : Convert.ToInt16(data.Rows[0]["UserId"].ToString()));
                        userBO.UserName = (data.Rows[0]["UserName"] == DBNull.Value ? "" : data.Rows[0]["UserName"].ToString());
                        userBO.UserType = (data.Rows[0]["UserType"] == DBNull.Value ? "User" : data.Rows[0]["UserType"].ToString ());
                      }
                    else
                    {
                        userBO = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return userBO;
        }
    }
}