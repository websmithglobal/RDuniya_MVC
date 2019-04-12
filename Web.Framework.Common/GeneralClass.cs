using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace Web.Framework.Common
{
    public class GeneralClass
    {
        public class ResponseWithOutCode
        {
            public object Key;
            public object ResponeParam;

            public ResponseWithOutCode() { }

            public ResponseWithOutCode(object _Key, object _ResponeParam)
            {
                this.Key = _Key;
                this.ResponeParam = _ResponeParam;
            }
        }

        public static string IpAddress()
        {
            string ConIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ConIP == null)
            {
                ConIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (ConIP == null) ConIP = "NA";
            return ConIP;
        }

        public enum MethodType
        {
            Customer_Validation = 5,
            Customer_Registration = 0,
            OTC_Confirmation = 2,
            Add_Beneficiary_Account = 4,
            Remittance = 3,
            OTC_Resend = 9,
            Re_Initialize = 7,
            Delete_Beneficiary = 6,
            Beneficiary_Account_Validation = 10,
            Get_User_Balance = 13,
            Get_Trans_History = 14,
            Get_Bank_Details = 15,
            Delete_Beneficiary_Confirm = 23
        }

        public static string Validation_URL = "https://in.cyberplat.com/cgi-bin/instp/instp_pay_check.cgi";
        public static string Payment_URL = "https://in.cyberplat.com/cgi-bin/instp/instp_pay.cgi";
        public static string StatusCheck_URL = "https://in.cyberplat.com/cgi-bin/instp/instp_pay_status.cgi";

        public string StatusCheckURL(string SessionID)
        {
            return "SD=104406\r\nAP=234515\r\nOP=234516\r\nSESSION=" + SessionID + "";
        }

        public class GeneralParam
        {
            public string OTC { get; set; }
            public string FName { get; set; }
            public string LName { get; set; }
            public string RoutingType { get; set; }
            public string TransRefID { get; set; }
            public string BeneficiaryAccountNo { get; set; }
            public string OTCRefCode { get; set; }
            public string BeneficiaryIFSC { get; set; }
            public string BeneficiaryName { get; set; }
            public string BeneficiaryCode { get; set; }
            public string BeneficiaryId { get; set; }
            public string Amount { get; set; }
            public string AmountAll { get; set; }
            public string BankName { get; set; }
            public string BranchName { get; set; }
            public string Session { get; set; }
            public string MobileNo { get; set; }
            public MethodType methodType { get; set; }
            public string Comment { get; set; }
            public string Pin { get; set; }
            public string RemitterId { get; set; }
        }

        public string SessioID()
        {
            Random rNext = new Random();
            Int32 mRandomeNo = rNext.Next(123, 999);
            return DateTime.Now.ToString("yyMMddHHmmss") + mRandomeNo.ToString();
        }

        public string General(GeneralParam obj)
        {
            List<string> Request = new List<string>();
            Request.Add("SD=134292");
            Request.Add("AP=175563");
            Request.Add("OP=175566");
            Request.Add("SESSION=" + SessioID() + "");
            Request.Add("Pin=" + obj.Pin + "");
            Request.Add("otc=" + obj.OTC + ""); // VERIFY OTC
            Request.Add("fName=" + obj.FName + ""); // CUSTOMER REGISTRATION
            Request.Add("routingType=" + obj.RoutingType + ""); // ADD BENEFICIARY [IMPS], MONEY TRANSFER [IMPS] BENEFICIARY ACCOUNT VALIDATION [IMPS]
            Request.Add("transRefId=" + obj.TransRefID + ""); // REQUERY (Require Original CyberPlat Transaction ID declined during the remittance transaction)
            Request.Add("mothersMaidenName=");
            Request.Add("state=");
            Request.Add("benAccount=" + obj.BeneficiaryAccountNo + ""); // ADD BENEFICIARY
            Request.Add("TERM_ID=175563"); // PASS IN ALL REQUEST
            Request.Add("benMobile=");
            Request.Add("address=");
            Request.Add("birthday=");
            Request.Add("NUMBER=" + obj.MobileNo + ""); // PASS IN ALL REQUEST
            Request.Add("gender=");
            Request.Add("otcRefCode=" + obj.OTCRefCode + ""); // VERIFY OTC, RESEND OTC
            Request.Add("benNick=");
            Request.Add("remId=" + obj.RemitterId + "");
            Request.Add("benId=" + obj.BeneficiaryId + "");

            if (obj.methodType == MethodType.Get_Bank_Details)
            {
                // THIS PART FOR GET BANK DETAIL / GET BANK DETAIL BY IFSC
                Request.Add("BankName=" + obj.BankName + ""); // GET BANK DETAIL 
                Request.Add("benIFSC=" + obj.BeneficiaryIFSC + ""); // GET BANK DETAIL BY IFSC
                Request.Add("BranchName=" + obj.BranchName + ""); // GET BANK DETAIL 
                Request.Add("lName=");
                Request.Add("benName=");
                Request.Add("benCode=");
                Request.Add("Type=" + ((Int32)obj.methodType).ToString() + "");
                Request.Add("ACCOUNT=");
                Request.Add("AMOUNT=" + obj.Amount + "");
                Request.Add("AMOUNT_ALL=" + obj.AmountAll + "");
                Request.Add("COMMENT=" + obj.Comment + "");
            }
            else
            {
                Request.Add("benIFSC=" + obj.BeneficiaryIFSC + ""); // ADD BENEFICIARY, MONEY TRANSFER, REQUERY, DELETE BENEFICIARY, BENEFICIARY ACCOUNT VALIDATION
                Request.Add("lName=" + obj.LName + ""); // CUSTOMER REGISTER
                Request.Add("benName=" + obj.BeneficiaryName + ""); // ADD BENEFICIARY
                Request.Add("benCode=" + obj.BeneficiaryCode + ""); // MONEY TRANSFER, REQUERY, DELETE BENEFICIARY, BENEFICIARY ACCOUNT VALIDATION
                Request.Add("Type=" + ((Int32)obj.methodType).ToString() + ""); // PASS IN ALL REQUEST
                Request.Add("ACCOUNT=");
                Request.Add("AMOUNT=" + obj.Amount + ""); // MONERY TRANSFER AMOUNT CHANGE, BENEFICIARY ACCOUNT VALIDATION AMOUNT SET 4.00 FIX AS PER DOCUMENT DETAILS, OTHERWISE SET 1.00 IN ALL REQUEST
                Request.Add("AMOUNT_ALL=" + obj.AmountAll + ""); // MONERY TRANSFER AMOUNT CHANGE (check the calculation in the Tech Document) EG. TXN IS 10.00 THAN PASS 14.00 / , BENEFICIARY ACCOUNT VALIDATION AMOUNT SET 4.00 FIX AS PER DOCUMENT DETAILS, OTHERWISE SET 1.00 IN ALL REQUEST
                Request.Add("COMMENT=" + obj.Comment + ""); // MONEY TRANSFER [Remittance], RESEND OTC [OTC Resend], REQUERY [ReInitiate], BENEFICIARY ACCOUNT VALIDATION [Account Validation] / GET TRANSACTION HISTORY PASS BALNK IN COMMENT
            }

            return string.Join("\r\n", Request.ToArray());
        }
        public static DataSet ReadDataFromJson(string jsonString)
        {
            var xd = new XmlDocument();
            jsonString = "{ \"rootNode\": {" + jsonString.Trim().TrimStart('{').TrimEnd('}') + @"} }";
            xd = JsonConvert.DeserializeXmlNode(jsonString);
            var result = new DataSet();
            result.ReadXml(new XmlNodeReader(xd));
            return result;
        }
    }
}
