﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;
using BAL = Web.Framework.BusinessLayer;
using Microsoft.AspNet.Identity;
using WsRecharge.Models;

namespace WsRecharge.Controllers
{
    [Authorize]
    public class CountryMasterController : BaseController
    {
        List<ENT.CountryMaster> lstEntity = new List<ENT.CountryMaster>();
        BAL.CountryMaster objBAL = new BAL.CountryMaster();
        ENT.CountryMaster Model;
        // GET: CountryMaster
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public JsonResult SaveEntry(ENT.CountryMaster model, string countryid)
        {
            try
            {
                List<Guid> dctDuplication = new List<Guid>();
                if (model.EntryMode == COM.Enumration.EntryMode.ADD)
                {
                    model.name = model.nicename.ToUpper();
                    model.Status = COM.MyEnumration.MyStatus.Active;
                    List<ENT.CountryMaster> lstResult = new BAL.CountryMaster().CheckDuplicateCombination(dctDuplication, model.nicename, "none");
                    if (lstResult.Count > 0)
                    {
                        throw new Exception("Country Name Already Exists.");
                    }
                    if (objBAL.Insert(model))
                    {
                        GlobalVarible.AddMessage("Record Save Successfully");
                    }
                }
                else
                {
                    model.name = model.nicename.ToUpper();
                    model.UpdatedDateTime = DateTime.Now;
                    model.UpdatedBy = Guid.Parse(User.Identity.GetUserId());
                    model.countryid = new Guid(countryid.Replace("/", ""));
                    if (objBAL.UpdatePartial(model))
                    {
                        GlobalVarible.AddMessage("Record Update Successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
            }
            MySession.Current.MessageResult.MessageHtml = GlobalVarible.GetMessageHTML();
            return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult GetAll()
        {
            //jQuery DataTables Param
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            //Find paging info
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            //Find order columns info

            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][data]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var search = Request.Form.Get("search[value]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt16(start) : 1;
            skip = (skip / pageSize) + 1;
            COM.TTPagination.isPageing = true;
            COM.TTPagination.PageSize = pageSize;
            COM.TTPagination.PageNo = Convert.ToInt64(skip);
            lstEntity = objBAL.GetAll(search.ToString().Replace("\0", string.Empty));
            COM.ExtendedMethods.SortList(lstEntity, sortColumn, sortColumnDir);
            return Json(new
            {
                draw = draw,
                recordsTotal = lstEntity.Count(),
                recordsFiltered = COM.TTPagination.RecordCount,
                data = lstEntity
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public JsonResult UpdateStatus(string id)
        {
            GlobalVarible.Clear();
            try
            {
                if (id != null)
                {
                    Model = new Web.Framework.Entity.CountryMaster();
                    Model.countryid = new Guid(id);
                    Model = (ENT.CountryMaster)objBAL.GetByPrimaryKey(Model);
                    if (Model.Status == COM.MyEnumration.MyStatus.Active)
                    {
                        if (!objBAL.UpdateStatus(Model.countryid, COM.MyEnumration.MyStatus.DeActive))
                        {
                            throw new Exception("Internal Server Error in status update.");
                        }
                    }
                    if (Model.Status == COM.MyEnumration.MyStatus.DeActive)
                    {
                        if (!objBAL.UpdateStatus(Model.countryid, COM.MyEnumration.MyStatus.Active))
                        {
                            throw new Exception("Internal Server Error in status update.");
                        }
                    }
                    GlobalVarible.AddMessage("Status Update Successfully.");
                }
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
            }
            MySession.Current.MessageResult.MessageHtml = GlobalVarible.GetMessageHTML();
            return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public JsonResult EditRecord(string id)
        {
            if (id != null)
            {
                Model = new Web.Framework.Entity.CountryMaster();
                Model.UpdatedDateTime = DateTime.Now;
                Model.countryid = new Guid(id);
                Model = (ENT.CountryMaster)objBAL.GetByPrimaryKey(Model);
            }
            return Json(new { Model = Model }, JsonRequestBehavior.AllowGet);
        }
    }
}