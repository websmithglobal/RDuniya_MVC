using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

    /// <summary>
    /// FCMNotification : this class is used for send custome fcm notification.
    /// </summary>
    public class FCMNotification
    {
        public string ServerKey = System.Configuration.ConfigurationManager.AppSettings["ServerKey"];
        public string SenderID = System.Configuration.ConfigurationManager.AppSettings["SenderID"];

        public FCMResponse PushNotifyAsync(string to, string message, Guid userid,int deviceType = 1)
        {
            FCMResponse res = new FCMResponse();
            string response = string.Empty;
            try
            {
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "POST";
                tRequest.ContentType = "application/json";

                // create data object
                FCMRootObject dataObject = new FCMRootObject();
                dataObject.priority = "high";
                dataObject.content_available = true;
                dataObject.to = to;

                Notification NotificationBody = new Notification
                {
                    body = message,
                    title = "WEB RECHARGE",
                    sound = "default"
                };

                // if devicetype = 0 then IOS and 1 for android
                // so we are not passing NotificationBody to android
                if (deviceType == 0)
                {
                    dataObject.notification = NotificationBody;
                }

                Data data = new Data
                {
                    Description = message,
                    NotificationTitle = "WEB RECHARGE",
                    UserId=userid,
                };

                // create data object end
                dataObject.data = data;

                string jsonNotificationFormat = Newtonsoft.Json.JsonConvert.SerializeObject(dataObject);
                Byte[] byteArray = Encoding.UTF8.GetBytes(jsonNotificationFormat);

                tRequest.Headers.Add(string.Format("Authorization: key={0}", ServerKey));
                tRequest.Headers.Add(string.Format("Sender: id={0}", SenderID));

                tRequest.ContentLength = byteArray.Length;
                tRequest.ContentType = "application/json";
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                response = tReader.ReadToEnd();
                                res = Newtonsoft.Json.JsonConvert.DeserializeObject<FCMResponse>(response);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }
            return res;
        }

        internal class FCMRootObject
        {
            public string to { get; set; }
            public Notification notification { get; set; }
            public Data data { get; set; }
            public string priority { get; set; }
            public bool content_available { get; set; }
        }

        internal class Data
        {
            public string Description { get; set; }
            public string NotificationTitle { get; set; }
            public Guid UserId { get; set; }
        }

        internal class Notification
        {
            public Notification()
            {
                this.title = "Fixzy";
                this.body = "Fixzy Notification";
                this.sound = "default";
            }

            public string title { get; set; }
            public string body { get; set; }
            public string sound { get; set; }
        }
    }

    public class FCMResponse
    {
        public long multicast_id { get; set; }
        public int success { get; set; }
        public int failure { get; set; }
        public int canonical_ids { get; set; }
        public List<FCMResult> results { get; set; }
    }

    public class FCMResult
    {
        public string message_id { get; set; }
        public string error { get; set; }
    }