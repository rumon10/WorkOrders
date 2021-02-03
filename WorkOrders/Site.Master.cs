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
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.checkandSetmenu();
        }

        private void checkandSetmenu()
        {
            try
            {
                bool IsLogin = (Session["Islogin"] == null ? false : Convert.ToBoolean(Session["Islogin"]));
                int userId = (Session["UserId"] == null ? 0 : Convert.ToInt16(Session["UserId"].ToString()));
                string username = (Session["UserName"] == null ? "" : Session["UserName"].ToString());

                if (IsLogin == false && (userId == 0 || username == ""))
                {
                    Response.Redirect("WorkUserLogin.aspx");
                }
                else if (IsLogin == true && userId != 0)
                {
                    UserBAL userBAL = new UserBAL();
                    UserBO userBO = userBAL.userLogin(username);

                    if (userBO != null && userBO.UserType == "User")
                    {
                        if (NavigationMenu.Items[1].Text == "Create User")
                            NavigationMenu.Items.RemoveAt(1);
                    }
                    else if (userBO != null && userBO.UserType == "Admin")
                    {
                        if (NavigationMenu.Items[1].Text != "Create User")
                            NavigationMenu.Items.AddAt(1, new MenuItem("Create User", "Create User", "", "~/Users.aspx"));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void NavigationMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            try
            {
                if (e.Item.Text == "Logout")
                {
                    Session["Islogin"] = null;
                    Session["userName"] = null;
                    Session["FullName"] = null;
                    Session["userId"] = 0;
                    Session["Email"] = null;
                    Session["UserType"] = "User";
                    Response.Redirect("~/WorkUserLogin.aspx");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
