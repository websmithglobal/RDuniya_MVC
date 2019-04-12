using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsRecharge.Models
{
    public class registerresponse
    {
        public int code { get; set; }
        public string message { get; set; }
        public Guid customerid { get; set; }
    }
}