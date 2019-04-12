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

namespace WsRecharge.Controllers
{
    [Authorize]
    public class OperatorMappingController : BaseController
    {
        List<ENT.OperatorMapping> lstEntity = new List<ENT.OperatorMapping>();
        BAL.OperatorMapping objBAL = new BAL.OperatorMapping();
        ENT.OperatorMapping Model;
        // GET: OperatorMapping
        public ActionResult Index()
        {
            ViewBag.PageHeader = "Operator Code Mapping Master";
            ViewBag.OperatorMaster = new BAL.OperatorSetup().GetAll(string.Empty);
            ViewBag.ApisMaster = new BAL.Apis().GetAll(string.Empty);
            return View();
        }
        [HttpPost]
        [Authorize]
        public JsonResult SaveEntry(ENT.OperatorMapping model, string operatormapid)
        {
            try
            {
                List<Guid> dctDuplication = new List<Guid>();
                if (model.EntryMode == COM.Enumration.EntryMode.ADD)
                {
                    if (objBAL.Insert(model))
                    {
                        GlobalVarible.AddMessage("Record Save Successfully");
                    }
                }
                else
                {
                    model.UpdatedDateTime = DateTime.Now;
                    model.UpdatedBy = Guid.Parse(User.Identity.GetUserId());
                    model.operatormapid = new Guid(operatormapid.Replace("/", ""));
                    if (string.IsNullOrEmpty(model.operatornomal)) { model.operatornomal = string.Empty; }
                    if (string.IsNullOrEmpty(model.operatorspecial)) { model.operatorspecial = string.Empty; }
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
        public JsonResult EditRecord(string id)
        {
            if (id != null)
            {
                Model = new Web.Framework.Entity.OperatorMapping();
                Model.UpdatedDateTime = DateTime.Now;
                Model.operatormapid = new Guid(id);
                Model = (ENT.OperatorMapping)objBAL.GetByPrimaryKey(Model);
            }
            return Json(new { Model = Model }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize]
        public JsonResult DeleteEntry(string id)
        {
            GlobalVarible.Clear();
            if (id != null)
            {
                Model = new Web.Framework.Entity.OperatorMapping();
                Model.operatormapid = new Guid(id);
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

    }
}