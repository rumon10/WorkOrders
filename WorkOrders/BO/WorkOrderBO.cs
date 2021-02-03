using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkOrders.BO
{
    public class WorkOrderBO
    {
        private Int64 _workOrderId;

        public Int64 WorkOrderId
        {
            get { return _workOrderId; }
            set { _workOrderId = value; }
        }
        private DateTime _orderDate;

        public DateTime OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; }
        }
        private Int32 _customerId;

        public Int32 CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        private string _customerName = "";
        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }

        private Int32 _techId;

        public Int32 TechId
        {
            get { return _techId; }
            set { _techId = value; }
        }

        private string _techName = "";

        public string TechName
        {
            get { return _techName; }
            set { _techName = value; }
        }

        private decimal _totalUsedTime;

        public decimal TotalUsedTime
        {
            get { return _totalUsedTime; }
            set { _totalUsedTime = value; }
        }
        private decimal _totalCharge;

        public decimal TotalCharge
        {
            get { return _totalCharge; }
            set { _totalCharge = value; }
        }
        private DateTime _createdDatetime;

        public DateTime CreatedDatetime
        {
            get { return _createdDatetime; }
            set { _createdDatetime = value; }
        }

        private string _lastEdit = "";

        public string LastEdit
        {
            get { return _lastEdit; }
            set { _lastEdit = value; }
        }
    }
}