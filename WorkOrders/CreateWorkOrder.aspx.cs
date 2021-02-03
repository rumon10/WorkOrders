using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkOrders.BAL;
using System.Data;
using WorkOrders.BO;
using System.IO;
using System.Runtime.InteropServices;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Text.RegularExpressions;

namespace WorkOrders
{
    public partial class CreateWorkOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.existLogin() == false)
            {
                Response.Redirect("WorkUserLogin.aspx");
            }

            if (!IsPostBack)
            {
                ViewState["CurWorkOrderId"] = null;
                this.loadAllCustomers();
                this.loadAllTechs();
                this.setInitWorkOrderInformation();
            }
        }

        private void setInitWorkOrderInformation()
        {
            try
            {
                try
                {
                    Int64 workOrderId = WorkOrderBAL.getLastWorkOrder();

                    if (workOrderId == 0)
                    {
                        this.createNewWorkOrder();
                        this.setBindingNavigatorCountrol();
                    }
                    else
                    {
                        txtWorkOrder.Text = workOrderId.ToString();
                        this.searchWorkOrderInformation();
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private bool existLogin()
        {
            try
            {
                bool IsLogin = (Session["Islogin"] == null ? false : Convert.ToBoolean(Session["Islogin"]));
                int userId = (Session["UserId"] == null ? 0 : Convert.ToInt16(Session["UserId"].ToString()));
                string username = (Session["UserName"] == null ? "" : Session["UserName"].ToString());

                if (username == "" || userId == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

            return false;
        }

        private void loadAllCustomers()
        {
            try
            {
                CustomerBAL customerBAL = new CustomerBAL ();
                DataTable dtCustomers = customerBAL.GetCustomerList ();
                ddlCustomerList .DataSource = dtCustomers ;
                ddlCustomerList.DataTextField ="CustomerName";
                ddlCustomerList .DataValueField ="CustomerId";
                ddlCustomerList .DataBind ();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void createNewWorkOrder()
        {
            try
            {
                txtWorkOrder.ReadOnly = true;
                this.GetNextWorkerId();
                txtTotalCharge.Text = "0.00";
                this.initalizeGrid();
                lblMessage.Text = "";
                lblTotalUsedTime.Text = "Total Used Time : 0.00";
                //Reset Control
                btnCreateInvoice.Text = "Create Invoice Order";
                btnCreateInvoice.CommandArgument = "C";
                btnCreateInvoice.CssClass = "bottomButton";
                btnSendInvoice.Enabled = false;
                btnSendInvoice.CssClass = "bottomButtonDisable";
                imgbtnSearch.CommandArgument = "e";
                imgbtnSearch.ImageUrl = "~/Image/edit.png";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void setBindingNavigatorCountrol()
        {
            try
            {
                Int64 workOrderId;
                if (string.IsNullOrEmpty(txtWorkOrder.Text) == true 
                    || Int64.TryParse(txtWorkOrder.Text, out workOrderId) == false)
                {
                    lblMessage.Text = "Invalid Work Order No#. Please enter the valid Work Order No.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Get Total & Current Record No
                workOrderId = Convert.ToInt64(txtWorkOrder .Text);
                int totalRecord = 0;
                int currentRecord = 0;
                WorkOrderBAL orderBAL = new WorkOrderBAL();
                orderBAL.getNavigatorCount(workOrderId, out totalRecord, out currentRecord);

                if (totalRecord <= 1)
                {
                    btnFirst.Enabled = false;
                    btnFirst.CssClass = "bindingNavigatorbtnDisable";
                    btnLast.Enabled = false;
                    btnLast.CssClass = "bindingNavigatorbtnDisable";
                    btnNext.Enabled = false;
                    btnNext.CssClass = "bindingNavigatorbtnDisable";
                    btnPrevious.Enabled = false;
                    btnPrevious.CssClass = "bindingNavigatorbtnDisable";
                }
                else if (currentRecord == totalRecord)
                {
                    btnFirst.Enabled = true;
                    btnFirst.CssClass = "bindingNavigatorbtn";
                    btnLast.Enabled = false;
                    btnLast.CssClass = "bindingNavigatorbtnDisable";
                    btnNext.Enabled = false;
                    btnNext.CssClass = "bindingNavigatorbtnDisable";
                    btnPrevious.Enabled = true;
                    btnPrevious.CssClass = "bindingNavigatorbtn";
                }
                else if (currentRecord == 1)
                {
                    btnFirst.Enabled = false;
                    btnFirst.CssClass = "bindingNavigatorbtnDisable";
                    btnLast.Enabled = true;
                    btnLast.CssClass = "bindingNavigatorbtn";
                    btnNext.Enabled = true;
                    btnNext.CssClass = "bindingNavigatorbtn";
                    btnPrevious.Enabled = false;
                    btnPrevious.CssClass = "bindingNavigatorbtnDisable";
                }
                else
                {
                    btnFirst.Enabled = true;
                    btnFirst.CssClass = "bindingNavigatorbtn";
                    btnLast.Enabled = true;
                    btnLast.CssClass = "bindingNavigatorbtn";
                    btnNext.Enabled = true;
                    btnNext.CssClass = "bindingNavigatorbtn";
                    btnPrevious.Enabled = true;
                    btnPrevious.CssClass = "bindingNavigatorbtn";
                }
                ViewState["CurWorkOrderId"] = txtWorkOrder.Text;
                lblCount.Text = currentRecord.ToString ()+" of " + totalRecord.ToString ();

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void GetNextWorkerId()
        {
            try
            {
                txtWorkOrder.Text =WorkOrderBAL.GetToday ()  + NumberSquenceBAL.GetNextCode("WK").ToString ();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void loadAllTechs()
        {
            try
            {
                TechBAL techBAL = new TechBAL();
                DataTable dtTechList = techBAL.GetTechList();
                ddlTechList.DataSource = dtTechList;
                ddlTechList.DataTextField = "TechName";
                ddlTechList.DataValueField = "TechId";
                ddlTechList.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void initalizeGrid()
        {
            try
            {
                DataTable dtWorkType = new DataTable();
                dtWorkType.Columns.Add(new DataColumn ("WorkType",typeof(string)));
                dtWorkType.Columns.Add(new DataColumn("Description", typeof(string)));
                dtWorkType.Columns.Add(new DataColumn("UsedTime", typeof(decimal)));

                DataRow dr = dtWorkType.NewRow();
                dr["WorkType"] = "Desktop";
                dr["Description"] = string.Empty;
                dr["UsedTime"] = 0.00;
                dtWorkType.Rows.Add(dr);
                gvWorkType.DataSource = dtWorkType;
                gvWorkType.DataBind();
                ViewState["WorkTypeLine"] = dtWorkType;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void addNewWorkType(object sender, EventArgs e)
        {
            try
            {
                string workType = ((DropDownList)gvWorkType.FooterRow.FindControl("dllInputWorkType")).SelectedValue;
                string description = ((TextBox)gvWorkType.FooterRow.FindControl("txtInputDescription")).Text;
                string  usedTime = ((TextBox)gvWorkType.FooterRow.FindControl("txtInputUsedTime")).Text;
                decimal time;
                if (string.IsNullOrEmpty(workType) == true)
                {
                    lblMessage.Text = "Work Type Required. Please select a work type from list.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else if (string.IsNullOrEmpty(description) == true)
                {
                    lblMessage.Text = "Work type description Required. Please enter the work type description.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else if (string.IsNullOrEmpty(usedTime) == true
                    || decimal.TryParse(usedTime, out time) == false)
                {
                    lblMessage.Text = "Invalid Used Time. Please enter a valid Used time.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else if (this.validateUsedTime() == false)
                {
                    return;
                }
                else
                {
                    DataTable dtWorkType = (DataTable)ViewState["WorkTypeLine"];
                    if (dtWorkType != null)
                    {
                        if (dtWorkType.Rows.Count == 1)
                        {
                            string firstWorkType = Convert.ToString(dtWorkType.Rows[0]["WorkType"]);
                            string firstDescription = Convert.ToString(dtWorkType.Rows[0]["Description"]);
                            string firstUsedTime = Convert.ToString(dtWorkType.Rows[0]["UsedTime"]);

                            if (firstWorkType == "Desktop" && string.IsNullOrEmpty(firstDescription) == true)
                            {
                                dtWorkType.Rows.RemoveAt(0);
                            }
                        }

                        DataRow dr = dtWorkType.NewRow();
                        dr["WorkType"] = workType;
                        dr["Description"] = description;
                        dr["UsedTime"] = decimal.Parse(usedTime);

                        dtWorkType.Rows.Add(dr);
                        gvWorkType.DataSource = dtWorkType;
                        gvWorkType.DataBind();
                        ViewState["WorkTypeLine"] = dtWorkType;
                        gvWorkType.EditIndex = -1;
                        this.loadAllWorkType();
                        lblMessage.Text = "";
                        int hours = 0;
                        int mintues = 0;
                        this.GetTotalTime(out hours , out mintues);
                        lblTotalUsedTime.Text = "Total Used Time : " + hours.ToString () + " hours " + mintues.ToString () + " minutes.";
                    }
                    else
                    {
                        this.initalizeGrid();
                        lblMessage.Text = "Initalize Error. Please try again.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        public void GetTotalTime(out int hours, out int mintues)
        {
            decimal totalTime = 0;
            hours = 0;
            mintues = 0;

            try
            {
                DataTable dtWorkType = (DataTable)ViewState["WorkTypeLine"];
                if (dtWorkType != null && dtWorkType.Rows.Count > 0)
                {
                    for (int i = 0; i < dtWorkType.Rows.Count; ++i)
                    {
                        decimal usedTime = ((dtWorkType.Rows[i]["UsedTime"] == DBNull.Value || dtWorkType.Rows[i]["UsedTime"] == string.Empty) ? 0 : decimal.Parse(dtWorkType.Rows[i]["UsedTime"].ToString()));
                        totalTime = totalTime + usedTime;
                    }
                }

                this.GetTotalTimePart(totalTime ,out hours, out mintues );
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        public void GetTotalTimePart(decimal totalusedTime,out int hours, out int  mintues)
        {
            hours = 0;
            mintues = 0;
    
            try
            {
                decimal totalhours = Math.Truncate(totalusedTime);
                decimal  totalMintues = totalusedTime - totalhours;
                totalMintues = totalMintues * 100;
                hours = (int) totalhours;
                mintues = (int) totalMintues;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void gvWorkType_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvWorkType.EditIndex = e.NewEditIndex;
                this.loadAllWorkType();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void loadAllWorkType()
        {
            try
            {
                DataTable dtWorkType = (DataTable)ViewState["WorkTypeLine"];
                if (dtWorkType != null)
                {
                    gvWorkType.DataSource = dtWorkType;
                    gvWorkType.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void gvWorkType_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gvWorkType.EditIndex = -1;
                this.loadAllWorkType();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void gvWorkType_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                string workType = ((DropDownList)gvWorkType.Rows[e.RowIndex].FindControl("ddlWorkType")).SelectedValue;
                string description = ((TextBox)gvWorkType.Rows[e.RowIndex].FindControl("txtDescription")).Text;
                string usedTime = ((TextBox)gvWorkType.Rows[e.RowIndex].FindControl("txtUsedTime")).Text;
                decimal time;
                if (string.IsNullOrEmpty(workType) == true)
                {
                    lblMessage.Text = "Work Type Required. Please select a work type from list.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else if (string.IsNullOrEmpty(description) == true)
                {
                    lblMessage.Text = "Work type description Required. Please enter the work type description.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else if (string.IsNullOrEmpty(usedTime) == true
                    || decimal.TryParse(usedTime, out time) == false)
                {
                    lblMessage.Text = "Invalid Used Time. Please enter a valid Used time.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else if (this.validateUsedTime(usedTime) == false)
                {
                    return;
                }
                else
                {
                    DataTable dtWorkType = (DataTable)ViewState["WorkTypeLine"];
                    if (dtWorkType != null)
                    {
                        if (dtWorkType.Rows.Count == 1)
                        {
                            string firstWorkType = Convert.ToString(dtWorkType.Rows[0]["WorkType"]);
                            string firstDescription = Convert.ToString(dtWorkType.Rows[0]["Description"]);
                            string firstUsedTime = Convert.ToString(dtWorkType.Rows[0]["UsedTime"]);

                            if (firstWorkType == "Desktop" && string.IsNullOrEmpty(firstDescription) == true)
                            {
                                dtWorkType.Rows.RemoveAt(0);
                            }
                        }

                        DataRow dr = dtWorkType.NewRow();
                        dr["WorkType"] = workType;
                        dr["Description"] = description;
                        dr["UsedTime"] = decimal.Parse(usedTime);
                        if (dtWorkType.Rows.Count > 0)
                        {
                            dtWorkType.Rows.RemoveAt(e.RowIndex);
                        }
                        dtWorkType.Rows.Add(dr);
                        gvWorkType.DataSource = dtWorkType;
                        gvWorkType.DataBind();
                        ViewState["WorkTypeLine"] = dtWorkType;
                        gvWorkType.EditIndex = -1;
                        this.loadAllWorkType();
                        lblMessage.Text = "";
                        int hours = 0;
                        int mintues = 0;
                        this.GetTotalTime(out hours, out mintues);
                        lblTotalUsedTime.Text = "Total Used Time : " + hours.ToString() + " hours " + mintues.ToString() + " minutes.";
                    }
                    else
                    {
                        this.initalizeGrid();
                        lblMessage.Text = "Initalize Error. Please try again.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void DeleteWorkType(object sender, EventArgs e)
        {
            try
            {
                LinkButton linkbtn = (LinkButton)sender;
                string rowNumber = linkbtn.CommandArgument;
                if (string.IsNullOrEmpty(rowNumber) == true)
                {
                    lblMessage.Text = "No Record is selected.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    int Index = Int32.Parse(rowNumber);
                    DataTable dtWorkType = (DataTable)ViewState["WorkTypeLine"];
                    if (dtWorkType != null && dtWorkType.Rows.Count > 0)
                    {
                        if (Index >= dtWorkType.Rows.Count)
                        {
                            lblMessage.Text = "Selected work type service record not exists.";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            dtWorkType.Rows.RemoveAt(Index);
                            if (dtWorkType.Rows.Count == 0)
                            {
                                DataRow dr = dtWorkType.NewRow();
                                dr["WorkType"] = "Desktop";
                                dr["Description"] = string.Empty;
                                dr["UsedTime"] = 0.00;
                                dtWorkType.Rows.Add(dr);
                                gvWorkType.DataSource = dtWorkType;
                                gvWorkType.DataBind();
                                ViewState["WorkTypeLine"] = dtWorkType;
                                lblMessage.Text = "";
                                //display total used time
                                int hours = 0;
                                int  mintues = 0;
                                this.GetTotalTime(out hours, out mintues);
                                lblTotalUsedTime.Text = "Total Used Time : " + hours.ToString() + " hours " + mintues.ToString() + " minutes.";
                            }
                            else
                            {
                                gvWorkType.DataSource = dtWorkType;
                                gvWorkType.DataBind();
                                ViewState["WorkTypeLine"] = dtWorkType;
                                gvWorkType.EditIndex = -1;
                                this.loadAllWorkType();
                                lblMessage.Text = "";
                                //display total used time
                                int  hours = 0;
                                int  mintues = 0;
                                this.GetTotalTime(out hours, out mintues);
                                lblTotalUsedTime.Text = "Total Used Time : " + hours.ToString() + " hours " + mintues.ToString() + " minutes.";
                            }
                        }
                    }
                    else
                    {
                        lblMessage.Text = "No work type service records exists.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
                
                }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void setControlState()
        {
            try
            {
                if (string.IsNullOrEmpty(txtWorkOrder.Text) == false)
                {
                    if (WorkOrderBAL.WorkOrderIdExist(txtWorkOrder.Text.Trim()) == true)
                    {
                        btnCreateInvoice.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtWorkOrder.ReadOnly == true
                    && (imgbtnSearch.CommandArgument != null && imgbtnSearch.CommandArgument == "e"))
                {
                    this.setWorkIdEditable();
                }
                else if (txtWorkOrder.ReadOnly == false
                    && (imgbtnSearch.CommandArgument != null && imgbtnSearch.CommandArgument == "s"))
                {
                    this.searchWorkOrderInformation();
                }
                else
                {
                    lblMessage.Text = "No Searching operation set. Please try again";
                    lblMessage.ForeColor = System.Drawing.Color.DarkRed;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "cursorDefault()", true);
            }
        }
        private void setWorkIdEditable()
        {
            try
            {
                txtWorkOrder.ReadOnly = false;
                imgbtnSearch.CommandArgument = "s";
                txtWorkOrder.Text = "";
                txtDate.Text = "";
                this.initalizeGrid();
                lblMessage.Text = "";
                //display total used time
                int hours = 0;
                int mintues = 0;
                this.GetTotalTime(out hours, out mintues);
                lblTotalUsedTime.Text = "Total Used Time : " + hours.ToString() + " hours " + mintues.ToString() + " minutes.";
                imgbtnSearch.ImageUrl = "~/Image/find.png";
                btnCreateInvoice.Enabled = false;
                btnCreateInvoice.CssClass = "bottomButtonDisable";
                //btnDownloadInvoiceReport.Enabled = false;
                //btnDownloadInvoiceReport.CssClass = "bottomButtonDisable";
                btnSendInvoice.Enabled = false;
                btnSendInvoice.CssClass = "bottomButtonDisable";
                btnCreateNewOrder.Enabled = true;
                btnCreateNewOrder.CssClass = "bottomButton";
                txtWorkOrder.Focus();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void searchWorkOrderInformation()
        {
            try
            {
                Int64 workOrderId;

                if (string.IsNullOrEmpty(txtWorkOrder.Text) == true)
                {
                    lblMessage.Text = "Work Order No. Required to search work order!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else if (Int64.TryParse(txtWorkOrder.Text, out workOrderId) == false)
                {
                    lblMessage.Text = "Invalid Work Order Id! Please enter the valid Work Order Id.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    WorkOrderBAL orderBAL = new WorkOrderBAL();
                    WorkOrderBO orderBO = new WorkOrderBO();
                    DataTable OrderLineData = new DataTable();
                    orderBAL.getWorkOrderInformation(workOrderId, out orderBO, out OrderLineData);
                    if (orderBO == null)
                    {
                        lblMessage.Text = "Work Order Id " + txtWorkOrder.Text + " Not found. Please try to search right work Order.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    if (OrderLineData == null || OrderLineData.Rows.Count == 0)
                    {
                        lblMessage.Text = "No Service line exist under this Work Order Id " + txtWorkOrder.Text + ". Please try to search right work Order.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }

                    //SET Work Order Information
                    txtWorkOrder.Text = orderBO.WorkOrderId.ToString();
                    txtWorkOrder.ReadOnly = true;
                    ddlCustomerList.SelectedValue = orderBO.CustomerId.ToString();
                    ddlCustomerList.SelectedItem.Text = orderBO.CustomerName;
                    ddlTechList.SelectedValue = orderBO.TechId.ToString();
                    ddlTechList.SelectedItem.Text = orderBO.TechName;
                    txtDate.Text = orderBO.OrderDate.ToString("MM/dd/yyyy");
                    txtTotalCharge.Text = orderBO.TotalCharge.ToString();
                    lblTotalUsedTime.Text = "Total Used Time : " + orderBO.TotalUsedTime.ToString();

                    // Set Search Control
                    imgbtnSearch.CommandArgument = "e";
                    imgbtnSearch.ImageUrl = "~/Image/edit.png";
                    lblMessage.Text = "";

                    //Bind Work order Line Information
                    DataTable dtWorkType = new DataTable();
                    dtWorkType.Columns.Add(new DataColumn("WorkType", typeof(string)));
                    dtWorkType.Columns.Add(new DataColumn("Description", typeof(string)));
                    dtWorkType.Columns.Add(new DataColumn("UsedTime", typeof(decimal)));

                    if (OrderLineData.Rows.Count > 0)
                    {
                        for (int i = 0; i < OrderLineData.Rows.Count; ++i)
                        {
                            DataRow dr = dtWorkType.NewRow();
                            dr["WorkType"] = (OrderLineData.Rows[i]["WorkType"] == DBNull.Value ? "" : Convert.ToString(OrderLineData.Rows[i]["WorkType"]));
                            dr["Description"] = (OrderLineData.Rows[i]["Description"] == DBNull.Value ? "" : Convert.ToString(OrderLineData.Rows[i]["Description"]));
                            dr["UsedTime"] = (OrderLineData.Rows[i]["UsedTime"] == DBNull.Value ? 0 : Convert.ToDecimal(OrderLineData.Rows[i]["UsedTime"]));
                            dtWorkType.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        DataRow dr = dtWorkType.NewRow();
                        dr["WorkType"] = string.Empty;
                        dr["Description"] = string.Empty;
                        dr["UsedTime"] = 0;
                        dtWorkType.Rows.Add(dr);
                    }

                    gvWorkType.DataSource = dtWorkType;
                    gvWorkType.DataBind();
                    ViewState["WorkTypeLine"] = dtWorkType;
                    ViewState["CurWorkOrderId"] = txtWorkOrder.Text;
                    lblMessage.Text = "";
                    //display total used time
                    int  hours = 0;
                    int mintues = 0;
                    this.GetTotalTime(out hours, out mintues);
                    lblTotalUsedTime.Text = "Total Used Time : " + hours.ToString() + " hours " + mintues.ToString() + " minutes.";

                    //Set Control
                    btnCreateInvoice.Enabled = true;
                    btnCreateInvoice.Text = "Save Invoice Order";
                    btnCreateInvoice.CommandArgument = "S";
                    btnCreateInvoice.CssClass = "bottomButton";
                    //btnDownloadInvoiceReport.Enabled = true;
                    //btnDownloadInvoiceReport.CssClass = "bottomButton";
                    btnSendInvoice.Enabled = true;
                    btnSendInvoice.CssClass = "bottomButton";
                    btnCreateNewOrder.Enabled = true;
                    btnCreateNewOrder.CssClass = "bottomButton";

                    //Set Binding Control
                    this.setBindingNavigatorCountrol();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnCreateInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime orderDate;
                decimal totalcharge;

                // Input validation & data checking
                if (ddlCustomerList.SelectedValue == null 
                    || string.IsNullOrEmpty(ddlCustomerList.Text) == true)
                {
                    lblMessage.Text = "Customer Required. Please select the customer from the drop-down list.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (string.IsNullOrEmpty (txtDate.Text) == true 
                    || DateTime.TryParse(txtDate.Text, out orderDate ) == false)
                {
                    lblMessage.Text = "Invalid Order Date. Please put valid order date.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (ddlTechList.SelectedValue == null
                   || string.IsNullOrEmpty(ddlTechList.Text) == true)
                {
                    lblMessage.Text = "Tech Required. Please select the Tech from the drop-down list.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (string.IsNullOrEmpty(txtTotalCharge.Text) == false
                    && decimal.TryParse(txtTotalCharge.Text, out totalcharge) == false)
                {
                    lblMessage.Text = "Invalid Total charge. Please enter the valid total charge.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(txtTotalCharge.Text) == true)
                    {
                        totalcharge = 0;
                    }
                    else
                    {
                        totalcharge = decimal.Parse(txtTotalCharge.Text);
                    }
                }

                if (string.IsNullOrEmpty(txtWorkOrder.Text) == true)
                {
                    lblMessage.Text = "Work Order No# Required.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                
                //Get Command
                string commandArg = btnCreateInvoice.CommandArgument.ToString();
                //Get Login User name
                string username = (Session["UserName"] == null ? "" : Session["UserName"].ToString());
                //Work Order header
                WorkOrderBO orderBO = new WorkOrderBO();
                orderBO.CustomerId = Convert.ToInt32(ddlCustomerList.SelectedValue.ToString ());
                orderBO.TechId = Convert.ToInt32(ddlTechList.SelectedValue.ToString());
                orderBO.OrderDate = Convert.ToDateTime(txtDate.Text);
                orderBO.TotalCharge = totalcharge;
                orderBO.WorkOrderId = Convert.ToInt64(txtWorkOrder.Text);
                orderBO.LastEdit = username;

                //Work Header line
                List<WorkOrderLineBO> orderLineListBO = new List<WorkOrderLineBO>();
                WorkOrderLineBO orderLineBO = null;
                DataTable dtWorkType = (DataTable)ViewState["WorkTypeLine"];
                decimal usedTime = 0;

                if(gvWorkType.Rows.Count == 0 
                    || (dtWorkType == null || dtWorkType.Rows.Count == 0))
                {
                    lblMessage.Text = "No Service added under this work order. Please add at least one service.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                for (int i = 0; i < dtWorkType.Rows.Count; ++i)
                {
                    orderLineBO = new WorkOrderLineBO();
                    orderLineBO.Description = ((dtWorkType.Rows[i]["Description"] == null || dtWorkType.Rows[i]["Description"] == DBNull.Value) ? string.Empty : Convert.ToString(dtWorkType.Rows[i]["Description"]));
                    orderLineBO.UsedTime = ((dtWorkType.Rows[i]["UsedTime"] == null || dtWorkType.Rows[i]["UsedTime"] == DBNull.Value
                                            || (string.IsNullOrEmpty(dtWorkType.Rows[i]["UsedTime"].ToString()) == false && decimal.TryParse(dtWorkType.Rows[i]["UsedTime"].ToString(), out usedTime) == false)) ? 0 : Convert.ToDecimal(dtWorkType.Rows[i]["UsedTime"]));
                    orderLineBO.WorkOrderId = orderBO.WorkOrderId;
                    orderLineBO.WorkType = ((dtWorkType.Rows[i]["WorkType"] == null || dtWorkType.Rows[i]["WorkType"] == DBNull.Value) ? string.Empty : Convert.ToString(dtWorkType.Rows[i]["WorkType"]));
                    
                    if (orderLineBO.WorkType != string.Empty && orderLineBO.Description != string.Empty)
                    {
                        orderLineListBO.Add(orderLineBO);
                    }
                }

                if (orderLineListBO == null || orderLineListBO.Count == 0)
                {
                    lblMessage.Text = "No Service added under this work order. Please add at least one service.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (commandArg == "C")
                {
                    if (WorkOrderBAL.WorkOrderIdExist(txtWorkOrder.Text) == true)
                    {
                        lblMessage.Text = "Work Order No# " + txtWorkOrder.Text + " already exist. Please create new work order.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    else
                    {
                        WorkOrderBAL orderBal = new WorkOrderBAL();
                        bool IsSuccess = orderBal.crateWorokOrder(orderBO, orderLineListBO);
                        if (IsSuccess == true)
                        {
                            btnCreateInvoice.CommandArgument = "S";
                            btnCreateInvoice.Text = "Save Invoice Order";
                            //btnDownloadInvoiceReport.Enabled = true;
                            //btnDownloadInvoiceReport.CssClass = "bottomButton";
                            btnSendInvoice.Enabled = true;
                            btnSendInvoice.CssClass = "bottomButton";
                            //Load Update Work Order Information
                            this.setInitWorkOrderInformation();
                            lblMessage.Text = "Work Order No# " + txtWorkOrder.Text + " Invoiced Sucessfully.";
                            lblMessage.ForeColor = System.Drawing.Color.DarkGreen;
                        }
                        else
                        {
                            lblMessage.Text = "Work Order No# " + txtWorkOrder.Text + " Invoiced Failed. Please try again.";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
                else if (commandArg == "S")
                {
                    if (WorkOrderBAL.WorkOrderIdExist(txtWorkOrder.Text) == false)
                    {
                        lblMessage.Text = "Work Order No# " + txtWorkOrder.Text + " does not exist into the database.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    else
                    {
                        WorkOrderBAL orderBal = new WorkOrderBAL();
                        bool IsSuccess = orderBal.saveWorokOrder(orderBO, orderLineListBO);
                        if (IsSuccess == true)
                        {
                            lblMessage.Text = "Work Order No# " + txtWorkOrder.Text + "'s Invoice Information Saved Sucessfully.";
                            lblMessage.ForeColor = System.Drawing.Color.DarkGreen;
                            btnCreateInvoice.CommandArgument = "S";
                            btnCreateInvoice.Text = "Save Invoice Order";
                            //btnDownloadInvoiceReport.Enabled = true;
                            //btnDownloadInvoiceReport.CssClass = "bottomButton";
                            btnSendInvoice.Enabled = true;
                            btnSendInvoice.CssClass = "bottomButton";
                        }
                        else
                        {
                            lblMessage.Text = "Work Order No# " + txtWorkOrder.Text + " Invoice Saved Failed. Please try again.";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "cursorDefault()", true);
            }
        }

        protected void btnCreateNewOrder_Click(object sender, EventArgs e)
        {
            try
            {
                this.createNewWorkOrder();
                this.setBindingNavigatorCountrol();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnFirst_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 workOrderId = WorkOrderBAL.getFirstWorkOrder();

                if (workOrderId == 0)
                {
                    lblMessage.Text = "No First Work Id Exists.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    txtWorkOrder.Text = workOrderId.ToString ();
                    this.searchWorkOrderInformation();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 workOrderId = WorkOrderBAL.getLastWorkOrder();

                if (workOrderId == 0)
                {
                    lblMessage.Text = "No Last Work Id Exists.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    txtWorkOrder.Text = workOrderId.ToString();
                    this.searchWorkOrderInformation();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnPrevious.Enabled == true && ViewState["CurWorkOrderId"] !=null)
                {
                    string OrderId = Convert.ToString(ViewState["CurWorkOrderId"]);
                    Int64 workOrderId;
                    if (Int64.TryParse(OrderId, out workOrderId) == false)
                    {
                        lblMessage.Text = "Invalid Work Order Id #" + OrderId + ". Please try again.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        workOrderId = Convert.ToInt64(OrderId);
                        workOrderId = WorkOrderBAL.getPreviousWorkOrder(workOrderId);
                        txtWorkOrder.Text = workOrderId.ToString();
                        this.searchWorkOrderInformation();
                    }
                }
                else
                {
                    lblMessage.Text = "No Previous Work Order Exists.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnNext.Enabled == true && ViewState["CurWorkOrderId"] != null)
                {
                    string OrderId = Convert.ToString(ViewState["CurWorkOrderId"]);
                    Int64 workOrderId;
                    if (Int64.TryParse(OrderId, out workOrderId) == false)
                    {
                        lblMessage.Text = "Invalid Work Order Id #" + OrderId + ". Please try again.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        workOrderId = Convert.ToInt64(OrderId);
                        workOrderId = WorkOrderBAL.getNextWorkOrder(workOrderId);
                        txtWorkOrder.Text = workOrderId.ToString();
                        this.searchWorkOrderInformation();
                    }
                }
                else
                {
                    lblMessage.Text = "No Next Work Order Exists.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnDownloadInvoiceReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtWorkOrder.Text) == true)
                {
                    lblMessage.Text = "Work Order not exist for downloading.";
                    lblMessage.ForeColor = System.Drawing.Color.DarkRed;
                    return;
                }

                Int64 workOrderId;
                if (Int64.TryParse(txtWorkOrder.Text, out workOrderId) == false)
                {
                    lblMessage.Text = "Invalid Work Order Id. Please enter the valid work order Id.";
                    lblMessage.ForeColor = System.Drawing.Color.DarkRed;
                    return;
                }

                //if (WorkOrderBAL.WorkOrderIdExist(txtWorkOrder.Text) == false)
                //{
                //    lblMessage.Text = "Work Order Id# " + txtWorkOrder.Text + " not exist.";
                //    lblMessage.ForeColor = System.Drawing.Color.DarkRed;
                //    return;
                //}

                // Work Order Information
                //WorkOrderBAL orderBAL = new WorkOrderBAL();
                WorkOrderBO orderBO = null;
                DataTable OrderLineData = new DataTable();
                //orderBAL.getWorkOrderInformation(workOrderId, out orderBO, out OrderLineData);
                if (orderBO == null)
                {
                    //lblMessage.Text = "Work Order Id " + txtWorkOrder.Text + " Not found. Please try to search right work Order.";
                    //lblMessage.ForeColor = System.Drawing.Color.Red;
                    //return;
                    orderBO = new WorkOrderBO();
                    orderBO.WorkOrderId = Convert.ToInt64(txtWorkOrder.Text);
                    orderBO.CustomerId = (ddlCustomerList.SelectedValue == null ? 0 : Convert.ToInt32(ddlCustomerList.SelectedValue.ToString()));
                    orderBO.CustomerName = (ddlCustomerList.SelectedItem.Text == null ? string.Empty : ddlCustomerList.SelectedItem.Text);
                    orderBO.TechId = (ddlTechList.SelectedValue == null ? 0 : Convert.ToInt32(ddlTechList.SelectedValue.ToString()));
                    orderBO.TechName = (ddlTechList.SelectedItem.Text == null ? string.Empty : ddlTechList.SelectedItem.Text);
                    orderBO.OrderDate = (string.IsNullOrEmpty(txtDate.Text) == true ? DateTime.MinValue : Convert.ToDateTime(txtDate.Text));
                    orderBO.TotalCharge = (string.IsNullOrEmpty(txtTotalCharge.Text) == true ? 0 : Convert.ToDecimal(txtTotalCharge.Text));
                }
                if (OrderLineData == null || OrderLineData.Rows.Count == 0)
                {
                    //lblMessage.Text = "No Service line exist under this Work Order Id " + txtWorkOrder.Text + ". Please try to search right work Order.";
                    //lblMessage.ForeColor = System.Drawing.Color.Red;
                    //return;
                    if (ViewState["WorkTypeLine"] != null)
                        OrderLineData = (DataTable)ViewState["WorkTypeLine"];
                }

                //Get WorkOrder Path
                string pdfPath = Server.MapPath("Pdf/" + txtWorkOrder.Text + ".pdf");
                if (File.Exists(pdfPath) == true)
                {
                    File.Delete(pdfPath);
                }

                //Create Pdf Document
                Document pdfDoc = new Document(PageSize.B5, 10, 10, 5, 5);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(pdfPath, FileMode.Create));
                pdfDoc.Open();

                //Report Header
                iTextSharp.text.Font font22B = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, iTextSharp.text.Font.BOLD);
                Paragraph p1 = new Paragraph("Work Order Invoice Report", font22B);
                p1.Alignment = Element.ALIGN_CENTER;
                pdfDoc.Add(new Paragraph(p1));

                // Report Logo
                //string companyLogo = "";
                //if (File.Exists(companyLogo) == true)
                //{
                //    iTextSharp.text.Image imghead = iTextSharp.text.Image.GetInstance(Server.MapPath(companyLogo));
                //    imghead.ScaleToFit(100f, 100f);
                //    imghead.SpacingBefore = 5f;
                //    imghead.SpacingAfter = 0f;
                //    imghead.Alignment = Element.ALIGN_LEFT;
                //    pdfDoc.Add(imghead);
                //}

                //Work Order Header Information
                iTextSharp.text.Font font10B = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, iTextSharp.text.Font.BOLD);
                Paragraph p3 = new Paragraph("Work Order Id : " + orderBO.WorkOrderId.ToString() + "\nCustomer Name : " + orderBO.CustomerName + "\nOrder Date : " + orderBO.OrderDate.ToString("MM/dd/yyyy") + "\nTech Name : " + orderBO.TechName + "\n\n\n");
                p3.Alignment = Element.ALIGN_JUSTIFIED;
                pdfDoc.Add(new Paragraph(p3));

                //Creating Table for Work Order Service Line
                PdfPTable pdfTable = new PdfPTable(OrderLineData.Columns.Count);
                pdfTable.DefaultCell.Padding = 3;
                pdfTable.WidthPercentage = 100;
                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfTable.DefaultCell.BorderWidth = 1;

                //Adding Header row
                iTextSharp.text.pdf.BaseFont bfTimes = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, false);
                iTextSharp.text.Font Curier = new iTextSharp.text.Font(bfTimes, 12, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.WHITE);

                foreach (DataColumn column in OrderLineData.Columns)
                {
                    PdfPCell cell;

                    if (column.ColumnName == "WorkType")
                    {
                        cell = new PdfPCell(new Phrase("Work Type\n ", Curier));
                        cell.BackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.SteelBlue);
                        pdfTable.AddCell(cell);
                    }
                    else if (column.ColumnName == "Description")
                    {
                        cell = new PdfPCell(new Phrase("Description\n ", Curier));
                        cell.BackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.SteelBlue);
                        pdfTable.AddCell(cell);
                    }
                    else if (column.ColumnName == "UsedTime")
                    {
                        cell = new PdfPCell(new Phrase("Used Time\n ", Curier));
                        cell.BackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.SteelBlue);
                        pdfTable.AddCell(cell);
                    }
                }

                //Work Service Line Information
                for (int i = 0; i < OrderLineData.Rows.Count; ++i)
                {
                    for (int j = 0; j < OrderLineData.Columns.Count; ++j)
                    {
                        pdfTable.AddCell(OrderLineData.Rows[i][j] == DBNull.Value ? "" : OrderLineData.Rows[i][j].ToString());
                    }
                }

                pdfDoc.Add(pdfTable);

                //Add Total Useage time
                int hours = 0;
                int mintues = 0;
                this.GetTotalTime(out hours, out mintues);
                lblTotalUsedTime.Text = "Total Used Time : " + hours.ToString() + " hours " + mintues.ToString() + " minutes.";
                Paragraph p4 = new Paragraph("\n\nTotal Used Time : " + hours.ToString() + " hours " + mintues.ToString() + " minutes.");
                p4.Alignment = Element.ALIGN_JUSTIFIED;
                pdfDoc.Add(new Paragraph(p4));

                //Add total Cost
                Paragraph p5 = new Paragraph("Total Charge : " + string.Format("{0:C}", orderBO.TotalCharge));
                p4.Alignment = Element.ALIGN_JUSTIFIED;
                pdfDoc.Add(new Paragraph(p5));
                pdfDoc.Close();
                writer.Close();
                Response.Redirect("~/Pdf/" + Path.GetFileName(pdfPath), true);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private bool validateUsedTime()
        {
            try
            {
                string usedTime = ((TextBox)gvWorkType.FooterRow.FindControl("txtInputUsedTime")).Text;
                decimal time;
                if (string.IsNullOrEmpty(usedTime) == true 
                    || decimal.TryParse (usedTime, out time) == false)
                {
                    lblMessage.Text = "Invalid Used Time. Please enter a valid Used time.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return false;
                }

                time = decimal.Parse(usedTime);
                decimal mintues = Math.Abs(time - Math.Truncate(time));
                
                if (mintues > new Decimal (0.59))
                {
                    lblMessage.Text = "Invalid Used Time. Mintues Should be equal or below 59";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

            return true;
        }

        private bool validateUsedTime(string usedTime)
        {
            try
            {
                decimal time;
                if (string.IsNullOrEmpty(usedTime) == true
                    || decimal.TryParse(usedTime, out time) == false)
                {
                    lblMessage.Text = "Invalid Used Time. Please enter a valid Used time.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return false;
                }

                time = decimal.Parse(usedTime);
                decimal mintues = Math.Abs(time - Math.Truncate(time));

                if (mintues > new Decimal(0.59))
                {
                    lblMessage.Text = "Invalid Used Time. Mintues Should be equal or below 59";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

            return true;
        }

        protected void btnSendInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "Invoice Report Sending ...";
                lblMessage.ForeColor = System.Drawing.Color.DarkGreen;

                //System Email Authentication
                string fromAddress = ConfigurationManager.AppSettings["gmailId"];
                string password = ConfigurationManager.AppSettings["gmailPassword"];

                if (this.IsValidEmailId(fromAddress) == false)
                {
                    lblMessage.Text = "Invalid From Address. Please confirgure it correctly.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (string.IsNullOrEmpty(fromAddress) == true || string.IsNullOrEmpty(password) == true)
                {
                    lblMessage.Text = "Not email sending parameter configured properly.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (btnDownloadInvoiceReport.Enabled == false
                    || string.IsNullOrEmpty(txtWorkOrder.Text) == true)
                {
                    lblMessage.Text = "Work Order not exist for downloading.";
                    lblMessage.ForeColor = System.Drawing.Color.DarkRed;
                    return;
                }

                Int64 workOrderId;
                if (Int64.TryParse(txtWorkOrder.Text, out workOrderId) == false)
                {
                    lblMessage.Text = "Invalid Work Order Id. Please enter the valid work order Id.";
                    lblMessage.ForeColor = System.Drawing.Color.DarkRed;
                    return;
                }

                if (WorkOrderBAL.WorkOrderIdExist(txtWorkOrder.Text) == false)
                {
                    lblMessage.Text = "Work Order Id# " + txtWorkOrder.Text + " not exist.";
                    lblMessage.ForeColor = System.Drawing.Color.DarkRed;
                    return;
                }

                // Work Order Information
                WorkOrderBAL orderBAL = new WorkOrderBAL();
                WorkOrderBO orderBO = new WorkOrderBO();
                DataTable OrderLineData = new DataTable();
                orderBAL.getWorkOrderInformation(workOrderId, out orderBO, out OrderLineData);
                if (orderBO == null)
                {
                    lblMessage.Text = "Work Order Id " + txtWorkOrder.Text + " Not found. Please try to search right work Order.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                //Customer Email
                if (ddlCustomerList.SelectedValue == null)
                {
                    lblMessage.Text = "No Customer is selected.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                CustomerBO customerBO = new CustomerBO();
                CustomerBAL customerBAL = new CustomerBAL();
                customerBO = customerBAL.GetCustomerInformation(Convert.ToInt16(ddlCustomerList.SelectedValue));

                if (customerBO == null || customerBO.EmailAddress == "")
                {
                    lblMessage.Text = "Invoice Report sending failed due to no email id exist of the customer.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (this.IsValidEmailId(customerBO.EmailAddress) == false)
                {
                    lblMessage.Text = "Invalid Customer email Address. Please make it correct before sending.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (OrderLineData == null || OrderLineData.Rows.Count == 0)
                {
                    lblMessage.Text = "No Service line exist under this Work Order Id " + txtWorkOrder.Text + ". Please try to search right work Order.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                //Get WorkOrder Path
                string pdfPath = Server.MapPath("Pdf/" + txtWorkOrder.Text + ".pdf");
                if (File.Exists(pdfPath) == true)
                {
                    File.Delete(pdfPath);
                }

                //Create Pdf Document
                Document pdfDoc = new Document(PageSize.B5, 10, 10, 5, 5);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(pdfPath, FileMode.Create));
                pdfDoc.Open();

                //Report Header
                iTextSharp.text.Font font22B = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, iTextSharp.text.Font.BOLD);
                Paragraph p1 = new Paragraph("Work Order Invoice Report", font22B);
                p1.Alignment = Element.ALIGN_CENTER;
                pdfDoc.Add(new Paragraph(p1));

                // Report Logo
                //string companyLogo = "";
                //if (File.Exists(companyLogo) == true)
                //{
                //    iTextSharp.text.Image imghead = iTextSharp.text.Image.GetInstance(Server.MapPath(companyLogo));
                //    imghead.ScaleToFit(100f, 100f);
                //    imghead.SpacingBefore = 5f;
                //    imghead.SpacingAfter = 0f;
                //    imghead.Alignment = Element.ALIGN_LEFT;
                //    pdfDoc.Add(imghead);
                //}

                //Work Order Header Information
                iTextSharp.text.Font font10B = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, iTextSharp.text.Font.BOLD);
                Paragraph p3 = new Paragraph("Work Order Id : " + txtWorkOrder.Text + "\nCustomer Name : " + ddlCustomerList.SelectedItem.Text + "\nOrder Date : " + orderBO.OrderDate.ToString("MM/dd/yyyy") + "\nTech Name : " + ddlTechList.SelectedItem.Text + "\n\n\n");
                p3.Alignment = Element.ALIGN_JUSTIFIED;
                pdfDoc.Add(new Paragraph(p3));

                //Creating Table for Work Order Service Line
                PdfPTable pdfTable = new PdfPTable(OrderLineData.Columns.Count);
                pdfTable.DefaultCell.Padding = 3;
                pdfTable.WidthPercentage = 100;
                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfTable.DefaultCell.BorderWidth = 1;

                //Adding Header row
                iTextSharp.text.pdf.BaseFont bfTimes = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, false);
                iTextSharp.text.Font Curier = new iTextSharp.text.Font(bfTimes, 12, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.WHITE);

                foreach (DataColumn column in OrderLineData.Columns)
                {
                    PdfPCell cell;

                    if (column.ColumnName == "WorkType")
                    {
                        cell = new PdfPCell(new Phrase("Work Type\n ", Curier));
                        cell.BackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.SteelBlue);
                        pdfTable.AddCell(cell);
                    }
                    else if (column.ColumnName == "Description")
                    {
                        cell = new PdfPCell(new Phrase("Description\n ", Curier));
                        cell.BackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.SteelBlue);
                        pdfTable.AddCell(cell);
                    }
                    else if (column.ColumnName == "UsedTime")
                    {
                        cell = new PdfPCell(new Phrase("Used Time\n ", Curier));
                        cell.BackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.SteelBlue);
                        pdfTable.AddCell(cell);
                    }
                }

                //Work Service Line Information
                for (int i = 0; i < OrderLineData.Rows.Count; ++i)
                {
                    for (int j = 0; j < OrderLineData.Columns.Count; ++j)
                    {
                        pdfTable.AddCell(OrderLineData.Rows[i][j] == DBNull.Value ? "" : OrderLineData.Rows[i][j].ToString());
                    }
                }

                pdfDoc.Add(pdfTable);

                //Add Total Useage time
                int hours = 0;
                int mintues = 0;
                this.GetTotalTime(out hours, out mintues);
                lblTotalUsedTime.Text = "Total Used Time : " + hours.ToString() + " hours " + mintues.ToString() + " minutes.";
                Paragraph p4 = new Paragraph("\n\nTotal Used Time : " + hours.ToString() + " hours " + mintues.ToString() + " minutes.");
                p4.Alignment = Element.ALIGN_JUSTIFIED;
                pdfDoc.Add(new Paragraph(p4));

                //Add total Cost
                Paragraph p5 = new Paragraph("Total Charge : " + string.Format("{0:C}", orderBO.TotalCharge));
                p4.Alignment = Element.ALIGN_JUSTIFIED;
                pdfDoc.Add(new Paragraph(p5));

                pdfDoc.Close();
                writer.Close();

                using (MailMessage mm = new MailMessage(fromAddress, customerBO.EmailAddress))
                {
                    mm.Subject = "Work Orer Id-" + txtWorkOrder.Text + " Invoice Report";
                    mm.Body = "Work Order Id- " + txtWorkOrder.Text + "'s Invoice Report has been attached as pdf. Please check & to correct miss match information please contact with admin.";

                    if (pdfPath != "")
                    {
                        string FileName = Path.GetFileName(pdfPath);
                        mm.Attachments.Add(new Attachment(pdfPath));
                    }

                    mm.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = true;
                    NetworkCredential NetworkCred = new NetworkCredential(fromAddress, password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                    lblMessage.Text = "Invoice Report has been sent sucessfully.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "cursorDefault()", true);
            }
        }

        private bool IsValidEmailId(string InputEmail)
        {
            try
            {
                //Regex To validate Email Address
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(InputEmail);
                if (match.Success)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

            return false;
        }
    }
}