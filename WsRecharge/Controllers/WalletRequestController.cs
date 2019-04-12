using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ENT = Web.Framework.Entity;
using BAL = Web.Framework.BusinessLayer;
using DAL = Web.Framework.DataLayer;
using COM = Web.Framework.Common;
using MDL = WsRecharge.Models;
using Microsoft.AspNet.Identity;


namespace WsRecharge.Controllers
{
    [Authorize]
    public class WalletRequestController : BaseController
    {
        ENT.WalletRequest Model;
        BAL.WalletRequest objBAL = new BAL.WalletRequest();
        MDL.WalletRequest EntityValidation = new MDL.WalletRequest();
        MDL.ApprovedRequest EntityValidation2 = new MDL.ApprovedRequest();
        List<string> strvalidationResult = new List<string>();
        COM.DBHelper sqlhelper = new COM.DBHelper();

        // GET: WalletRequest
        public ActionResult Index()
        {
            return View();
        }
        private List<string> ValidationEntry(object obj)
        {
            strvalidationResult.Clear();
            EntityValidation = (MDL.WalletRequest)obj;
            if (string.IsNullOrWhiteSpace(EntityValidation.wr_bankname)) { strvalidationResult.Add("Bank Name is required!"); }
            if (string.IsNullOrWhiteSpace(EntityValidation.wr_accountno)) { strvalidationResult.Add("Account No is required!"); }
            if (string.IsNullOrWhiteSpace(EntityValidation.wr_amount)) { strvalidationResult.Add("Amount is required!"); }
            if (string.IsNullOrWhiteSpace(EntityValidation.wr_refrenceid)) { strvalidationResult.Add("Refrence id is required!"); }
            return strvalidationResult;

        }
        private List<string> ValidationEntry2(object obj)
        {
            strvalidationResult.Clear();
            EntityValidation2 = (MDL.ApprovedRequest)obj;
            if (string.IsNullOrWhiteSpace(EntityValidation2.LoginPwd)) { strvalidationResult.Add("Login Password is required!"); }
            return strvalidationResult;

        }
        [HttpPost]
        [Authorize]
        public JsonResult SaveEntry(MDL.WalletRequest model)
        {
            GlobalVarible.Clear();
            try
            {
                strvalidationResult = ValidationEntry(model);
                if (strvalidationResult.Count() == 0)
                {
                    model.userid = Guid.Parse(User.Identity.GetUserId());

                    if (String.IsNullOrEmpty(model.wr_remakrs)) {
                        model.wr_remakrs = string.Empty;
                    }

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
                else { throw new Exception(string.Join("<br />", strvalidationResult)); }
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
            }
            MySession.Current.MessageResult.MessageHtml = GlobalVarible.GetMessageHTML();
            return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
        }

        #region Approved Wallet
        public ActionResult Approved()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetAll(int ddrange, string fromdate, string todate, int ddData)
        {
            List<ENT.WalletRequest> lstEntity = new List<ENT.WalletRequest>();
            //jQuery DataTables Param
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            //Find paging info
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            //Find order columns info
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][data]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt16(start) : 1;
            skip = (skip / pageSize) + 1;
            COM.TTPagination.isPageing = true;
            COM.TTPagination.PageSize = pageSize;
            COM.TTPagination.PageNo = Convert.ToInt64(skip);
            lstEntity = new BAL.WalletRequest().GetAllSearch(ddrange, fromdate.GetDate(), todate.GetDate(), ddData, User.GetUserlevel(), Guid.Parse(User.Identity.GetUserId()));
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
        public JsonResult Approved(MDL.ApprovedRequest model)
        {
            GlobalVarible.Clear();
            try
            {
                strvalidationResult = ValidationEntry2(model);
                if (strvalidationResult.Count() == 0)
                {
                    if (checkpassword(model.LoginPwd))
                    {
                        Guid wrid = new Guid(model.wr_id.Replace("/", ""));
                        Guid userid = Guid.Parse(User.Identity.GetUserId());
                        string[,] param = { { "WR_ID", wrid.ToString() }, { "UPPERID", userid.ToString() } };
                        COM.DBHelper.SQLReturnValue M = sqlhelper.ExecuteProcWithMessageKMT("WALLET_APPROVED", param, true);
                        if (M.ValueFromSQL == 1)
                        {
                            GlobalVarible.AddMessage(M.MessageFromSQL);
                        }
                        else
                        {
                            throw new Exception(string.Join("<br />", M.MessageFromSQL));
                        }
                    }
                    else { throw new Exception(string.Join("<br />", "Invalid Password !")); }
                }
                else { throw new Exception(string.Join("<br />", strvalidationResult)); }
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
            }
            MySession.Current.MessageResult.MessageHtml = GlobalVarible.GetMessageHTML();
            return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}