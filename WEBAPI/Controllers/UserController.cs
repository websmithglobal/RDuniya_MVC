using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using COM = Web.Framework.Common;
using BAL = Web.Framework.BusinessLayer;
using ENT = Web.Framework.Entity;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using WEBAPI.Models;
using System.Web.Script.Serialization;

namespace WEBAPI.Controllers
{
    public class UserController : BaseApiController
    {
        private ApplicationUserManager _userManager;

        public UserController()
        {
        }

        public UserController(ApplicationUserManager userManager,
           ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        /// <summary>
        /// This api is used to  register device token of user
        /// </summary>
        /// <returns>List of all available services.</returns>
        /// <remarks>Get all services from database whose status is active</remarks>
        [HttpPost]
        [Authorize]
        [ActionName("RegisterToken")]
        public HttpResponseMessage RegisterToken(ENT.UserDeviceToken modal)
        {
            GlobalVarible.Clear();
            try
            {
                using (BAL.UserDeviceToken objBal = new BAL.UserDeviceToken())
                {
                    modal.udt_userid = _LOGINUSERID;
                    modal.CreatedBy = _LOGINUSERID;
                    modal.CreatedDateTime = DateTime.Now;
                    if (objBal.Insert(modal))
                    {
                        GlobalVarible.AddMessage("Token Registered Successfully.");
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
                    }
                    else
                    {
                        GlobalVarible.AddError("Unable to Add Token.Please Try Again");
                        ERRORREPORTING.Report(new Exception("Unable to Add Token.Please Try Again"), _REQUESTURL, _LOGINUSERID, _ERRORKEY, new JavaScriptSerializer().Serialize(modal));
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
                ERRORREPORTING.Report(ex, _REQUESTURL, _LOGINUSERID, _ERRORKEY, new JavaScriptSerializer().Serialize(modal));
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
            }
        }

        /// <summary>
        /// This api is used to  get latest balance of logedin user
        /// </summary>
        /// <returns>Final balance of user.</returns>
        /// <remarks>Get all services from database whose status is active</remarks>
        [HttpPost]
        [Authorize]
        [ActionName("GetLatesBalance")]
        public HttpResponseMessage GetLatesBalance()
        {
            GlobalVarible.Clear();
            try
            {
                using (BAL.UserProfile objBal = new BAL.UserProfile())
                {
                    ENT.UserProfile up = objBal.GetBalance(_LOGINUSERID);

                    GlobalVarible.AddMessage("User Balance Get Successfully.");

                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult, up.up_balance });
                }
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
                ERRORREPORTING.Report(ex, _REQUESTURL, _LOGINUSERID, _ERRORKEY, string.Empty);
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
            }
        }

        /// <summary>
        /// This api is used get all child user of loggdein user.
        /// </summary>
        /// <returns>return list of all downline users .</returns>
        /// <remarks></remarks>
        [HttpPost]
        [Authorize]
        [ActionName("GetDownlineUsers")]
        public HttpResponseMessage GetDownlineUsers()
        {
            GlobalVarible.Clear();

            List<ENT.UserProfileApiView> downlinelist = new List<ENT.UserProfileApiView>();

            try
            {
                if (User.IsInRole("masterdistributor"))
                {
                    downlinelist = new BAL.UserProfile().GetMasterDownline(_LOGINUSERID);
                }
                else if (User.IsInRole("distributor"))
                {
                    downlinelist = new BAL.UserProfile().GetDistributorDownline(_LOGINUSERID);
                }

                GlobalVarible.AddMessage("Downline users get successfully.");

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult, downlinelist });
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
                ERRORREPORTING.Report(ex, _REQUESTURL, _LOGINUSERID, _ERRORKEY, string.Empty);
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
            }
        }

        /// <summary>
        /// This api is used create new child user.
        /// </summary>
        /// <returns>return general response object in json formate .</returns>
        /// <remarks></remarks>
        [HttpPost]
        [Authorize]
        [ActionName("CreateUser")]
        public HttpResponseMessage CreateUser(RegisterViewModel model)
        {
            GlobalVarible.Clear();

            try
            {
                var user = new ApplicationUser { UserName = model.Mobile, Email = model.Email, EmailConfirmed = true, PhoneNumber = model.Mobile };
                var result = UserManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    if ((int)User.GetUserlevel() == 1)
                    {
                        this.UserManager.AddToRole(user.Id, "distributor");
                    }
                    else if ((int)User.GetUserlevel() == 2)
                    {
                        this.UserManager.AddToRole(user.Id, "retailer");
                    }

                    using (BAL.UserProfile objBAL = new BAL.UserProfile())
                    {
                        ENT.UserProfile userProfile = objBAL.GetSlabName(_LOGINUSERID);

                        objBAL.Entity.up_userid = new Guid(user.Id);
                        objBAL.Entity.up_username = model.UserName;
                        objBAL.Entity.up_mobile = model.Mobile;
                        objBAL.Entity.up_email = model.Email;
                        objBAL.Entity.up_address = model.Address;
                        objBAL.Entity.up_upperid = _LOGINUSERID;

                        if ((int)User.GetUserlevel() == 1)
                        {
                            objBAL.Entity.up_userlevel = COM.MyEnumration.Userlevel.SD;
                        }
                        else if ((int)User.GetUserlevel() == 2)
                        {
                            objBAL.Entity.up_userlevel = COM.MyEnumration.Userlevel.R;
                        }

                        objBAL.Entity.Status = COM.MyEnumration.MyStatus.Active;
                        objBAL.Entity.up_balance = 0;
                        objBAL.Entity.slabid = userProfile.slabid;
                        objBAL.Entity.up_imei = string.Empty;
                        objBAL.Entity.CreatedBy = _LOGINUSERID;
                        objBAL.Entity.CreatedDateTime = DateTime.Now;

                        if (objBAL.Insert(objBAL.Entity))
                        {
                            GlobalVarible.AddMessage("User created successfully.");
                            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
                        }
                        else
                        {
                            GlobalVarible.AddError("Unable to save user detail.Please check data and try again.");
                            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
                        }
                    }
                }
                else
                {
                    GlobalVarible.AddErrors(result.Errors.ToList<string>());
                    ERRORREPORTING.Report(new Exception(string.Join(",",result.Errors.ToList<string>())), _REQUESTURL, _LOGINUSERID, _ERRORKEY, string.Empty);
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
                }
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
                ERRORREPORTING.Report(ex, _REQUESTURL, _LOGINUSERID, _ERRORKEY, string.Empty);
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
            }
        }
    }
}
