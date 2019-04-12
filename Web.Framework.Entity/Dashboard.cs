using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Framework.Entity
{
    public class Dashboard
    {
        public string TotalCustomers { get; set; }
        public string TotalBalance { get; set; }
        public string TotalTodayRequests { get; set; }
        public string TotalSuccess { get; set; }
        public string TotalFailed { get; set; }
        public string DMTtotalSuccess { get; set; }

        public string DMTtotalFailed { get; set; }
        public string DMTinproccess { get; set; }
        public string WalletRequests { get; set; }
        public string LoginActivity { get; set; }
    }
}
