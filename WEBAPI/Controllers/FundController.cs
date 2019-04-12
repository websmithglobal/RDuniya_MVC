using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ENT = Web.Framework.Entity;
using BAL = Web.Framework.BusinessLayer;
using DAL = Web.Framework.DataLayer;
using COM = Web.Framework.Common;
using Microsoft.AspNet.Identity;
using System.Web.Script.Serialization;
using System.Net.Http;
using MDL = WEBAPI.Models;
using System.Net;
using WEBAPI.Models;

namespace WEBAPI.Controllers
{
    public class FundController : BaseApiController
    {
        /// <summary>
        /// This api is used get wallte request for particular user.
        /// </summary>
        /// <returns>return general recharge response object .</returns>
        /// <remarks></remarks>
        [HttpPost]
        [Authorize]
        [ActionName("doWalletRequest")]
        public HttpResponseMessage doWalletRequest(MDL.WalletRequest model)
        {
            GlobalVarible.Clear();
            List<string> strvalidationResult = new List<string>();
            try
            {
                COM.DBHelper sqlhelper = new COM.DBHelper();
                strvalidationResult = ValidateWalletRequest(model);
                if (strvalidationResult.Count() == 0)
                {
                    model.userid = Guid.Parse(User.Identity.GetUserId());

                    string[,] param = { { "USERID", model.userid.ToString() }, { "AMOUNT", model.wr_amount }, { "TRANSMODE", model.wr_mode.ToString() },
                        { "ACCOUNTNO", model.wr_accountno }, { "BANKNAME", model.wr_bankname },{ "REFID", model.wr_refrenceid },{ "REMARKS", model.wr_remakrs },
                        { "USERLEVEL", ((int)User.GetUserlevel()).ToString() }};
                    COM.DBHelper.SQLReturnValue M = sqlhelper.ExecuteProcWithMessageKMT("WALLET_REQUEST", param, true);
                    if (M.ValueFromSQL == 1)
                    {
                        GlobalVarible.AddMessage(M.MessageFromSQL);
                    }
                    else
                    {
                        throw new Exception(string.Join("<br />", M.MessageFromSQL));
                    }
                }
                else
                {
                    throw new Exception(string.Join("<br />", strvalidationResult));
                }
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
                ERRORREPORTING.Report(ex, _REQUESTURL, _LOGINUSERID, _ERRORKEY, new JavaScriptSerializer().Serialize(model));
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
            }
        }

        /// <summary>
        /// This api is used to do balance transfer
        /// </summary>
        /// <param name="data">encrypted request model</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ActionName("doTransfer")]
        public HttpResponseMessage doTransfer(doFundTransferModel data)
        {
            COM.DBHelper sqlhelper = new COM.DBHelper();
            GlobalVarible.Clear();
            List<string> strvalidationResult = new List<string>();
            try
            {
                string token = _LOGINUSERID.ToString();

                string reqdata = ENCDESC.Decrypt(data.data, token, data.userid);

                MDL.FundTransfer model = new FundTransfer();

                model = new JavaScriptSerializer().Deserialize<MDL.FundTransfer>(reqdata);

                strvalidationResult = ValidateFundTransfer(model);
                if (strvalidationResult.Count() == 0)
                {
                    if (checkpassword(model.up_password))
                    {
                        model.up_upperid = Guid.Parse(User.Identity.GetUserId());

                        string[,] param = {
                            { "PARENTID", model.up_upperid.ToString() },
                            { "USERID", model.up_userid.ToString() },
                            { "BALTOADD", model.up_fundbalance },
                            { "TRANSMODE", model.up_mode },
                            { "REMARKS", model.up_remakrs },
                        };

                        COM.DBHelper.SQLReturnValue M = sqlhelper.ExecuteProcWithMessageKMT("ADD_FUND_TO_ACCOUNT", param, true);

                        if (M.ValueFromSQL == 1)
                        {
                            // get detail user device token

                            using (BAL.UserDeviceToken objUDT = new BAL.UserDeviceToken())
                            {
                                ENT.UserDeviceToken usertoken = objUDT.GetUserDeviceToken(model.up_userid);

                                if (usertoken != null)
                                {
                                    string message = "Your account is credited with with "+ model.up_fundbalance.ToString();
                                    FCMNOTIFICATION.SendPushNotification(usertoken.udt_devicetoken,message, model.up_userid, 1);
                                }
                            }

                            GlobalVarible.AddMessage(M.MessageFromSQL);
                        }
                        else
                        {
                            throw new Exception(string.Join("<br />", M.MessageFromSQL));
                        }
                    }
                    else { throw new Exception(string.Join("<br />", "Invalid Password !")); }
                }
                else
                {
                    throw new Exception(string.Join("<br />", strvalidationResult));
                }

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
                ERRORREPORTING.Report(ex, _REQUESTURL, _LOGINUSERID, _ERRORKEY, string.Empty);
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
            }
        }


