using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkOrders.BAL;
using WorkOrders.BO;

namespace WorkOrders
{
    public partial class WorkUserLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblLoginMessage.Text = string.Empty;
            txtUsername.Attributes.Add("onfocus", "if (this.value == 'Username') {this.value = '';}");
            txtPassword.Attributes.Add("onfocus", "if (this.value == 'Password') {this.value = '';}");
        }

        private void userLogin()
        {
            try
            {
                lblLoginMessage.Text = string.Empty;

                if (String.IsNullOrEmpty(txtUsername.Text.Trim()) == false 
                    && string.IsNullOrEmpty(txtPassword.Text.Trim()) == false)
                {

                    lblLoginMessage.Text = "Work Order Application System Login ...";
                    LoginUserBAL  userloginBAL = new LoginUserBAL();
                    UserBO  userBO;
                    string message = "";
                    bool isLogin = userloginBAL.userLogin(txtUsername.Text, txtPassword.Text, out userBO, out message);

                    if (isLogin == true)
                    {
                        if (message == "")
                        {
                            Session["Islogin"] = true;
                            Session["userName"] = userBO.UserName;
                            Session["FullName"] = userBO.FullName;
                            Session["userId"] = userBO.UserId;
                            Session["Email"] = userBO.Email;
                            Session["UserType"] = userBO.UserType;
                            Response.Redirect("CreateWorkOrder.aspx");
                        }
                        else
                        {
                            lblLoginMessage.Text = message;
                        }
                    }
                    else
                    {
                        lblLoginMessage.Text = message;
                    }

                }
            }
            catch (Exception ex)
            {
                lblLoginMessage.Text = "Error : " + ex.Message;
                lblLoginMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack == true)
                    return;
                lblLoginMessage.Text = string.Empty;

                if (string.IsNullOrEmpty(txtUsername.Text.Trim()) == true
                    && string.IsNullOrEmpty(txtPassword.Text.Trim()) == true)
                {
                    return;
                }
                else if (string.IsNullOrEmpty(txtUsername.Text.Trim()) == true)
                {
                    return;
                }
                else if (string.IsNullOrEmpty(txtPassword.Text.Trim()) == true)
                {
                    return;
                }
                else
                {
                    userLogin();
                }
            }
            catch (Exception ex)
            {
                lblLoginMessage.Text = "Error : " + ex.Message;
                lblLoginMessage.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "cursorDefault()", true);
            }
        }

        protected void txtPinNumber_PreRender(object sender, EventArgs e)
        {
            txtUsername.Attributes["value"] = "Username";
            txtPassword.Attributes["value"] = "Password";
        }
    }
}