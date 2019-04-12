using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;
using BAL = Web.Framework.BusinessLayer;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using WsRecharge.Models;

namespace WsRecharge.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        BAL.Dashboard objBalDashboard = new BAL.Dashboard();
        ENT.Dashboard objENTDashboard = new ENT.Dashboard();

        // [OutputCache(Duration = 3600, VaryByParam = "none", NoStore = true)]
        public ActionResult Index()
        {

            DashBoardModels objDashBoardModels = new DashBoardModels();
            try
            {
                if (User.isInRoles("administrator", "apiuser", "masterdistributor", "distributor", "retailer"))
                {
                    ViewBag.PageHeader = "Dashboard";
                    objENTDashboard = objBalDashboard.GetDashboardData(Guid.Parse(User.Identity.GetUserId()), User.GetUserlevel());
                    objDashBoardModels.TotalCustomers = objENTDashboard.TotalCustomers;
                    objDashBoardModels.TotalBalance = objENTDashboard.TotalBalance;
                    objDashBoardModels.TotalTodayRequests = objENTDashboard.TotalTodayRequests;
                    objDashBoardModels.TotalSuccess = objENTDashboard.TotalSuccess;
                    objDashBoardModels.TotalFailed = objENTDashboard.TotalFailed;
                    objDashBoardModels.DMTtotalSuccess = objENTDashboard.DMTtotalSuccess;
                    objDashBoardModels.DMTtotalFailed = objENTDashboard.DMTtotalFailed;
                    objDashBoardModels.DMTinproccess = objENTDashboard.DMTinproccess;
                    objDashBoardModels.WalletRequests = objENTDashboard.WalletRequests;
                    objDashBoardModels.LoginActivity = objENTDashboard.LoginActivity;
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }

            List<ENT.ServiceMaster> lstService = new List<ENT.ServiceMaster>();
            using (BAL.ServiceMaster objBal = new BAL.ServiceMaster())
            {
                lstService = objBal.GetService("30B24352-9F4B-485A-AB18-528E74F6F260");
            }

            List<ENT.CircleMaster> lstCircles = new List<ENT.CircleMaster>();
            using (BAL.CircleMaster objBal = new BAL.CircleMaster())
            {
                lstCircles = objBal.GetAllActive("IN");
            }

            ViewBag.ServiceMaster = lstService;
            ViewBag.CircleMaster = lstCircles;

            return View(objDashBoardModels);
        }

        public JsonResult LoadStatastics()
        {
            DashBoardModels objDashBoardModels = new DashBoardModels();
            try
            {
                objENTDashboard = objBalDashboard.GetDashboardData(Guid.Parse(User.Identity.GetUserId()), User.GetUserlevel());
                objDashBoardModels.TotalCustomers = objENTDashboard.TotalCustomers;
                objDashBoardModels.TotalBalance = objENTDashboard.TotalBalance;
                objDashBoardModels.TotalTodayRequests = objENTDashboard.TotalTodayRequests;
                objDashBoardModels.TotalSuccess = objENTDashboard.TotalSuccess;
                objDashBoardModels.TotalFailed = objENTDashboard.TotalFailed;
                objDashBoardModels.DMTtotalSuccess = objENTDashboard.DMTtotalSuccess;
                objDashBoardModels.DMTtotalFailed = objENTDashboard.DMTtotalFailed;
                objDashBoardModels.DMTinproccess = objENTDashboard.DMTinproccess;
                objDashBoardModels.WalletRequests = objENTDashboard.WalletRequests;
                objDashBoardModels.LoginActivity = objENTDashboard.LoginActivity;
            }
            catch (Exception ex)
            {
                ERRORREPORTING.Report(ex, Request.Url.ToString(), _LoginUserId, _ERRORKEY, string.Empty);
            }
            return Json(objDashBoardModels, JsonRequestBehavior.AllowGet);
        }
    }
}