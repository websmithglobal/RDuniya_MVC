using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBAPI.Models
{
    public class WalletRequest
    {
        public Guid userid { get; set; }
        public string wr_bankname { get; set; }
        public string wr_accountno { get; set; }
        public string wr_amount { get; set; }
        public int wr_mode { get; set; }
        public string wr_remakrs { get; set; }
        public string wr_refrenceid { get; set; }
    }
    public class ApproveRequest
    {
        public string wr_id { get; set; }
        public string loginpassword { get; set; }
    }
}