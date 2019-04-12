using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WEBRECHARGE.ENTITIES;

namespace WEBRECHARGE
{
    public class RECHARGE : HTTPHELPER
    {
        private string _username { get; set; }
        private string _password { get; set; }
        private string _logintoken { get; set; }
        private string _apiurl { get; set; }

        public RECHARGE(string u , string p ,string a) {
            _username = u;
            _password = p;
            _apiurl = a+ "api/Recharge/";
            var t = validateUser(_username, _password,a+"token");
            if(t != null)
            {
                _logintoken = t.access_token;
            }
        }

        public RECHARGERESPONSE ProcessRecharge(object postdata, string apiname)
        {
            RECHARGERESPONSE rECHARGERESPONSE = new RECHARGERESPONSE();

            var httpresponse = ProcessRequest(postdata, _apiurl +apiname,_logintoken);

            using (Stream respStr = httpresponse.GetResponseStream())
            {
                using (StreamReader rdr = new StreamReader(respStr, Encoding.UTF8))
                {
                    string response = rdr.ReadToEnd();
                    rdr.Close();
                    rECHARGERESPONSE = JsonConvert.DeserializeObject<RECHARGERESPONSE>(response);
                }
            }

            return rECHARGERESPONSE;
        }
    }
}
