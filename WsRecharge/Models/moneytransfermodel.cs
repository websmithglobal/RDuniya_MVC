using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsRecharge.Models
{
    public class moneytransfermodel
    {
        public Guid customerid { get; set; }
        public Guid benificaryid { get; set; }
        public String password { get; set; }
        public String transfertype { get; set; }
        public String remarks { get; set; }
        public double transferamount { get; set; }
    }
}