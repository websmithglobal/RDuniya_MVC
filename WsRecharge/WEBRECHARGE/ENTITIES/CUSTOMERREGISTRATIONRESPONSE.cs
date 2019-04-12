using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsRecharge.WEBRECHARGE.ENTITIES
{
    public class CUSTOMERREGISTRATIONRESPONSE
    {
        public string Code { get; set; }
        public object Message { get; set; }
    }

    public class Message
    {
        public string status { get; set; }
        public string statuscode { get; set; }
        public DATA data { get; set; }
    }

    public class DATA
    {
        public List<BENEFICIARY> beneficiary { get; set; }
        public REMITTER remitter { get; set; }
    }

    public class BENEFICIARY
    {
        public string imps { get; set; }
        public string status { get; set; }
        public string account { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string ifsc { get; set; }
        public string bank { get; set; }
        public string last_success_name { get; set; }
        public string id { get; set; }
        public string last_success_date { get; set; }
        public string last_success_imps { get; set; }
    }

    public class REMITTER
    {
        public string kycstatus { get; set; }
        public int consumedlimit { get; set; }
        public string name { get; set; }
        public int remaininglimit { get; set; }
        public string mobile { get; set; }
        public string state { get; set; }
        public string pincode { get; set; }
        public string kycdocs { get; set; }
        public int perm_txn_limit { get; set; }
        public string city { get; set; }
        public string id { get; set; }
        public string address { get; set; }
    }
}