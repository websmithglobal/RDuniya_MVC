using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Web.Framework.Entity
{
    public class CyberReponseClass
    {
        public string Date { get; set; }
        public string Session { get; set; }
        public string Error { get; set; }
        public string Result { get; set; }
        public string TransID { get; set; }
        public string AddInfo { get; set; }
        public string AuthCode { get; set; }
        public string ErrorMsg { get; set; }
        public string TRNXSTATUS { get; set; }
    }

    public class Beneficiary
    {
        public int id { get; set; }
    }

    public class CustomerRegistrationData
    {
        public Remitter remitter { get; set; }
        public Beneficiary beneficiary { get; set; }
    }

    public class CustomerRegistrationResponse
    {
        public string statuscode { get; set; }
        public string status { get; set; }
        public CustomerRegistrationData data { get; set; }
    }

    public class Remitter
    {
        public string kycstatus { get; set; }
        public int consumedlimit { get; set; }
        public string name { get; set; }
        public int remaininglimit { get; set; }
        public string mobile { get; set; }
        public string state { get; set; }
        public string pincode { get; set; }
        public string kycdocs { get; set; }
        public int perm_txn_limit { get; set; }
        public string city { get; set; }
        public string id { get; set; }
        public string address { get; set; }
    }

    public class CustomerValidationData
    {
        public List<object> beneficiary { get; set; }
        public Remitter remitter { get; set; }
    }

    public class CustomerValidationResponse
    {
        public string status { get; set; }
        public string statuscode { get; set; }
        public CustomerValidationData data { get; set; }
    }

    public class BenificaryRegisterData
    {
        public Remitter remitter { get; set; }
        public Beneficiary beneficiary { get; set; }
    }

    public class BenificaryRegisterResponse
    {
        public string statuscode { get; set; }
        public string status { get; set; }
        public BenificaryRegisterData data { get; set; }
    }
    
    public class DataBankDetail
    {
        public string id { get; set; }
        public string bank_name { get; set; }
        public string imps_enabled { get; set; }
        public string aeps_enabled { get; set; }
        public string bank_sort_name { get; set; }
        public string branch_ifsc { get; set; }
        public string bank_iin { get; set; }
        public string is_down { get; set; }
        public string ifsc_alias { get; set; }
    }

    public class BankDetailResponse
    {
        public string statuscode { get; set; }
        public string status { get; set; }
        public List<DataBankDetail> data { get; set; }
    }

    public class OtpConfirmResponse
    {
        public string statuscode { get; set; }
        public string status { get; set; }
        public string data { get; set; }
    }
    
    public class MoneyRemittanceResponse
    {
        public string status { get; set; }
        public string statuscode { get; set; }
        public Data data { get; set; }
    }
    public class Data
    {
        public string charged_amount { get; set; }
        public string ref_no { get; set; }
        public string name { get; set; }
        public string amount { get; set; }
        public int ccf_bank { get; set; }
        public string bank_alias { get; set; }
        public int locked_amt { get; set; }
        public string opr_id { get; set; }
        public string ipay_id { get; set; }
        public string total_amount { get; set; }
    }
    public class Data2
    {
        public int otp { get; set; }
    }

    public class DeletebeneficiryResponse
    {
        public string statuscode { get; set; }
        public string status { get; set; }
        public Data2 data { get; set; }
    }
}
