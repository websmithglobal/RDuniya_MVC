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
    public class FundTransferController : BaseController
    {
        ENT.UserProfile Model;
        BAL.UserProfile objBAL = new BAL.UserProfile();
        MDL.FundTransfer EntityValidation = new MDL.FundTransfer();
        List<string> strvalidationResult = new List<string>();
        COM.DBHelper sqlhelper = new COM.DBHelper();
        // GET: FundTransfer
        public ActionResult Index()
        {
            if (User.IsInRole("administrator"))
            {
                ViewBag.Users = new BAL.UserProfile().GetMdApiUsers();
            }
            else if (User.IsInRole("masterdistributor"))
            {
                ViewBag.Users = new BAL.UserProfile().GetDistributors();
            }
            else if (User.IsInRole("distributor"))
            {
                ViewBag.Users = new BAL.UserProfile().GetRetailers();
            }
            return View();
        }

        [HttpGet]
        public JsonResult SearchCustomer(string id)
        {
            bool EStatu = true;
            if (id != null)
            {
                Model = new Web.Framework.Entity.UserProfile();
                Model = (ENT.UserProfile)objBAL.SearchData(id);
                if (Model == null)
                {
                    EStatu = false;
                }
            }
            return Json(new { EntryStatus = EStatu, Model = Model }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveEntry(MDL.FundTransfer model)
        {
            GlobalVarible.Clear();
            try
            {
                strvalidationResult = ValidationEntry(model);
                if (strvalidationResult.Count() == 0)
                {
                    if (checkpassword(model.up_password))
                    {
                        model.up_upperid = Guid.Parse(User.Identity.GetUserId());
                        string[,] param = { { "PARENTID", model.up_upperid.ToString() }, { "USERID", model.up_userid.ToString() }, { "BALTOADD", model.up_fundbalance }, { "TRANSMODE", model.up_mode }, { "REMARKS", model.up_remakrs }, };
                        COM.DBHelper.SQLReturnValue M = sqlhelper.ExecuteProcWithMessageKMT("ADD_FUND_TO_ACCOUNT", param, true);
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

        private List<string> ValidationEntry(object obj)
        {
            strvalidationResult.Clear();
            EntityValidation = (MDL.FundTransfer)obj;
            if (string.IsNullOrWhiteSpace(EntityValidation.up_fundbalance)) { strvalidationResult.Add("Amount is required!"); }
            if (string.IsNullOrWhiteSpace(EntityValidation.up_password)) { strvalidationResult.Add("Password is required!"); }
            return strvalidationResult;
        }
    }
}
