using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Framework.Entity
{
    public class DMT_OTP_Confirmation
    {

    }
    public class EntityOTPConfirmation
    {
        public string BeneficiaryMobile { get; set; }
        public string RemitterId { get; set; }
        public string BeneficiaryCode { get; set; }
        public string OTP { get; set; }
        public string CustomerMobile { get; set; }
    }
}
