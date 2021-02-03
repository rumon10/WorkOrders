using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkOrders
{
    public partial class WorkOrder : System.Web.UI.MasterPage
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

                if (IsLogin == true && (userId != 0 || username != ""))
                {
                    Response.Redirect("CreateWorkOrder.aspx");
                }
                else
                {
                    Session["Islogin"] = null;
                    Session["userName"] = null;
                    Session["FullName"] = null;
                    Session["userId"] = 0;
                    Session["Email"] = null;
                    Session["UserType"] = "User";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}