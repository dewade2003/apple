using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace njapple.Model
{
    public class OrderInfo
    {
        public string Index
        {
            get;
            set;
        }

        public string OrderCode
        {
            get;
            set;
        }

        public string SJName
        {
            get;
            set;
        }

        public string SJAddress
        {
            get;
            set;
        }

        public string SJNumber
        {
            get;
            set;
        }

        public int Count
        {
            get;
            set;
        }

        public string SJRemark
        {
            get;
            set;
        }

        public string FirstExpressCode
        {
            get;
            set;
        }

        public List<ExpressInfo> ExpressCode
        {
            get;
            set;
        }
    }
}
