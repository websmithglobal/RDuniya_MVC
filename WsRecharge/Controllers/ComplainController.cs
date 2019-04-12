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
using System.Web.Script.Serialization;

namespace WsRecharge.Controllers
{
    public class ComplainController : BaseController
    {
        List<ENT.Complain> lstEntity = new List<ENT.Complain>();

        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Client, VaryByParam = "none", NoStore = true)]
        // GET: Complain
        public ActionResult Index()
        {
            ViewBag.PageHeader = "Complain Report";
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult SaveComplain(ENT.Complain model)
        {
            try
            {
                using (BAL.Complain objBAL = new BAL.Complain())
                {
                    model.CreatedDateTime = DateTime.Now;
                    model.CreatedBy = _LoginUserId;
                    model.complainstatus = (int)COM.MyEnumration.TICKETSTATUS.CREATED;
                    model.adminremarks = string.Empty;
                    
                    if (objBAL.Insert(model))
                    {
                        GlobalVarible.AddMessage("Complain registered successfully.");
                    }
                    else
                    {
                        GlobalVarible.AddError("Unable to register complain.Please contact your administrator.");
                    }
                }
            }
            catch (Exception ex)
            {
                ERRORREPORTING.Report(ex, _REQUESTURL, _LoginUserId, _ERRORKEY,new JavaScriptSerializer().Serialize(model));
                GlobalVarible.AddError(ex.Message);
            }
            MySession.Current.MessageResult.MessageHtml = GlobalVarible.GetMessageHTML();
            return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAll(int ddrange, string fromdate, string todate)
        {
            List<ENT.ComplainView> lstEntity = new List<ENT.ComplainView>();

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

            lstEntity = new BAL.Complain().GetAllSearch(ddrange, fromdate.GetDate(), todate.GetDate());

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
        public JsonResult UpdateComplain(ENT.Complain model)
        {
            try
            {
                using (BAL.Complain objBAL = new BAL.Complain())
                {
                    objBAL.Entity.UpdatedBy = _LoginUserId;
                    objBAL.Entity.UpdatedDateTime = DateTime.Now;
                    objBAL.Entity.complainid = model.complainid;
                    objBAL.Entity.complainstatus = (int)COM.MyEnumration.TICKETSTATUS.RESOLVED;
                    objBAL.Entity.adminremarks = model.adminremarks;

                    if (objBAL.UpdateStatus(objBAL.Entity))
                    {
                        GlobalVarible.AddMessage("Complain updated successfully.");
                    }
                    else
                    {
                        GlobalVarible.AddError("Unable to update complain.Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                ERRORREPORTING.Report(ex, _REQUESTURL, _LoginUserId, _ERRORKEY, new JavaScriptSerializer().Serialize(model));
                GlobalVarible.AddError(ex.Message);
            }
            MySession.Current.MessageResult.MessageHtml = GlobalVarible.GetMessageHTML();
            return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
        }
    }
}