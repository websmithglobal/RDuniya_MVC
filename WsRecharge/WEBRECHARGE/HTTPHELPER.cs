using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WEBRECHARGE.ENTITIES;

namespace WEBRECHARGE
{
    public class HTTPHELPER
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

        public HttpWebResponse ProcessRequest(object _postdata, string _url,string _token)
        {
            HttpWebResponse httpWebResponse = null;
            try
            {
                //request to get the access token
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(_url);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/json";

                webRequest.Headers["Authorization"] = "bearer " + _token.Trim();

                string jsontopost = JsonConvert.SerializeObject(_postdata);
                byte[] data = Encoding.UTF8.GetBytes(jsontopost);

                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(data, 0, data.Length);
                }

                httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return httpWebResponse;
        }
    }
}
