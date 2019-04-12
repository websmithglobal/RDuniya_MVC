using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;
using BAL = Web.Framework.BusinessLayer;
using Microsoft.AspNet.Identity;
using System.Web.UI;

namespace WsRecharge.Controllers
{
    public class RechargeReportController : BaseController
    {
        List<ENT.RechargeReport> lstEntity = new List<ENT.RechargeReport>();
        // GET: RechargeReport
        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Client, VaryByParam = "none", NoStore = true)]
        public ActionResult Index()
        {
            ViewBag.PageHeader = "Recharge Report";
            ViewBag.OperatorMaster = new BAL.OperatorSetup().GetAll(string.Empty);
            return View();
        }
        [HttpPost]
        public JsonResult GetAll(int ddrange, string fromdate, string todate, int ddData, string search, int status, int operatorcode)
        {

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
            lstEntity = new BAL.RechargeReport().GetAllSearch(ddrange, fromdate.GetDate(), todate.GetDate(), ddData, search, User.GetUserlevel(), Guid.Parse(User.Identity.GetUserId()), status, operatorcode);
            
            return Json(new
            {
                draw = draw,
                recordsTotal = lstEntity.Count(),
                recordsFiltered = COM.TTPagination.RecordCount,
                data = lstEntity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}