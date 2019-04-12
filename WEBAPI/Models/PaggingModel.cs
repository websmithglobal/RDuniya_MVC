using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBAPI.Models
{
    public class PaggingModel
    {
        public int PageStart { get; set; }
        public int PageSize { get; set; }
    }
}