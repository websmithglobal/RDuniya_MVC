using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WEBCHARGE.ENTITIES;

namespace WEBCHARGE
{
    public class VALIDATEUSER
    {
        public Token validateUser(string _username, string _password, string _url)
        {
            Token token = new Token();
            try
            {
                String postdata = "grant_type=password&username=" + _username + "&password=" + _password;

                //request to get the access token
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(_url);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                byte[] credentials = Encoding.UTF8.GetBytes(postdata);
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(credentials, 0, credentials.Length);
                }

                HttpWebResponse resp = (HttpWebResponse)webRequest.GetResponse();
                using (Stream respStr = resp.GetResponseStream())
                {
                    using (StreamReader rdr = new StreamReader(respStr, Encoding.UTF8))
                    {
                        //should get back a string i can then turn to json and parse for accesstoken
                        string responseString = rdr.ReadToEnd();
                        token = JsonConvert.DeserializeObject<Token>(responseString);
                        rdr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                token = null;
                throw ex;
            }
            return token;
        }
    }
}
