using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WEBAPI.Models;
using ENT = Web.Framework.Entity;
using BAL = Web.Framework.BusinessLayer;
using COM = Web.Framework.Common;

namespace WEBAPI.Controllers
{
    public class ReportsController : BaseApiController
    {
        /// <summary>
        /// This api is used to get recharge Report.
        /// </summary>
        /// <returns>List of services which are booked by loggedin user.</returns>
        /// <remarks>Get all services list booked by particular user.</remarks>
        [HttpPost]
        [Authorize]
        [ActionName("RechargeReport")]
        [Route("api/Reports/RechargeReport")]
        public HttpResponseMessage RechargeReport(GetRechargeReportModal modal)
        {
            GlobalVarible.Clear();
            List<ENT.RechargeReportApiView> lstEntity = new List<ENT.RechargeReportApiView>();
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

                using (BAL.RechargeReport objBal = new BAL.RechargeReport())
                {
                    if (modal.ddrange == 1)
                    {
                        if (string.IsNullOrEmpty(modal.fromdate) || string.IsNullOrEmpty(modal.todate)) {

                            GlobalVarible.AddError("For custome date you must have to select form and to date.");
                            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
                        }
                        lstEntity = objBal.GetAllSearchApi(modal.ddrange, modal.fromdate.GetDate(), modal.todate.GetDate(), modal.ddData, modal.search, User.GetUserlevel(), _LOGINUSERID, modal.status, modal.operatorcode);
                    }
                    else {
                        lstEntity = objBal.GetAllSearchApi(modal.ddrange, DateTime.Now, DateTime.Now, modal.ddData, modal.search, User.GetUserlevel(), _LOGINUSERID, modal.status, modal.operatorcode);
                    }
                }

                GlobalVarible.AddMessage("Recharge report get successfully.");
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
        /// This api is used to get ledger Report.
        /// </summary>
        /// <returns>List of ledger items which are booked by loggedin user.</returns>
        /// <remarks>Get all services list booked by particular user.</remarks>
        [HttpPost]
        [Authorize]
        [ActionName("LedgerReport")]
        [Route("api/Reports/LedgerReport")]
        public HttpResponseMessage LedgerReport(GetLedgerReportModal modal)
        {
            GlobalVarible.Clear();
            List<ENT.LedgerReportApiView> lstEntity = new List<ENT.LedgerReportApiView>();
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

                using (BAL.LedgerReport objBal = new BAL.LedgerReport())
                {
                    if (modal.ddrange == 1)
                    {
                        //if (string.IsNullOrEmpty(modal.fromdate) || string.IsNullOrEmpty(modal.todate))
                        //{
                        //    GlobalVarible.AddError("For custome date you must have to select form and to date.");
                        //    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
                        //}
                        lstEntity = objBal.GetAllSearchApi(modal.ddrange, modal.fromdate.GetDate(), modal.todate.GetDate(), modal.ddData, modal.search, User.GetUserlevel(), _LOGINUSERID);
                    }
                    else
                    {
                        lstEntity = objBal.GetAllSearchApi(modal.ddrange, DateTime.Now, DateTime.Now, modal.ddData, modal.search, User.GetUserlevel(), _LOGINUSERID);
                    }
                }

                GlobalVarible.AddMessage("Recharge report get successfully.");
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
        /// This api is used to get ledger Report.
        /// </summary>
        /// <returns>List of ledger items which are booked by loggedin user.</returns>
        /// <remarks>Get all services list booked by particular user.</remarks>
        [HttpPost]
        [Authorize]
        [ActionName("FundTransferReport")]
        [Route("api/Reports/FundTransferReport")]
        public HttpResponseMessage FundTransferReport(GetFundTransferReportModal modal)
        {
            GlobalVarible.Clear();
            List<ENT.FundReportApiView> lstEntity = new List<ENT.FundReportApiView>();
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

                using (BAL.FundReport objBal = new BAL.FundReport())
                {
                    if (modal.ddrange == 1)
                    {
                        lstEntity = objBal.GetAllSearchApi(modal.ddrange, modal.fromdate.GetDate(), modal.todate.GetDate(), modal.ddData, modal.search, User.GetUserlevel(), _LOGINUSERID);
                    }
                    else
                    {
                        lstEntity = objBal.GetAllSearchApi(modal.ddrange, DateTime.Now, DateTime.Now, modal.ddData, modal.search, User.GetUserlevel(), _LOGINUSERID);
                    }
                }

                GlobalVarible.AddMessage("Fund report get successfully.");
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult, lstEntity, COM.TTPagination.RecordCount });
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
