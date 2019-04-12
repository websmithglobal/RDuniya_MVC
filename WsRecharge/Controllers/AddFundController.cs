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
using WsRecharge.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WsRecharge.Controllers
{
    [Authorize]
    public class AddFundController : BaseController
    {
        MDL.AddFundTransfer EntityValidation = new MDL.AddFundTransfer();
        List<string> strvalidationResult = new List<string>();
        COM.DBHelper sqlhelper = new COM.DBHelper();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult SaveEntry(MDL.AddFundTransfer model)
        {
            GlobalVarible.Clear();
            try
            {
                strvalidationResult = ValidationEntry(model);
                if (strvalidationResult.Count() == 0)
                {
                    if (checkpassword(model.up_password))
                    {
                        string[,] param = { { "USERID", Guid.Parse(User.Identity.GetUserId()).ToString() }, { "BALTOADD", model.up_fundbalance }, { "TRANSMODE", "WEB" }, };
                        COM.DBHelper.SQLReturnValue M = sqlhelper.ExecuteProcWithMessageKMT("ADD_FUND_SUPER", param, true);
                        if (M.ValueFromSQL == 1)
                        {
                            GlobalVarible.AddMessage(M.MessageFromSQL);
                        }
                        else
                        {
                            throw new Exception(string.Join("<br />", M.MessageFromSQL));
                        }
                    }
                    else { throw new Exception(string.Join("<br />", "Invalid Password!")); }
                }
                else
                {
                    throw new Exception(string.Join("<br />", strvalidationResult));
                }
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
            EntityValidation = (MDL.AddFundTransfer)obj;
            if (string.IsNullOrWhiteSpace(EntityValidation.up_fundbalance)) { strvalidationResult.Add("Amount is required!"); }
            if (string.IsNullOrWhiteSpace(EntityValidation.up_password)) { strvalidationResult.Add("Password is required!"); }
            if (EntityValidation.up_userid.ToString() == "00000000-0000-0000-0000-000000000000") { strvalidationResult.Add("please select a Account!"); }
            return strvalidationResult;
        }
    }
}