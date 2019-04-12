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
    public class ProfileController : BaseController
    {
        ENT.UserProfile Model;
        BAL.UserProfile objBAL = new BAL.UserProfile();
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetUserProfile()
        {
            Model = new Web.Framework.Entity.UserProfile();
            Model = (ENT.UserProfile)objBAL.GetUserProfile(User.Identity.GetUserId());
            return Json(new { Model = Model }, JsonRequestBehavior.AllowGet);
        }
            
    }
}