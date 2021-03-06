﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;
using BAL = Web.Framework.BusinessLayer;
using Microsoft.AspNet.Identity;

namespace WsRecharge.Controllers
{
    [Authorize]
    public class FundReportController : BaseController
    {
        // GET: FundReport
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetAll(int ddrange, string fromdate, string todate, int ddData,int ddUserType, string search)
        {
            List<ENT.FundReport> lstEntity = new List<ENT.FundReport>();
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

            lstEntity = new BAL.FundReport().GetAllSearch(ddrange, fromdate.GetDate(), todate.GetDate(), ddData, ddUserType, search, User.GetUserlevel(), Guid.Parse(User.Identity.GetUserId()));

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