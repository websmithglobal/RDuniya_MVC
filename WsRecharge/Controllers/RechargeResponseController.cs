using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL = Web.Framework.BusinessLayer;
using DAL = Web.Framework.DataLayer;
using COM = Web.Framework.Common;
using ENT = Web.Framework.Entity;


namespace WsRecharge.Controllers
{
    public class RechargeResponseController : BaseController
    {
        BAL.Recharge.OurResponseClass obj = new BAL.Recharge.OurResponseClass();

        #region response example
        // ID=SNTL&TNO=114252562495986&ST=1&STMSG=Success&TID=10696655&OPRTID=1177914592&Mobile=9157575687&Amount=199&PRB=72660.51&POB=72461.51&DP=4&DR=7.96
        #endregion

        [HttpPost]
        public JsonResult SNTL(string ST, string OPRTID, string TNO)
        {
            // add log or response here.
            new BAL.Log().Insert(new ENT.Log()
            {
                logdata = Request.QueryString.ToString(),
                CreatedDateTime = DateTime.Now,
                type = COM.MyEnumration.LOGTYPE.OUTRES,
                title = "OUT URL - SNTL"
            });

            if (ST != "5" && ST != "2" && ST != "3" && ST != "1")
            {
                return Json("Status returned is : " + ST);
            }
            else if (ST == "1")
            {
                obj.status = 4;
            }
            else if (ST == "5" || ST == "2" || ST == "3")
            {
                obj.status = 3;
            }
            else
            {
                return Json("1.Status returned is : " + ST);
            }
            new BAL.Recharge().UpdateRecharge(TNO, OPRTID, obj.status, Request.QueryString.ToString());
            return Json("SUCCESS");
        }

        [HttpPost]
        public JsonResult INOUT()
        {
            // add log or response here.
            new BAL.Log().Insert(new ENT.Log()
            {
                logdata = Request.QueryString.ToString(),
                CreatedDateTime = DateTime.Now,
                type = COM.MyEnumration.LOGTYPE.OUTRES,
                title = "OUT URL - INOUT"
            });

            if (Request.QueryString.Count < 3 || Request.QueryString.Count > 3)
            {
                return Json("FAILED : INVALID PARAMETERS");
            }

            string TransactionId = Request.QueryString[0];
            string AccountId = Request.QueryString[1];
            string Status = Request.QueryString[2];

            int status = 0;

            if (Status.ToLower() == "s") { status = 4; }
            if (Status.ToLower() == "f") { status = 3; }

            new BAL.Recharge().UpdateRecharge(AccountId,TransactionId,status, Request.QueryString.ToString());

            return Json("SUCCESS");
        }
    }
}