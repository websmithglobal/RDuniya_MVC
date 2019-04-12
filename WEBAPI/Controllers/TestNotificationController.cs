using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ENT = Web.Framework.Entity;
using BAL = Web.Framework.BusinessLayer;

namespace WEBAPI.Controllers
{
    public class TestNotificationController : ApiController
    {
        [HttpGet]
        [ActionName("SendNotification")]
        public HttpResponseMessage SendNotification()
        {
            GlobalVarible.Clear();

            using (BAL.UserDeviceToken objUDT = new BAL.UserDeviceToken())
            {
                ENT.UserDeviceToken usertoken = objUDT.GetUserDeviceToken(Guid.Parse("5a5537fc-0411-45c1-8960-b24bbf1ea375"));

                if (usertoken != null)
                {
                    string message = "Your account is credited with with 100. : "+DateTime.Now.Ticks.ToString();
                    FCMNOTIFICATION.SendPushNotification(usertoken.udt_devicetoken, message, Guid.Parse("5a5537fc-0411-45c1-8960-b24bbf1ea375"), 1);
                }
            }

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
        }
    }
}
