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
    public partial class PasswordChange : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.existLogin() == false)
            {
                Response.Redirect("WorkUserLogin.aspx");
            }
        }

        private bool existLogin()
        {
            try
            {
                bool IsLogin = (Session["Islogin"] == null ? false : Convert.ToBoolean(Session["Islogin"]));
                int userId = (Session["UserId"] == null ? 0 : Convert.ToInt16(Session["UserId"].ToString()));
                string username = (Session["UserName"] == null ? "" : Session["UserName"].ToString());

                if(username == "" || userId == 0)
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

        protected void btnAddNewUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.existLogin() == false)
                {
                    Response.Redirect("WorkUserLogin.aspx");
                    return;
                }

                string username = (Session["UserName"] == null ? "" : Session["UserName"].ToString());
                UserBAL userBAL = new UserBAL();
                UserBO userBO = userBAL.userLogin(username);

                if (userBO == null || userBO.UserId == 0)
                {
                    lblMessage.Text = "Username " + username + " does not exist. Please contact with administrator." ;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (userBO.Password != txtCurrentPassword.Text)
                {
                    lblMessage.Text = "Incorrect Password. Please type correct password to change password.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                bool Success = userBAL.changePassword(username, txtNewPassword.Text);
                if (Success == true)
                {
                    txtConfirmPassword.Text = "";
                    txtCurrentPassword.Text = "";
                    txtNewPassword.Text = "";
                    lblMessage.Text = "Password has been changed Sucessfully.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    return;
                }
                else
                {
                    lblMessage.Text = "Password changed Failed. Please try again.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
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
    }
}