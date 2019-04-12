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
    public class ApiUserController : BaseController
    {
        List<ENT.UserProfile> lstEntity = new List<ENT.UserProfile>();
        BAL.UserProfile objBAL = new BAL.UserProfile();
        ENT.UserProfile Model;
        // GET: ApiUser
        public ActionResult Index()
        {
            ViewBag.PageHeader = "ApiUser Master";
            ViewBag.Slab = new BAL.RechargeSlab().GetAllSlab();
            return View();
        }
        [HttpPost]
        [Authorize]
        public JsonResult UpdateEntry(ENT.UserProfile model, string up_id)
        {
            try
            {
                model.UpdatedDateTime = DateTime.Now;
                model.UpdatedBy = Guid.Parse(User.Identity.GetUserId());
                model.up_id = new Guid(up_id.Replace("/", ""));
                if (objBAL.UpdatePartial(model))
                {
                    //string id = User.Identity.GetUserId();
                    //  ApplicationUser currentUser = UserManager.FindById(id);
                    var user = new ApplicationUser();
                    user.Email = model.up_email;


                    GlobalVarible.AddMessage("Record Update Successfully");
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
            lstEntity = objBAL.GetAll(search.ToString().Replace("\0", string.Empty), COM.MyEnumration.Userlevel.APIUSER);
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
                    Model = new Web.Framework.Entity.UserProfile();
                    Model.up_id = new Guid(id);
                    Model = (ENT.UserProfile)objBAL.GetByPrimaryKey(Model);
                    if (Model.Status == COM.MyEnumration.MyStatus.Active)
                    {
                        if (!objBAL.UpdateStatus(Model.up_id, COM.MyEnumration.MyStatus.DeActive))
                        {
                            throw new Exception("Internal Server Error in status update.");
                        }
                    }
                    if (Model.Status == COM.MyEnumration.MyStatus.DeActive)
                    {
                        if (!objBAL.UpdateStatus(Model.up_id, COM.MyEnumration.MyStatus.Active))
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
                Model = new Web.Framework.Entity.UserProfile();
                Model.UpdatedDateTime = DateTime.Now;
                Model.up_id = new Guid(id);
                Model = (ENT.UserProfile)objBAL.GetByPrimaryKey(Model);
            }
            return Json(new { Model = Model }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize]
        public JsonResult CheckTwofactor(string id, Boolean flag)
        {
            Boolean myflag = CheckTwofactorEnabled(id, flag);
            GlobalVarible.AddMessage("Two factor Enabled Update Successfully.");
            MySession.Current.MessageResult.MessageHtml = GlobalVarible.GetMessageHTML();
            return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
        }


    }
}