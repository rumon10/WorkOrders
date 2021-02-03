using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkOrders.BAL;
using WorkOrders.BO;
using System.Text.RegularExpressions;

namespace WorkOrders
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.existLogin() == false)
            {
                Response.Redirect("WorkUserLogin.aspx");
            }

            if (!this.IsPostBack)
            {
                this.displayUserInformation();
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

        protected void btnAddNewUser_Click(object sender, EventArgs e)
        {
            try
            {
                this.addNewUser();
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

        private void addNewUser()
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "cursorDefault()", true);
                if (this.IsValidEmailId(txtEmail.Text) == false)
                {
                    lblMessage.Text = "Invalid Email Address. Please put the valid email address.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (UserBAL.ExistsUserName(txtUsername.Text.Trim()) == true)
                {
                    lblMessage.Text = "Username " + txtUsername .Text + " already exists. Please enter unique user name to identify each user.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                UserBO userBO = new UserBO();
                userBO.Email = txtEmail.Text;
                userBO.FullName = txtFullName.Text;
                userBO.IsEnable = chbUserStatus.Checked;
                userBO.Password = txtPassword.Text;
                userBO.UserName = txtUsername.Text;
                userBO.UserType = ddlUserType.Text;

                UserBAL userBAL = new UserBAL();
                bool success = userBAL.addNewUser(userBO);

                if (success == true)
                {
                    this.displayUserInformation();
                    lblMessage.Text = "Username " + userBO.UserName + " has been created sucessfully.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMessage.Text = "User creation Failed. Please try again.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
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

        private void displayUserInformation()
        {
            try
            {
                UserBAL userBAL = new UserBAL();
                System.Data.DataTable data = new System.Data.DataTable();
                data = userBAL.UserInfo();
                gvUser.DataSource = data;
                gvUser.DataBind();

                if (data.Rows.Count == 0)
                {
                    lblMessage.Text = "No User Information.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void gvUser_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gvUser.EditIndex = -1;
                this.displayUserInformation();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void gvUser_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvUser.EditIndex = e.NewEditIndex;
                this.displayUserInformation();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void gvUser_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                string userName = ((TextBox)gvUser.Rows[e.RowIndex].FindControl("txtUserName")).Text;
                string fullName = ((TextBox)gvUser.Rows[e.RowIndex].FindControl("txtFullName")).Text;
                string email = ((TextBox)gvUser.Rows[e.RowIndex].FindControl("txtEmail")).Text;
                bool enable = ((CheckBox)gvUser.Rows[e.RowIndex].FindControl("chbIsEnabled")).Checked;
                string userType = ((DropDownList)gvUser.Rows[e.RowIndex].FindControl("ddlUserType")).Text;
                
                if (this.IsValidEmailId(email) == false)
                {
                    lblMessage.Text = "Invalid Email Address. Please put the valid email address.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                
                UserBO userBO = new UserBO();
                userBO.Email = email;
                userBO.FullName = fullName;
                userBO.IsEnable = enable;
                userBO.UserName = userName;
                userBO.UserType = userType;

                UserBAL userBAL = new UserBAL();
                bool success = userBAL.editUser(userBO);

                if (success == true)
                {
                    gvUser.EditIndex = -1;
                    this.displayUserInformation();
                    lblMessage.Text = "Username " + userBO.UserName + " iformation has been modified sucessfully.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMessage.Text = "User information update Failed. Please try again.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
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

        protected void DeleteUser(object sender, EventArgs e)
        {
            try
            {
                LinkButton linkbtn = (LinkButton)sender;
                string userName = linkbtn.CommandArgument;
                if (string.IsNullOrEmpty(userName) == true)
                {
                    lblMessage.Text = "No Record is selected.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    UserBAL userBAL = new UserBAL();
                    bool success = userBAL.deleteUser(userName);

                    if (success == true)
                    {
                        this.displayUserInformation();
                        lblMessage.Text = "Username " + userName + " iformation has been deleted sucessfully.";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblMessage.Text = "User information delete Failed. Please try again.";
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
    }
}