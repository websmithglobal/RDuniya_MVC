using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBAPI.Models
{
    public class GetPendingWalletRequestModel : PaggingModel
    {
        public int ddrange { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public int ddData { get; set; }
    }
}