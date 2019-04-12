using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;
using BAL = Web.Framework.BusinessLayer;
using Microsoft.AspNet.Identity;
using WsRecharge.Models;
using System.Net.Http;


namespace WsRecharge.Controllers
{
    [Authorize]
    public class APIDocumentController : BaseController
    {
        List<ENT.APIDocument> lstEntity = new List<ENT.APIDocument>();
        BAL.APIDocument objBAL = new BAL.APIDocument();
        ENT.APIDocument Model;

        // GET: APIDocument
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public JsonResult SaveEntry(ENT.APIDocument model, string ipid)
        {
            try
            {
                if (model.EntryMode == COM.Enumration.EntryMode.ADD)
                {
                    model.Status = COM.MyEnumration.MyStatus.Active;
                    model.userid = Guid.Parse(User.Identity.GetUserId());
                    if (objBAL.Insert(model))
                    {
                        GlobalVarible.AddMessage("Record Save Successfully");
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
        [HttpGet]
        public JsonResult IsEnabledActivity(bool id)
        {
            try
            {
                ENT.IsEnabledApiCall model2 = new ENT.IsEnabledApiCall();
                BAL.IsEnabledApiCall objBAL = new BAL.IsEnabledApiCall();
                if (id == true || id == false)
                {
                    model2.Isenabled = id;
                    model2.ipaddress = new GenralGetIp().GetIPAddress(Request);
                    model2.userid = Guid.Parse(User.Identity.GetUserId());
                    model2.enabletext = (id == true ? "ON" : "OFF");
                    if (objBAL.Insert(model2))
                    {
                        GlobalVarible.AddMessage("Record Save Successfully");
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
                    Model = new Web.Framework.Entity.APIDocument();
                    Model.ipid = new Guid(id);
                    Model = (ENT.APIDocument)objBAL.GetByPrimaryKey(Model);
                    if (Model.Status == COM.MyEnumration.MyStatus.Active)
                    {
                        if (!objBAL.UpdateStatus(Model.ipid, COM.MyEnumration.MyStatus.DeActive))
                        {
                            throw new Exception("Internal Server Error in status update.");
                        }
                    }
                    if (Model.Status == COM.MyEnumration.MyStatus.DeActive)
                    {
                        if (!objBAL.UpdateStatus(Model.ipid, COM.MyEnumration.MyStatus.Active))
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
        public JsonResult DeleteEntry(string id)
        {
            GlobalVarible.Clear();
            if (id != null)
            {
                Model = new Web.Framework.Entity.APIDocument();
                Model.ipid = new Guid(id);
                if (objBAL.Delete(Model))
                {
                    GlobalVarible.AddMessage("Record Delete Successfully.");
                }
                else
                {
                    GlobalVarible.AddError("Internal Server Error Please Try Again");
                }

            }
            MySession.Current.MessageResult.MessageHtml = GlobalVarible.GetMessageHTML();
            return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public JsonResult GetIsEnabled()
        {
            GlobalVarible.Clear();
            ENT.IsEnabledApiCall model = new ENT.IsEnabledApiCall();
            using (BAL.IsEnabledApiCall obj = new BAL.IsEnabledApiCall())
            {
                model = (ENT.IsEnabledApiCall)obj.GetIsEnabled(Guid.Parse(User.Identity.GetUserId()));
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

    }
}