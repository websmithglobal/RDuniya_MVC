using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBAPI;
using ENT = Web.Framework.Entity;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using WEBAPI.ENTITIES;

namespace WEBAPI
{
    public class DMT : HTTPHELPER
    {
        private string _username { get; set; }
        private string _password { get; set; }
        private string _logintoken { get; set; }
        private string _apiurl { get; set; }

        public DMT(string u, string p, string a)
        {
            _username = u;
            _password = p;
            _apiurl = a + "api/Remittance/";
            var t = validateUser(_username, _password, a + "token");
            if (t != null)
            {
                _logintoken = t.access_token;
            }
        }

        public string CheckUser(object postdata, string apiname)
        {
            string response = string.Empty;

            var httpresponse = ProcessRequest(postdata, _apiurl + apiname, _logintoken);

            RECHARGERESPONSE rECHARGERESPONSE = new RECHARGERESPONSE();

            using (Stream respStr = httpresponse.GetResponseStream())
            {
                using (StreamReader rdr = new StreamReader(respStr, Encoding.UTF8))
                {
                    response = rdr.ReadToEnd();
                    rdr.Close();
                }
            }
            return response;
        }

        public string RegisterCustomer(object postdata, string apiname)
        {
            string response = string.Empty;

            var httpresponse = ProcessRequest(postdata, _apiurl + apiname, _logintoken);

            RECHARGERESPONSE rECHARGERESPONSE = new RECHARGERESPONSE();

            using (Stream respStr = httpresponse.GetResponseStream())
            {
                using (StreamReader rdr = new StreamReader(respStr, Encoding.UTF8))
                {
                    response = rdr.ReadToEnd();
                    rdr.Close();
                }
            }
            return response;
        }

        public string ValidateCustomerOTP(object postdata, string apiname)
        {
            string response = string.Empty;

            var httpresponse = ProcessRequest(postdata, _apiurl + apiname, _logintoken);

            RECHARGERESPONSE rECHARGERESPONSE = new RECHARGERESPONSE();

            using (Stream respStr = httpresponse.GetResponseStream())
            {
                using (StreamReader rdr = new StreamReader(respStr, Encoding.UTF8))
                {
                    response = rdr.ReadToEnd();
                    rdr.Close();
                }
            }
            return response;
        }

        public string RegisterBenificary(object postdata, string apiname)
        {
            string response = string.Empty;

            var httpresponse = ProcessRequest(postdata, _apiurl + apiname, _logintoken);

            RECHARGERESPONSE rECHARGERESPONSE = new RECHARGERESPONSE();

            using (Stream respStr = httpresponse.GetResponseStream())
            {
                using (StreamReader rdr = new StreamReader(respStr, Encoding.UTF8))
                {
                    response = rdr.ReadToEnd();
                    rdr.Close();
                }
            }
            return response;
        }

        public string DeleteBeneficary(object postdata, string apiname)
        {
            string response = string.Empty;

            var httpresponse = ProcessRequest(postdata, _apiurl + apiname, _logintoken);

            RECHARGERESPONSE rECHARGERESPONSE = new RECHARGERESPONSE();

            using (Stream respStr = httpresponse.GetResponseStream())
            {
                using (StreamReader rdr = new StreamReader(respStr, Encoding.UTF8))
                {
                    response = rdr.ReadToEnd();
                    rdr.Close();
                }
            }
            return response;
        }

        public string TransferAmount(object postdata, string apiname)
        {
            string response = string.Empty;

            var httpresponse = ProcessRequest(postdata, _apiurl + apiname, _logintoken);

            RECHARGERESPONSE rECHARGERESPONSE = new RECHARGERESPONSE();

            using (Stream respStr = httpresponse.GetResponseStream())
            {
                using (StreamReader rdr = new StreamReader(respStr, Encoding.UTF8))
                {
                    response = rdr.ReadToEnd();
                    rdr.Close();
                }
            }
            return response;
        }
    }
}