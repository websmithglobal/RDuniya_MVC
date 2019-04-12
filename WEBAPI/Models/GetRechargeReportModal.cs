using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBAPI.Models
{
    public class GetRechargeReportModal : PaggingModel
    {
        public int ddrange { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string search { get; set; }
        public int ddData { get; set; }
        public int status { get; set; }
        public int operatorcode { get; set; }
    }
}