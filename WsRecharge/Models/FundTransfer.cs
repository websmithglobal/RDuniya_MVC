using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsRecharge.Models
{
    public class FundTransfer
    {
        public Guid up_upperid { get; set; }
        public Guid up_userid { get; set; }
        public string up_fundbalance { get; set; }
        public string up_mode { get; set; }
        public string up_remakrs { get; set; }
        public string up_password { get; set; }
    }

    public class AddFundTransfer
    {
        public Guid up_userid { get; set; }
        public string up_fundbalance { get; set; }
        public string up_mode { get; set; }
        public string up_password { get; set; }
    }


}