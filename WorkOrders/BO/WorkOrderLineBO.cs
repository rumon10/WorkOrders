using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkOrders.BO
{
    public class WorkOrderLineBO
    {
        private Int64 _transId;

        public Int64 TransId
        {
            get { return _transId; }
            set { _transId = value; }
        }
        private Int64 _workOrderId;

        public Int64 WorkOrderId
        {
            get { return _workOrderId; }
            set { _workOrderId = value; }
        }
        private string _workType;

        public string WorkType
        {
            get { return _workType; }
            set { _workType = value; }
        }
        private string _description = "";

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private decimal _usedTime = 0;

        public decimal UsedTime
        {
            get { return _usedTime; }
            set { _usedTime = value; }
        }

        private DateTime _lastEditDate = DateTime.Now;
        public DateTime LastEditDate
        {
            get { return _lastEditDate; }
            set { _lastEditDate = value; }
        }

    }
}