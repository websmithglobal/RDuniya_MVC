using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBAPI.WEBRECHARGE.ENTITIES
{
    public class BENEFICIARYREGISTRATIONRESPONSE
    {
        public int Code { get; set; }
        public MESSAGE Message { get; set; }
    }

    public class MESSAGE
    {
        public string statuscode { get; set; }
        public string status { get; set; }
        public DATA1 data { get; set; }
    }

    public class BENEFICIARY1
    {
        public int id { get; set; }
    }

    public class REMITTER1
    {
        public object kycstatus { get; set; }
        public int consumedlimit { get; set; }
        public object name { get; set; }
        public int remaininglimit { get; set; }
        public object mobile { get; set; }
        public object state { get; set; }
        public object pincode { get; set; }
        public object kycdocs { get; set; }
        public int perm_txn_limit { get; set; }
        public object city { get; set; }
        public string id { get; set; }
        public object address { get; set; }
    }

    public class DATA1
    {
        public REMITTER1 remitter { get; set; }
        public BENEFICIARY1 beneficiary { get; set; }
    }

}