        /// <summary>
        /// This api is used to get pending approval requests.
        /// </summary>
        /// <returns>List of WalletRequestApiView for given user.</returns>
        /// <remarks>Get all services list booked by particular user.</remarks>
        [HttpPost]
        [Authorize]
        [ActionName("PendingApprovalRequest")]
        public HttpResponseMessage PendingApprovalRequest(GetPendingWalletRequestModel modal)
        {
            GlobalVarible.Clear();
            List<ENT.WalletRequestApiView> lstEntity = new List<ENT.WalletRequestApiView>();
            try
            {
                //Find paging info
                var start = modal.PageStart;
                var length = modal.PageSize;

                int pageSize = length != 0 ? length : 0;
                int skip = start != 0 ? start : 1;
                skip = (skip / pageSize) + 1;

                COM.TTPagination.isPageing = true;
                COM.TTPagination.PageSize = pageSize;
                COM.TTPagination.PageNo = Convert.ToInt64(skip);

                using (BAL.WalletRequest objBal = new BAL.WalletRequest())
                {
                    // lstEntity = new BAL.WalletRequest().GetAllSearch(ddrange, fromdate.GetDate(), todate.GetDate(), ddData, User.GetUserlevel(), Guid.Parse(User.Identity.GetUserId()));

                    if (modal.ddrange == 1)
                    {
                        lstEntity = objBal.GetAllSearchApi(modal.ddrange, modal.fromdate.GetDate(), modal.todate.GetDate(), modal.ddData, User.GetUserlevel(), _LOGINUSERID);
                    }
                    else
                    {
                        lstEntity = objBal.GetAllSearchApi(modal.ddrange, DateTime.Now, DateTime.Now, modal.ddData, User.GetUserlevel(), _LOGINUSERID);
                    }
                }

                GlobalVarible.AddMessage("Approval Pending get successfully.");
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult, lstEntity, COM.TTPagination.RecordCount });
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
                ERRORREPORTING.Report(ex, _REQUESTURL, _LOGINUSERID, _ERRORKEY, string.Empty);
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
            }
        }

        /// <summary>
        /// This api is used to do balance transfer
        /// </summary>
        /// <param name="data">encrypted request model</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ActionName("approveRequest")]
        public HttpResponseMessage approveRequest(approveRequest data)
        {
            COM.DBHelper sqlhelper = new COM.DBHelper();
            GlobalVarible.Clear();
            List<string> strvalidationResult = new List<string>();
            try
            {
                string token = _LOGINUSERID.ToString();

                string reqdata = ENCDESC.Decrypt(data.data, token, data.userid);

                MDL.ApproveRequest model = new ApproveRequest();

                model = new JavaScriptSerializer().Deserialize<MDL.ApproveRequest>(reqdata);

                strvalidationResult = ValidateApproveRequest(model);
                if (strvalidationResult.Count() == 0)
                {
                    if (checkpassword(model.loginpassword))
                    {
                        Guid wrid = new Guid(model.wr_id);
                        Guid userid = Guid.Parse(User.Identity.GetUserId());

                        string[,] param = { { "WR_ID", wrid.ToString() }, { "UPPERID", userid.ToString() } };

                        COM.DBHelper.SQLReturnValue M = sqlhelper.ExecuteProcWithMessageKMT("WALLET_APPROVED", param, true);

                        if (M.ValueFromSQL == 1)
                        {
                            // get detail user device token

                            using (BAL.WalletRequest objUDT = new BAL.WalletRequest())
                            {
                                ENT.WalletRequest request = objUDT.GetRequestById(Guid.Parse(model.wr_id));

                                if (request != null)
                                {
                                    string message = "Your account is credited with with " + request.wr_amount.ToString();
                                    FCMNOTIFICATION.SendPushNotification(request.udt_devicetoken, message, request.wr_userid, 1);
                                }
                            }
                            GlobalVarible.AddMessage(M.MessageFromSQL);
                        }
                        else
                        {
                            throw new Exception(string.Join("<br />", M.MessageFromSQL));
                        }
                    }
                    else { throw new Exception(string.Join("<br />", "Invalid Password !")); }
                }
                else
                {
                    throw new Exception(string.Join("<br />", strvalidationResult));
                }

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
                ERRORREPORTING.Report(ex, _REQUESTURL, _LOGINUSERID, _ERRORKEY, string.Empty);
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
            }
        }

        #region internalmethods
        private List<string> ValidateWalletRequest(object obj)
        {
            List<string> strvalidationResult = new List<string>();

            MDL.WalletRequest EntityValidation = new MDL.WalletRequest();

            EntityValidation = (MDL.WalletRequest)obj;
            if (string.IsNullOrWhiteSpace(EntityValidation.wr_bankname)) { strvalidationResult.Add("Bank Name is required!"); }
            if (string.IsNullOrWhiteSpace(EntityValidation.wr_accountno)) { strvalidationResult.Add("Account No is required!"); }
            if (string.IsNullOrWhiteSpace(EntityValidation.wr_amount)) { strvalidationResult.Add("Amount is required!"); }
            if (string.IsNullOrWhiteSpace(EntityValidation.wr_refrenceid)) { strvalidationResult.Add("Refrence id is required!"); }
            return strvalidationResult;
        }

        private List<string> ValidateFundTransfer(object obj)
        {
            List<string> strvalidationResult = new List<string>();

            MDL.FundTransfer EntityValidation = new MDL.FundTransfer();

            EntityValidation = (MDL.FundTransfer)obj;
            if (string.IsNullOrWhiteSpace(EntityValidation.up_fundbalance)) { strvalidationResult.Add("Amount is required!"); }
            if (string.IsNullOrWhiteSpace(EntityValidation.up_password)) { strvalidationResult.Add("Password is required!"); }
            return strvalidationResult;
        }

        private List<string> ValidateApproveRequest(object obj)
        {
            List<string> strvalidationResult = new List<string>();

            MDL.ApproveRequest EntityValidation = new MDL.ApproveRequest();

            EntityValidation = (MDL.ApproveRequest)obj;
            if (string.IsNullOrWhiteSpace(EntityValidation.wr_id)) { strvalidationResult.Add("Please provide proper request id."); }
            if (string.IsNullOrWhiteSpace(EntityValidation.loginpassword)) { strvalidationResult.Add("Password is required!"); }
            return strvalidationResult;
        }
        #endregion
    }
}
