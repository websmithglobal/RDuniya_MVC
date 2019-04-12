using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBAPI.ENTITIES
{
    public class RECHARGERESPONSE
    {
        public IList<RECHARGERESPONSERESULT> results { get; set; }
    }

    public class RECHARGERESPONSERESULT
    {
        public int status_code { get; set; }
        public string status_description { get; set; }
        public string transNo { get; set; }
        public string authorization_code { get; set; }
        public string username { get; set; }
        public int operator_code { get; set; }
        public double amount { get; set; }
        public string requestid { get; set; }
        public string accountref { get; set; }
        public double balance { get; set; }
        public string transDate { get; set; }
        public string transTime { get; set; }
    }
}