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
using System.Web;
using System.Web.Script.Serialization;
using System.Security.Claims;

namespace WEBAPI.Controllers
{
    public class RechargeController : BaseApiController
    {
        /// <summary>
        /// This api is used to get list of all active services for india
        /// </summary>
        /// <returns>List of all available services.</returns>
        /// <remarks>Get all services from database whose status is active</remarks>
        [HttpPost]
        [Authorize]
        [ActionName("GetServices")]
        [Route("api/Recharge/GetServices")]
        public HttpResponseMessage GetServices()
        {
            GlobalVarible.Clear();
            List<ENT.ServiceMasterView> lstEntity = new List<ENT.ServiceMasterView>();
            try
            {
                using (BAL.ServiceMaster objBal = new BAL.ServiceMaster())
                {
                    lstEntity = objBal.GetServices("30B24352-9F4B-485A-AB18-528E74F6F260");
                }

                foreach (ENT.ServiceMasterView s in lstEntity)
                {
                    s.serviceimage = "http://dmt.appsmith.co.in/Uploads/Services/postpaid.png";
                }

                GlobalVarible.AddMessage("Services get successfully.");
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult, lstEntity });
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
                ERRORREPORTING.Report(ex, _REQUESTURL, _LOGINUSERID, _ERRORKEY, string.Empty);
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
            }
        }

        /// <summary>
        /// This api is used to get available operators for given service
        /// </summary>
        /// <returns>List of all available operators.</returns>
        /// <remarks>Get all operator from database whose status is active and mapped with given service.</remarks>
        [HttpPost]
        [Authorize]
        [ActionName("GetOperators")]
        [Route("api/Recharge/GetOperators")]
        public HttpResponseMessage GetOperators(GetOperatorModel model)
        {
            GlobalVarible.Clear();
            List<ENT.OperatorView> lstEntity = new List<ENT.OperatorView>();
            try
            {
                using (BAL.OperatorSetup objBal = new BAL.OperatorSetup())
                {
                    lstEntity = objBal.GetOperators("IN", model.serviceid);
                }

                foreach (ENT.OperatorView o in lstEntity)
                {
                    if (o.servicename == "DTH")
                    {
                        o.auth_maxlength = 16;
                        o.auth_maxlength2 = 0;
                    }
                    else if (o.servicename == "PostPaid" || o.servicename == "Prepaid")
                    {
                        o.auth_maxlength = 10;
                        o.auth_maxlength2 = 0;
                    }
                    else
                    {
                        o.auth_maxlength = 30;
                        o.auth_maxlength2 = 50;
                    }
                }

                GlobalVarible.AddMessage("Operators get successfully.");
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult, lstEntity });
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
                ERRORREPORTING.Report(ex, _REQUESTURL, _LOGINUSERID, _ERRORKEY, string.Empty);
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
            }
        }

        /// <summary>
        /// This api is used get recharge
        /// </summary>
        /// <returns>status of recharge based on reply from recharge service provider.</returns>
        /// <remarks>This api is code is changed based on the third party api provider for each api.</remarks>
        //[HttpPost]
        //[Authorize]
        //[ActionName("DoRecharge")]
        //[Route("api/Recharge/DoRecharge")]
        //public HttpResponseMessage DoRecharge(ENT.Recharge recharge)
        //{
        //    GlobalVarible.Clear();

        //    try
        //    {
        //        recharge.reqvia = "GPRS";
        //        recharge.countrycode = "IN";
        //        recharge.accountref = DateTime.Now.Ticks.ToString();
        //        recharge.userid = _LOGINUSERID;
        //        recharge.ipaddress = HttpContext.Current.Request.UserHostAddress;

        //        // this will be provided after recharge

        //        recharge.requestid = string.Empty;

        //        COM.MEMBERS.SQLReturnMessageNValue response = new COM.MEMBERS.SQLReturnMessageNValue();

        //        using (BAL.Recharge objBal = new BAL.Recharge())
        //        {
        //            response = objBal.DoRecharge(recharge, _LOGINUSERID, recharge.ipaddress);
        //        }

        //        if (response.Outval == 1)
        //        {
        //            string[] REQ = response.Outmsg.Split(';');

        //            // process for real recharge here.

        //            GlobalVarible.AddMessage(REQ[0].Split(',')[0]);
        //        }
        //        else
        //        {
        //            GlobalVarible.AddError(response.Outmsg);
        //        }
        //        return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
        //    }
        //    catch (Exception ex)
        //    {
        //        ERRORREPORTING.Report(ex, _REQUESTURL, _LOGINUSERID, _ERRORKEY, new JavaScriptSerializer().Serialize(recharge));
        //        GlobalVarible.AddError(ex.Message);
        //        return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
        //    }
        //}

        /// <summary>
        /// This api is used get recharge
        /// </summary>
        /// <returns>status of recharge based on reply from recharge service provider.</returns>
        /// <remarks>This api is code is changed based on the third party api provider for each api.</remarks>
        [HttpPost]
        [Authorize]
        [ActionName("DoRechargeEnc")]
        [Route("api/Recharge/DoRechargeEnc")]
        public HttpResponseMessage DoRechargeEnc(doRechargeModel data)
        {
            try
            {
                string token = _LOGINUSERID.ToString();

                if (String.IsNullOrEmpty(token))
                {
                    GlobalVarible.AddError("Unable to get token");
                    ERRORREPORTING.Report(new Exception("UNABLE TO GET TOKEN"), _REQUESTURL, _LOGINUSERID, _ERRORKEY, new JavaScriptSerializer().Serialize(data));
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
                }

                string reqdata = ENCDESC.Decrypt(data.data, token, data.userid);

                ENT.Recharge recharge = new ENT.Recharge();

                recharge = new JavaScriptSerializer().Deserialize<ENT.Recharge>(reqdata);

                try
                {
                    ERRORREPORTING.Report(new Exception("API CALL STARTED"), _REQUESTURL, _LOGINUSERID, _ERRORKEY, reqdata);

                    recharge.reqvia = "GPRS";
                    recharge.countrycode = "IN";
                    recharge.accountref = DateTime.Now.Ticks.ToString();
                    recharge.userid = _LOGINUSERID;
                    recharge.ipaddress = HttpContext.Current.Request.UserHostAddress;

                    // this will be provided after recharge

                    recharge.requestid = string.Empty;

                    COM.MEMBERS.SQLReturnMessageNValue response = new COM.MEMBERS.SQLReturnMessageNValue();

                    using (BAL.Recharge objBal = new BAL.Recharge())
                    {
                        response = objBal.DoRecharge(recharge, _LOGINUSERID, recharge.ipaddress);
                    }

                    if (response.Outval == 1)
                    {
                        string[] REQ = response.Outmsg.Split(';');

                        // process for real recharge here.

                        GlobalVarible.AddMessage(REQ[0].Split(',')[0]);
                    }
                    else
                    {
                        GlobalVarible.AddError(response.Outmsg);
                    }
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
                }
                catch (Exception ex)
                {
                    ERRORREPORTING.Report(ex, _REQUESTURL, _LOGINUSERID, _ERRORKEY, new JavaScriptSerializer().Serialize(recharge));
                    GlobalVarible.AddError(ex.Message);
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
                }
            }
            catch (Exception ex)
            {
                ERRORREPORTING.Report(ex, _REQUESTURL, _LOGINUSERID, _ERRORKEY, new JavaScriptSerializer().Serialize(data));
                GlobalVarible.AddError(ex.Message);
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
            }
        }
    }
}
