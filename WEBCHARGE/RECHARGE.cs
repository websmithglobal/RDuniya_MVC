using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WEBCHARGE.ENTITIES;

namespace WEBCHARGE
{
    public class RECHARGE : VALIDATEUSER
    {
        private string _username { get; set; }
        private string _password { get; set; }
        private string _logintoken { get; set; }
        private string _apiurl { get; set; }

        public RECHARGE(string u , string p ,string a) {
            _username = u;
            _password = p;
            _apiurl = a;
            var t = validateUser(_username, _password,a+"/token");
            if(t != null)
            {
                _logintoken = t.access_token;
            }
        }

        public string ProcessRecharge(object postdata, string apiname) {
            string response = string.Empty;

            return response;
        }
    }
}
