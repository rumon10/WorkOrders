using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkOrders
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                bool IsLogin = (Session["Islogin"] == null ? false : Convert.ToBoolean(Session["Islogin"]));
                int userId = (Session["UserId"] == null ? 0 : Convert.ToInt16(Session["UserId"].ToString ()));

                if (IsLogin == true  && userId != 0)
                    Response.Redirect("CreateWorkOrder.aspx");
                else
                    Response.Redirect("WorkUserLogin.aspx");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
