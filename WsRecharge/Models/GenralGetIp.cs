using Microsoft.Owin.BuilderProperties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using ENT = Web.Framework.Entity;
using BAL = Web.Framework.BusinessLayer;
using Microsoft.AspNet.Identity;
using System.Globalization;

namespace WsRecharge.Models
{
    public class GenralGetIp
    {
        public string GetIPAddress(HttpRequestBase request)
        {

            string ip;
            try
            {
                ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(ip))
                {
                    if (ip.IndexOf(",") > 0)
                    {
                        string[] ipRange = ip.Split(',');
                        int le = ipRange.Length - 1;
                        ip = ipRange[le];
                    }
                }
                else
                {
                    ip = request.UserHostAddress;
                }
            }
            catch { ip = null; }

            return ip;
        }
        public string GetMacAddress()
        {
            const int MIN_MAC_ADDR_LENGTH = 12;
            string macAddress = string.Empty;
            long maxSpeed = -1;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                string tempMac = nic.GetPhysicalAddress().ToString();
                if (nic.Speed > maxSpeed &&
                    !string.IsNullOrEmpty(tempMac) &&
                    tempMac.Length >= MIN_MAC_ADDR_LENGTH)
                {

                    maxSpeed = nic.Speed;
                    macAddress = tempMac;
                }
            }
            return macAddress;
        }
        public string[] GetBrowerInformation()
        {
            string[] Result = new string[3];
            try
            {
                var userAgent = HttpContext.Current.Request.UserAgent;
                var userBrowser = new HttpBrowserCapabilities { Capabilities = new Hashtable { { string.Empty, userAgent } } };
                var factory = new BrowserCapabilitiesFactory();
                factory.ConfigureBrowserCapabilities(new NameValueCollection(), userBrowser);
                Result[0] = userBrowser.Capabilities["browser"].ToString();
                Result[1] = userBrowser.Capabilities["version"].ToString();
                Result[2] = userAgent;
            }
            catch
            {
                Result[0] = "NA"; Result[1] = "NA"; Result[2] = "NA";
                return Result;
            }
            return Result;
        }
        
    }

}
