using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Framework.Common
{
    public class ResponseClass
    {
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string TransNo { get; set; }
        public string AuthorizationCode { get; set; }
        public string UserName { get; set; }
        public int Operatorcode { get; set; }
        public decimal Amount { get; set; }
        public string AccountRef { get; set; }
        public string OurAccountRef { get; set; }
        public decimal Balance { get; set; }
        public string TransDate { get; set; }

        public ResponseClass() { }
        public ResponseClass(int _StatusCode, string _StatusDescription,string _TransNo,string _AuthorizationCode,string _UserName,int _Operatorcode, decimal _Amount,string _AccountRef,string _OurAccountRef,decimal _Balance,string _TransDate)
        {
            StatusCode = _StatusCode; StatusDescription = _StatusDescription;
            TransNo = _TransNo; AuthorizationCode = _AuthorizationCode;
            UserName = _UserName; Operatorcode = _Operatorcode;
            Amount = _Amount; AccountRef = _AccountRef;
            OurAccountRef = _OurAccountRef; Balance = _Balance;
            TransDate = _TransDate;
        }
    }
}
