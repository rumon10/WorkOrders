using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using WorkOrders.BO;

namespace WorkOrders.BAL
{
    public class WorkOrderBAL
    {
        public static bool WorkOrderIdExist(string workOrderId)
        {
            try
            {
                DataTable dt = new DataTable ();
                string query = "SELECT * FROM WorkOrder WHERE WorkOrderId=@workOrderId";

                using (SqlConnection sqlConnection = new SqlConnection (DatabaseConnector.ConnectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCmd = new SqlCommand(query, sqlConnection);
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.Add("@workOrderId",SqlDbType.BigInt).Value = workOrderId;
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

        public void getNavigatorCount(Int64 currentworkId, out int totalRecord, out int currentRecordNo)
        {
            try
            {
                DataTable dt = new DataTable();
                string totalRecordQuery = "SELECT COUNT(*) FROM WorkOrder";

                using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnector.ConnectionString))
                {
                    sqlConnection.Open();
                    // Total Record Query
                    SqlCommand sqlCmd = new SqlCommand(totalRecordQuery, sqlConnection);
                    sqlCmd.Parameters.Clear();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCmd);
                    sqlDataAdapter.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        totalRecord = 0;
                    }
                    else
                    {
                        totalRecord = (dt.Rows[0][0] == DBNull.Value ? 0 : Convert.ToInt16(dt.Rows[0][0]));
                    }

                    // Current Record Query
                    string currentRecordQuery = "SELECT COUNT(*) FROM WorkOrder WHERE WorkOrderId <= @workOrderId";
                    sqlCmd = new SqlCommand(currentRecordQuery, sqlConnection);
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.Add("@workOrderId",SqlDbType.BigInt).Value = currentworkId;
                    sqlDataAdapter = new SqlDataAdapter(sqlCmd);
                    dt = new DataTable();
                    sqlDataAdapter.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        currentRecordNo = 0;
                    }
                    else
                    {
                        currentRecordNo = (dt.Rows[0][0] == DBNull.Value ? 0 : Convert.ToInt16(dt.Rows[0][0]));
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool crateWorokOrder(WorkOrderBO orderBO,List<WorkOrderLineBO> orderlineListBO)
        {
            //Declare Transaction
            SqlTransaction transaction = null;
            decimal totalUsedTime = 0;

            try
            {
                if (orderBO == null || (orderlineListBO == null || orderlineListBO.Count == 0))
                {
                    return false;
                }

                int rowAffected = 0;

                using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnector.ConnectionString))
                { 
                    string workOrderCreateQuery = "INSERT INTO [WorkOrder]([WorkOrderId],[OrderDate],[CustomerId],[TechId],[TotalCharge],[CreateDateTime],[LastEditDate],[LastEdit]) " +
                                                  "VALUES (@workOrderId,@OrderDate,@customerId,@techId,@totalCharge,GETDATE(),GETDATE(),@lastEdit)";
                    
                    
                    
                    string orderLineCreateQuery = "INSERT INTO [WorkOrderLine]([WorkOrderId],[WorkType],[Description],[UsedTime],[LastEditDate]) "+
                                                   "VALUES (@workOrderId,@workType,@description,@usedTime,GETDATE())";

                    string updateWorkTable ="UPDATE WorkOrder SET TotalUsedTime=@totalUsedTime WHERE WorkOrderId=@workOrderId";
                    
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlConnection.Open();
                    sqlCmd.Connection = sqlConnection;

                    //Init Transaction
                    transaction = sqlConnection.BeginTransaction(IsolationLevel.ReadCommitted);

                    //Insert work order Table Record
                    sqlCmd.Connection = sqlConnection;
                    sqlCmd.Transaction = transaction;
                    sqlCmd.CommandText = workOrderCreateQuery;
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.Add ("@workOrderId",SqlDbType.BigInt).Value = orderBO.WorkOrderId;
                    sqlCmd.Parameters.Add ("@OrderDate",SqlDbType.DateTime).Value = orderBO.OrderDate;
                    sqlCmd.Parameters.Add ("@customerId",SqlDbType.Int).Value = orderBO.CustomerId;
                    sqlCmd.Parameters.Add ("@techId",SqlDbType.Int).Value = orderBO.TechId;
                    sqlCmd.Parameters.Add ("@totalCharge",SqlDbType.Money).Value = orderBO.TotalCharge;
                    sqlCmd.Parameters.Add("@lastEdit", SqlDbType.VarChar).Value = orderBO.LastEdit;
                    rowAffected = sqlCmd.ExecuteNonQuery();

                    if (rowAffected == 0)
                    {
                        transaction.Rollback();
                        return false;
                    }

                    //Insert workOrderLine Table Record
                    for(int i=0; i<orderlineListBO.Count; ++i)
                    {
                        sqlCmd.CommandText = orderLineCreateQuery;
                        sqlCmd.Parameters.Clear();
                        sqlCmd.Parameters.Add ("@workOrderId",SqlDbType.BigInt).Value = orderlineListBO[i].WorkOrderId;
                        sqlCmd.Parameters.Add ("@workType",SqlDbType.VarChar,50).Value = orderlineListBO[i].WorkType;
                        sqlCmd.Parameters.Add ("@description",SqlDbType.VarChar,500).Value = orderlineListBO[i].Description;
                        sqlCmd.Parameters.Add ("@usedTime",SqlDbType.Decimal).Value = orderlineListBO[i].UsedTime;
                        rowAffected = sqlCmd.ExecuteNonQuery ();
                        totalUsedTime = totalUsedTime + orderlineListBO[i].UsedTime;
                        
                        if(rowAffected == 0)
                        {
                            break ;
                        }
                    }

                    if(rowAffected == 0)
                    {
                        transaction.Rollback ();
                        return false;
                    }

                    
                    //Update work Order Table
                    sqlCmd .CommandText = updateWorkTable;
                    sqlCmd .Parameters.Clear ();
                    sqlCmd .Parameters.Add ("@totalUsedTime",SqlDbType.Decimal).Value = totalUsedTime;
                    sqlCmd .Parameters.Add ("@workOrderId",SqlDbType.BigInt).Value = orderBO.WorkOrderId;
                    rowAffected = sqlCmd.ExecuteNonQuery ();

                    if (rowAffected > 0)
                    {
                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
                
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                throw ex;
            }
        }

        public bool saveWorokOrder(WorkOrderBO orderBO, List<WorkOrderLineBO> orderlineListBO)
        {
            //Declare Transaction
            SqlTransaction transaction = null;
            decimal totalUsedTime = 0;

            try
            {
                if (orderBO == null || (orderlineListBO == null || orderlineListBO.Count == 0))
                {
                    return false;
                }

                int rowAffected = 0;

                using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnector.ConnectionString))
                {
                    string workOrderUpdateQuery = "UPDATE [WorkOrder] SET [OrderDate]=@OrderDate,[CustomerId]=@customerId,[TechId]=@techId,[TotalCharge]=@totalCharge,[LastEditDate]=GETDATE(),[LastEdit]=@lastEdit WHERE WorkOrderId=@workOrderId";

                    string workLineDeleteQuery = "DELETE FROM [WorkOrderLine] WHERE WorkOrderId=@workOrderId";
                    string orderLineCreateQuery = "INSERT INTO [WorkOrderLine]([WorkOrderId],[WorkType],[Description],[UsedTime],[LastEditDate]) " +
                                                   "VALUES (@workOrderId,@workType,@description,@usedTime,GETDATE())";
                    string updateWorkTable = "UPDATE WorkOrder SET TotalUsedTime=@totalUsedTime WHERE WorkOrderId=@workOrderId";

                    SqlCommand sqlCmd = new SqlCommand();
                    sqlConnection.Open();
                    sqlCmd.Connection = sqlConnection;

                    //Init Transaction
                    transaction = sqlConnection.BeginTransaction(IsolationLevel.ReadCommitted);

                    //Update work order Table Record
                    sqlCmd.Connection = sqlConnection;
                    sqlCmd.Transaction = transaction;
                    sqlCmd.CommandText = workOrderUpdateQuery;
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.Add("@OrderDate", SqlDbType.DateTime).Value = orderBO.OrderDate;
                    sqlCmd.Parameters.Add("@customerId", SqlDbType.Int).Value = orderBO.CustomerId;
                    sqlCmd.Parameters.Add("@techId", SqlDbType.Int).Value = orderBO.TechId;
                    sqlCmd.Parameters.Add("@totalCharge", SqlDbType.Money).Value = orderBO.TotalCharge;
                    sqlCmd.Parameters.Add("@lastEdit", SqlDbType.VarChar).Value = orderBO.LastEdit;
                    sqlCmd.Parameters.Add("@workOrderId", SqlDbType.BigInt).Value = orderBO.WorkOrderId;
                    rowAffected = sqlCmd.ExecuteNonQuery();

                    if (rowAffected == 0)
                    {
                        transaction.Rollback();
                        return false;
                    }

                    //Order Line Delete Query
                    sqlCmd.CommandText = workLineDeleteQuery;
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.Add("@workOrderId", SqlDbType.BigInt).Value = orderBO.WorkOrderId;
                    rowAffected = sqlCmd.ExecuteNonQuery();

                    if (rowAffected == 0)
                    {
                        // Check Work Order Exists or Not
                        sqlCmd.CommandText = "SELECT * FROM WorkOrderLine WHERE WorkOrderId=@workOrderId";
                        sqlCmd.Parameters.Clear();
                        sqlCmd.Parameters.Add("@workOrderId", SqlDbType.BigInt).Value = orderBO.WorkOrderId;
                        DataTable data = new DataTable();
                        SqlDataAdapter dataApadpter = new SqlDataAdapter(sqlCmd);
                        dataApadpter.Fill(data);
                        if (data.Rows.Count > 0)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }

                    //Insert workOrderLine Table Record
                    for (int i = 0; i < orderlineListBO.Count; ++i)
                    {
                        sqlCmd.CommandText = orderLineCreateQuery;
                        sqlCmd.Parameters.Clear();
                        sqlCmd.Parameters.Add("@workOrderId", SqlDbType.BigInt).Value = orderlineListBO[i].WorkOrderId;
                        sqlCmd.Parameters.Add("@workType", SqlDbType.VarChar, 50).Value = orderlineListBO[i].WorkType;
                        sqlCmd.Parameters.Add("@description", SqlDbType.VarChar, 500).Value = orderlineListBO[i].Description;
                        sqlCmd.Parameters.Add("@usedTime", SqlDbType.Decimal).Value = orderlineListBO[i].UsedTime;
                        rowAffected = sqlCmd.ExecuteNonQuery();
                        totalUsedTime = totalUsedTime + orderlineListBO[i].UsedTime;

                        if (rowAffected == 0)
                        {
                            break;
                        }
                    }

                    if (rowAffected == 0)
                    {
                        transaction.Rollback();
                        return false;
                    }


                    //Update work Order Table
                    sqlCmd.CommandText = updateWorkTable;
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.Add("@totalUsedTime", SqlDbType.Decimal).Value = totalUsedTime;
                    sqlCmd.Parameters.Add("@workOrderId", SqlDbType.BigInt).Value = orderBO.WorkOrderId;
                    rowAffected = sqlCmd.ExecuteNonQuery();

                    if (rowAffected > 0)
                    {
                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                throw ex;
            }
        }

        public void getWorkOrderInformation(Int64 workOrderId, out WorkOrderBO workOrderBO, out DataTable orderLineData)
        {
            try
            {
                string orderQuery = "SELECT WorkOrder.[WorkOrderId],WorkOrder.[OrderDate],WorkOrder.[CustomerId],CustTable.CustomerName,WorkOrder.[TechId],TechTable.TechName,WorkOrder.[TotalUsedTime],WorkOrder.[TotalCharge],WorkOrder.[CreateDateTime],WorkOrder.[LastEditDate] FROM [WorkOrder] LEFT JOIN CustTable ON CustTable.CustomerId=WorkOrder.CustomerId LEFT JOIN TechTable ON TechTable.TechId=WorkOrder.TechId WHERE WorkOrder.[WorkOrderId]=@workOrderId";
                DataTable orderData = new DataTable();
                string orderLineQuery = "SELECT [WorkType],[Description],[UsedTime] FROM WorkOrderLine WHERE [WorkOrderId]=@workOrderId";
                DataTable linedata = new DataTable();

                using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnector.ConnectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCmd = new SqlCommand(orderQuery, sqlConnection);
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.Add("@workOrderId", SqlDbType.BigInt).Value = workOrderId;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCmd);
                    sqlDataAdapter.Fill(orderData);

                    if (orderData.Rows.Count == 0)
                    {
                        workOrderBO = null;
                    }
                    else
                    {
                        workOrderBO = new WorkOrderBO();
                        workOrderBO.CreatedDatetime = (orderData.Rows[0]["CreateDateTime"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(orderData.Rows[0]["CreateDateTime"]));
                        workOrderBO.CustomerId = (orderData.Rows[0]["CustomerId"] == DBNull.Value ? 0 : Convert.ToInt32(orderData.Rows[0]["CustomerId"]));
                        workOrderBO.CustomerName = (orderData.Rows[0]["CustomerName"] == DBNull.Value ? string.Empty : orderData.Rows[0]["CustomerName"].ToString ());
                        workOrderBO.OrderDate = (orderData.Rows[0]["OrderDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(orderData.Rows[0]["OrderDate"]));
                        workOrderBO.TechId  = (orderData.Rows[0]["TechId"] == DBNull.Value ? 0 : Convert.ToInt32(orderData.Rows[0]["TechId"]));
                        workOrderBO.TechName = (orderData.Rows[0]["TechName"] == DBNull.Value ? string.Empty : orderData.Rows[0]["TechName"].ToString ());
                        workOrderBO.TotalCharge = (orderData.Rows[0]["TotalCharge"] == DBNull.Value ? 0 : Convert.ToDecimal(orderData.Rows[0]["TotalCharge"]));
                        workOrderBO.TotalUsedTime = (orderData.Rows[0]["TotalUsedTime"] == DBNull.Value ? 0 : Convert.ToDecimal(orderData.Rows[0]["TotalUsedTime"]));
                        workOrderBO.WorkOrderId = (orderData.Rows[0]["WorkOrderId"] == DBNull.Value ? 0 : Convert.ToInt64(orderData.Rows[0]["WorkOrderId"]));
                    }

                    // Work Order Line
                    sqlCmd.CommandText = orderLineQuery;
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.Add("@workOrderId", SqlDbType.BigInt).Value = workOrderId;
                    sqlDataAdapter = new SqlDataAdapter(sqlCmd);
                    sqlDataAdapter.Fill(linedata);
                    orderLineData = linedata;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Int64 getFirstWorkOrder()
        {
            Int64 workOrderId = 0;

            try
            {
                DataTable dt = new DataTable();
                string totalRecordQuery = "SELECT ISNULL(MIN(WorkOrderId),0) FROM WorkOrder";
                using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnector.ConnectionString))
                {
                    sqlConnection.Open();
                    // Total Record Query
                    SqlCommand sqlCmd = new SqlCommand(totalRecordQuery, sqlConnection);
                    sqlCmd.Parameters.Clear();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCmd);
                    sqlDataAdapter.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        workOrderId = 0;
                    }
                    else
                    {
                        workOrderId = (dt.Rows[0][0] == DBNull.Value ? 0 : Convert.ToInt64(dt.Rows[0][0]));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return workOrderId; 
        }

        public static Int64 getLastWorkOrder()
        {
            Int64 workOrderId = 0;

            try
            {
                DataTable dt = new DataTable();
                string totalRecordQuery = "SELECT ISNULL(MAX(WorkOrderId),0) FROM WorkOrder";
                using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnector.ConnectionString))
                {
                    sqlConnection.Open();
                    // Total Record Query
                    SqlCommand sqlCmd = new SqlCommand(totalRecordQuery, sqlConnection);
                    sqlCmd.Parameters.Clear();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCmd);
                    sqlDataAdapter.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        workOrderId = 0;
                    }
                    else
                    {
                        workOrderId = (dt.Rows[0][0] == DBNull.Value ? 0 : Convert.ToInt64(dt.Rows[0][0]));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return workOrderId;
        }

        public static Int64 getPreviousWorkOrder(Int64 orderId)
        {
            Int64 workOrderId = 0;

            try
            {
                DataTable dt = new DataTable();
                string totalRecordQuery = "SELECT TOP 1 WorkOrderId FROM WorkOrder WHERE WorkOrderId < @workOrderId ORDER BY WorkOrderId DESC";
                using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnector.ConnectionString))
                {
                    sqlConnection.Open();
                    // Total Record Query
                    SqlCommand sqlCmd = new SqlCommand(totalRecordQuery, sqlConnection);
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.Add("@workOrderId",SqlDbType.BigInt).Value = orderId;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCmd);
                    sqlDataAdapter.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        workOrderId = 0;
                    }
                    else
                    {
                        workOrderId = (dt.Rows[0][0] == DBNull.Value ? 0 : Convert.ToInt64(dt.Rows[0][0]));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return workOrderId;
        }

        public static Int64 getNextWorkOrder(Int64 orderId)
        {
            Int64 workOrderId = 0;

            try
            {
                DataTable dt = new DataTable();
                string totalRecordQuery = "SELECT TOP 1 WorkOrderId FROM WorkOrder WHERE WorkOrderId > @workOrderId ORDER BY WorkOrderId ASC";
                using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnector.ConnectionString))
                {
                    sqlConnection.Open();
                    // Total Record Query
                    SqlCommand sqlCmd = new SqlCommand(totalRecordQuery, sqlConnection);
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.Add("@workOrderId", SqlDbType.BigInt).Value = orderId;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCmd);
                    sqlDataAdapter.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        workOrderId = 0;
                    }
                    else
                    {
                        workOrderId = (dt.Rows[0][0] == DBNull.Value ? 0 : Convert.ToInt64(dt.Rows[0][0]));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return workOrderId;
        }

        public static string GetToday()
        {
            string  today = "19000000";

            try
            {
                DataTable dt = new DataTable();
                string totalRecordQuery = "SELECT GETDATE()";
                using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnector.ConnectionString))
                {
                    sqlConnection.Open();
                    // Total Record Query
                    SqlCommand sqlCmd = new SqlCommand(totalRecordQuery, sqlConnection);
                    sqlCmd.Parameters.Clear();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCmd);
                    sqlDataAdapter.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        today = "19000000";
                    }
                    else
                    {
                        DateTime date = (dt.Rows[0][0] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[0][0]));
                        if (date == DateTime.MinValue)
                        {
                            today = "19000000";
                        }
                        else
                        {
                            today = date.Year.ToString()
                                   + (date.Month < 10 ? "0" + date.Month.ToString() : date.Month.ToString())
                                   + (date.Day < 10 ? "0" + date.Day.ToString() : date.Day.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return today;
        }

    }
}