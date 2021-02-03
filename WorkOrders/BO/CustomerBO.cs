using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkOrders.BO
{
    public class CustomerBO
    {
        private int _customerId = 0;

        public int CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }
        private string _customerName = string.Empty;

        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }
        private string _contactNo = "";

        public string ContactNo
        {
            get { return _contactNo; }
            set { _contactNo = value; }
        }
        private string _emailAddress = "";

        public string EmailAddress
        {
            get { return _emailAddress; }
            set { _emailAddress = value; }
        }
        private string _address = "";

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        private string _city = "";

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        private string _state = "";

        public string State
        {
            get { return _state; }
            set { _state = value; }
        }
        private string _zipCode = "";

        public string ZipCode
        {
            get { return _zipCode; }
            set { _zipCode = value; }
        }
        private bool _status = false;

        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}