using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ENT = Web.Framework.Entity;
using BAL = Web.Framework.BusinessLayer;
using COM = Web.Framework.Common;
using WEBAPI.Models;
using System.Web.Script.Serialization;

namespace WEBAPI.Controllers
{
    public class ApplicationController : BaseApiController
    {
        /// <summary>
        /// This api is used to  get latest balance of logedin user
        /// </summary>
        /// <returns>Final balance of user.</returns>
        /// <remarks>Get all services from database whose status is active</remarks>
        [HttpPost]
        [ActionName("GetLatestVersion")]
        [Route("api/Application/GetLatestVersion")]
        public HttpResponseMessage GetLatestVersion()
        {
            GlobalVarible.Clear();

            try
            {
                using (BAL.ApplicationVersion objBal = new BAL.ApplicationVersion())
                {
                    ENT.ApplicationVersionView av = objBal.GetLatestVersion();

                    GlobalVarible.AddMessage("Application Version Get Successfully.");

                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult, av });
                }
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
                ERRORREPORTING.Report(ex, _REQUESTURL, _LOGINUSERID, _ERRORKEY, string.Empty);
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
            }
        }
    }
}
