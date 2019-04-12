using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ENT = Web.Framework.Entity;
using BAL = Web.Framework.BusinessLayer;
using COM = Web.Framework.Common;
using Microsoft.AspNet.Identity;
using WEBRECHARGE;
using WEBRECHARGE.ENTITIES;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using WsRecharge.Models;
using RECHARGEDUNIYA.SRService;

namespace WsRecharge.Controllers
{
    /// <summary>
    /// <summary>
    /// This controller is used to process all kind of recharge.
    /// All required functions for the recharge is performed here.
    /// </summary>
    public class RechargeController : BaseController
    {
        // GET: Recharge
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get Service list which for do recharge.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public JsonResult GetService()
        {
            List<ENT.ServiceMaster> lstEntity = new List<ENT.ServiceMaster>();
            GlobalVarible.Clear();
            try
            {
                using (BAL.ServiceMaster objBal = new BAL.ServiceMaster())
                {
                    lstEntity = objBal.GetService("30B24352-9F4B-485A-AB18-528E74F6F260");
                }
                GlobalVarible.AddMessage("Services get successfully.");
                return Json(new { MySession.Current.MessageResult, lstEntity }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
                return Json(new { MySession.Current.MessageResult, lstEntity }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Get list of operators for which we have to do recharge
        /// </summary>
        /// <returns></returns>

        [Authorize]
        [HttpGet]
        public JsonResult GetOperators(string id)
        {
            List<ENT.OperatorSetup> lstEntity = new List<ENT.OperatorSetup>();
            GlobalVarible.Clear();
            try
            {
                using (BAL.OperatorSetup objBal = new BAL.OperatorSetup())
                {
                    lstEntity = objBal.GetOperatorByService(id, "30B24352-9F4B-485A-AB18-528E74F6F260");
                }
                GlobalVarible.AddMessage("Services get successfully.");
                return Json(new { MySession.Current.MessageResult, lstEntity }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
                return Json(new { MySession.Current.MessageResult, lstEntity }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Get list of circle of selected operators
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public JsonResult GetCircles(string id)
        {
            List<ENT.CircleMaster> lstEntity = new List<ENT.CircleMaster>();
            GlobalVarible.Clear();
            try
            {
                using (BAL.CircleMaster objBal = new BAL.CircleMaster())
                {
                    lstEntity = objBal.GetAllActive("IN");
                }
                GlobalVarible.AddMessage("Circles get successfully.");
                return Json(new { MySession.Current.MessageResult, lstEntity }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
                return Json(new { MySession.Current.MessageResult, lstEntity }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Get list of circle of selected operators
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public JsonResult doRecharge(ENT.Recharge recharge)
        {
            GlobalVarible.Clear();
            try
            {
                recharge.reqvia = "WEB";
                recharge.countrycode = "IN";
                recharge.accountref = DateTime.Now.Ticks.ToString();
                recharge.userid = _LoginUserId;
                recharge.ipaddress = Request.UserHostAddress.ToString();

                // this will be provided after recharge

                recharge.requestid = string.Empty;

                COM.MEMBERS.SQLReturnMessageNValue response = new COM.MEMBERS.SQLReturnMessageNValue();

                using (BAL.Recharge objBal = new BAL.Recharge())
                {
                    response = objBal.DoRecharge(recharge, _LoginUserId, recharge.ipaddress);
                }

                string LocalID = string.Empty;

                if (response.Outval == 1)
                {
                    // manage route here to call real api for process recharge.
                    string[] REQ = response.Outmsg.Split(';');

                    // add log or response here.
                    new BAL.Log().Insert(new ENT.Log()
                    {
                        logdata = REQ[1],
                        CreatedDateTime = DateTime.Now,
                        type = COM.MyEnumration.LOGTYPE.DATA,
                        title = "PORTAL-REQUEST"
                    });

                    #region WEBSMITHAPI
                    if (REQ[0].Split(':')[2].StartsWith("W"))
                    {
                        LocalID = REQ[0].Split(':')[2].StartsWith("W") ? REQ[0].Split(':')[2].Remove(0, 1) : REQ[0].Split(':')[2];
                        ProcessRecharge(recharge.numbertorecharge, recharge.operatorcode, (int)recharge.amount, LocalID, recharge.reqtype);
                    }
                    else if (REQ[0].Split(':')[2].StartsWith("X"))
                    {
                        LocalID = REQ[0].Split(':')[2].StartsWith("X") ? REQ[0].Split(':')[2].Remove(0, 2) : REQ[0].Split(':')[2];

                        ProcessINOUT(REQ[1], LocalID);
                    }
                    #endregion

                    GlobalVarible.AddMessage(REQ[0].Split(',')[0]);
                }
                else
                {
                    GlobalVarible.AddError(response.Outmsg);
                }
                return Json(MySession.Current.MessageResult);
            }
            catch (Exception ex)
            {
                ERRORREPORTING.Report(ex, Request.Url.ToString(), _LoginUserId, _ERRORKEY, new JavaScriptSerializer().Serialize(recharge));
                GlobalVarible.AddError(ex.Message);
                return Json(MySession.Current.MessageResult);
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult getPendingRecharge()
        {
            List<ENT.Recharge> lstEntity = new List<ENT.Recharge>();
            BAL.Recharge objBAL = new BAL.Recharge();
            //jQuery DataTables Param
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            //Find paging info
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            //Find order columns info

            var search = Request.Form.Get("search[value]");

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt16(start) : 1;
            skip = (skip / pageSize) + 1;
            COM.TTPagination.isPageing = true;
            COM.TTPagination.PageSize = pageSize;
            COM.TTPagination.PageNo = Convert.ToInt64(skip);

            if (User.IsInRole("administrator"))
            {
                lstEntity = objBAL.GetPendingListAdmin(search.ToString());
            }
            else {
                lstEntity = objBAL.GetPending(_LoginUserId, search.ToString());
            }
            
            return Json(new
            {
                draw = draw,
                recordsTotal = lstEntity.Count(),
                recordsFiltered = COM.TTPagination.RecordCount,
                data = lstEntity
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult UpdateStatus(string transid, int status, string accountid)
        {
            GlobalVarible.Clear();
            try
            {
                COM.MEMBERS.SQLReturnMessageNValue mUpdate = new BAL.Recharge().UpdateRecharge(
                    accountid,
                    !string.IsNullOrEmpty(transid) ? transid : "N/A",
                    status,
                    "Manually by : " + User.Identity.GetUserName());

                if (mUpdate.Outval > 0)
                {
                    GlobalVarible.AddMessage(mUpdate.Outmsg);
                }
                else
                {
                    GlobalVarible.AddError(mUpdate.Outmsg);
                }
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
                ERRORREPORTING.Report(ex, Request.Url.ToString(), _LoginUserId, _ERRORKEY, (transid + "|" + status + "|" + accountid));
            }
            MySession.Current.MessageResult.MessageHtml = GlobalVarible.GetMessageHTML();
            return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Test(String id)
        {
            RECHARGE WC = new RECHARGE("advancetronics.nanpura@yahoo.com", "Web@123", "http://dmtsrv.appsmith.co.in/");
            object postdata = new
            {
                country_code = "IN",
                operator_code = 1,
                number = id,
                amount = 10,
                requestid = "C1B78C983B0E46A",
                reqvia = "API",
                recharge_type = 0,
                optionalparam = string.Empty,
                optionalparam1 = string.Empty,
            };

            RECHARGERESPONSE RR = WC.ProcessRecharge(postdata, "Recharge");

            if (RR.results.Count > 0)
            {
                // getting first item from recharge result object which got from websmith api
                RECHARGERESPONSERESULT _result = RR.results[0];
                if (_result.status_code == 3)
                {
                    // update fail code here to neej recharge database.
                }
            }

            return View();
        }

        #region process recharge from websmith api
        internal void ProcessRecharge(string _number, int _operatorcode, int _amounttorecharge, string _requestid, int _rechargetype)
        {
            object postdata = new
            {
                country_code = "IN",
                operator_code = _operatorcode,
                number = _number,
                amount = _amounttorecharge,
                requestid = _requestid,
                reqvia = "API",
                recharge_type = _rechargetype,
                optionalparam = string.Empty,
                optionalparam1 = string.Empty,
            };

            try
            {
                RECHARGE WC = new RECHARGE("gosrani_enterprises@hotmail.com", "Bansi@123", "http://dmtsrv.appsmith.co.in/");

                // add log or response here.
                new BAL.Log().Insert(new ENT.Log()
                {
                    logdata = new JavaScriptSerializer().Serialize(postdata),
                    CreatedDateTime = DateTime.Now,
                    type = COM.MyEnumration.LOGTYPE.APIREQ,
                    title = "PROCESS - WEBSMITH API"
                });

                RECHARGERESPONSE RR = WC.ProcessRecharge(postdata, "Recharge");

                // add log or response here.
                new BAL.Log().Insert(new ENT.Log()
                {
                    logdata = new JavaScriptSerializer().Serialize(RR),
                    CreatedDateTime = DateTime.Now,
                    type = COM.MyEnumration.LOGTYPE.APIRES,
                    title = "PROCESS - WEBSMITH API"
                });

                if (RR.results.Count > 0)
                {
                    // getting first item from recharge result object which got from websmith api
                    RECHARGERESPONSERESULT _result = RR.results[0];
                    if (_result.status_code == 3)
                    {
                        // update fail code here to local database.
                        new BAL.Recharge().UpdateRecharge(_requestid, _result.transNo == null ? string.Empty : _result.transNo, 3, new JavaScriptSerializer().Serialize(_result));
                    }
                    else if (_result.status_code == 4)
                    {
                        // update success code here to local database.
                        new BAL.Recharge().UpdateRecharge(
                            _requestid,
                            _result.transNo == null ? string.Empty : _result.transNo,
                            4,
                            new JavaScriptSerializer().Serialize(_result));
                    }
                }
            }
            catch (Exception ex)
            {
                ERRORREPORTING.Report(ex, Request.Url.ToString(), _LoginUserId, _ERRORKEY, new JavaScriptSerializer().Serialize(postdata));
            }
        }
        #endregion

        #region process recharge from INOUT
        internal void ProcessINOUT(String REQ,string _requestid)
        {
            string[] VAL = REQ.Split('|');

            try
            {
                // add log or response here.
                new BAL.Log().Insert(new ENT.Log()
                {
                    logdata = REQ,
                    CreatedDateTime = DateTime.Now,
                    type = COM.MyEnumration.LOGTYPE.DATA,
                    title = "PROCESS - INOUT API"
                });

                SRServiceSoapClient client = new SRServiceSoapClient();

                string X = client.DoRecharge(new Information
                {
                    Amount = Convert.ToDecimal(VAL[1]),
                    ApiCredentials = new Credentials { Password = VAL[5], UserName = VAL[6] },
                    MobileToRecharge = VAL[0],
                    OperatorCode = int.Parse(VAL[2]),
                    ServiceCode = int.Parse(VAL[4]),
                    TypeOfRecharge = Convert.ToByte(VAL[3].ToString()),
                });

                new BAL.Log().Insert(new ENT.Log()
                {
                    logdata = X,
                    CreatedDateTime = DateTime.Now,
                    type = COM.MyEnumration.LOGTYPE.APIRES,
                    title = "PROCESS - INOUT API"
                });

                INOUTRESPONSE Response = JsonConvert.DeserializeObject<INOUTRESPONSE>(X);

                //if (Response.DoRechargeResponse.Code == "SR113")
                //{
                //    new BAL.Recharge().UpdateRecharge(_requestid, Response.DoRechargeResponse.ReferenceID == null ? string.Empty : Response.DoRechargeResponse.ReferenceID, 3, X);
                //}
                // parse json here and get action as per response.
            }
            catch (Exception ex)
            {
                new BAL.Log().Insert(new ENT.Log()
                {
                    logdata = new JavaScriptSerializer().Serialize(ex),
                    CreatedDateTime = DateTime.Now,
                    type = COM.MyEnumration.LOGTYPE.APIERROR,
                    title = "PROCESS - INOUT API"
                });

                ERRORREPORTING.Report(ex, Request.Url.ToString(), _LoginUserId, _ERRORKEY, REQ);
                ex.Message.ToString();
            }
        }
        #endregion
    }
}