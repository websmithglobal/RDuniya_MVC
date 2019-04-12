using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Framework.Entity
{
    public class DMT_Resend_OTP
    {

    }
    public class EntityResendOTP
    {
        public string BeneficiaryCode { get; set; }
        public string RemitterId { get; set; }
        public string BeneficiaryMobile { get; set; }
        public string CustomerMobile { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BeneficiaryAccountNo { get; set; }
        public string BeneficiaryIFSC { get; set; }
        public string Pincode { get; set; }
    }
}
