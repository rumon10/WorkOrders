using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace WorkOrders.BAL
{
    public class DatabaseConnector
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
    }
}