using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkOrders.BO
{
    public class UserBO
    {
        private int _userId = 1;

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        private string _userName = "";

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        private string _password = "";

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        private string _fullName = "";

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }
        private string _email = "";

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _userType = "";

        public string UserType
        {
            get { return _userType; }
            set { _userType = value; }
        }

        private bool _isEnable = false;

        public bool IsEnable
        {
            get { return _isEnable; }
            set { _isEnable = value; }
        }
        private DateTime _lastEdit = DateTime.Now;

        public DateTime LastEdit
        {
            get { return _lastEdit; }
            set { _lastEdit = value; }
        }

    }
}