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
using WsRecharge.Models;
using COM = Web.Framework.Common;
using BAL = Web.Framework.BusinessLayer;
using ENT = Web.Framework.Entity;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WsRecharge.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.Mobile, model.Password, model.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    ApplicationUser user = UserManager.FindByName(model.Mobile);
                    ENT.ActivityLog model2 = new Web.Framework.Entity.ActivityLog();
                    BAL.ActivityLog ObjBAL = new Web.Framework.BusinessLayer.ActivityLog();
                    string[] RetOutput = new string[3];
                    RetOutput = new GenralGetIp().GetBrowerInformation();
                    model2.browser = RetOutput[0].ToString() + " " + RetOutput[1].ToString();
                    model2.useragent = RetOutput[2].ToString();
                    model2.ipaddress = new GenralGetIp().GetIPAddress(Request);
                    model2.userid = Guid.Parse(user.Id);
                    model2.macaddress = new GenralGetIp().GetMacAddress();
                    ObjBAL.Insert(model2);
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }
        //
        // POST: /Account/Register
        [HttpPost]
        public async Task<JsonResult> ApiUsersRegister(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email, EmailConfirmed = true, PhoneNumber = model.Mobile };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        try
                        {
                            await this.UserManager.AddToRoleAsync(user.Id, "apiuser");
                            ENT.UserProfile objmodel = new ENT.UserProfile();
                            BAL.UserProfile objBAL = new BAL.UserProfile();

                            objmodel.up_userid = new Guid(user.Id);
                            objmodel.up_username = model.UserName;
                            objmodel.up_mobile = model.Mobile;
                            objmodel.up_email = model.Email;
                            objmodel.up_address = model.Address;
                            objmodel.up_upperid = Guid.Parse(User.Identity.GetUserId());
                            objmodel.up_userlevel = COM.MyEnumration.Userlevel.APIUSER;
                            objmodel.Status = COM.MyEnumration.MyStatus.Active;
                            objmodel.up_balance = 0;
                            objmodel.slabid = model.slabid;
                            objmodel.up_imei = string.Empty;
                            if (objBAL.Insert(objmodel))
                            {
                                GlobalVarible.AddMessage("Record Save Successfully");
                            }
                            else
                            {
                                GlobalVarible.AddError("Internal Error...");
                                return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
                            }
                        }
                        catch (Exception ex)
                        {
                            GlobalVarible.AddError("Contact to developer..");
                            var context = new ApplicationDbContext();
                            await UserManager.DeleteAsync(user);
                        }
                    }
                    else
                    {
                        GlobalVarible.AddErrors(result.Errors.ToList<string>());
                        return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    var Errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                    List<string> ErrorList = new List<string>();
                    foreach (var item in Errors)
                    {
                        ErrorList.Add(item[0].ErrorMessage);
                    }
                    GlobalVarible.AddErrors(ErrorList);
                    return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                GlobalVarible.AddError(ex.Message);
            }
            MySession.Current.MessageResult.MessageHtml = GlobalVarible.GetMessageHTML();
            return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// This method is used for register new MD in application.
        /// </summary>
        
        [HttpPost]
        public async Task<JsonResult> MDRegister(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.Mobile, Email = model.Email, EmailConfirmed = true, PhoneNumber = model.Mobile };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        try
                        {
                            await this.UserManager.AddToRoleAsync(user.Id, "masterdistributor");
                            ENT.UserProfile objmodel = new ENT.UserProfile();
                            BAL.UserProfile objBAL = new BAL.UserProfile();

                            objmodel.up_userid = new Guid(user.Id);
                            objmodel.up_username = model.UserName;
                            objmodel.up_mobile = model.Mobile;
                            objmodel.up_email = model.Email;
                            objmodel.up_address = model.Address;
                            objmodel.up_upperid = Guid.Parse(User.Identity.GetUserId());
                            objmodel.up_userlevel = COM.MyEnumration.Userlevel.MD;
                            objmodel.Status = COM.MyEnumration.MyStatus.Active;
                            objmodel.up_balance = 0;
                            objmodel.slabid = model.slabid;
                            objmodel.up_imei = string.Empty;
                            objmodel.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                            objmodel.CreatedDateTime = DateTime.Now;

                            if (objBAL.Insert(objmodel))
                            {
                                GlobalVarible.AddMessage("Record Save Successfully.");
                            }
                            else
                            {
                                GlobalVarible.AddError("Internal Error...");
                                return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
                            }
                        }
                        catch (Exception ex)
                        {
                            ERRORREPORTING.Report(ex, _REQUESTURL, _LoginUserId, _ERRORKEY, JsonConvert.SerializeObject(model));
                            GlobalVarible.AddError("Contact to developer..");
                            var context = new ApplicationDbContext();
                            await UserManager.DeleteAsync(user);
                        }
                    }
                    else
                    {
                        GlobalVarible.AddErrors(result.Errors.ToList<string>());
                        return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    var Errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                    List<string> ErrorList = new List<string>();
                    string error = string.Empty;
                    foreach (var item in Errors)
                    {
                        error = error + ','+ item[0].ErrorMessage;
                        ErrorList.Add(item[0].ErrorMessage);
                    }
                    ERRORREPORTING.Report(new Exception(error), _REQUESTURL, _LoginUserId, _ERRORKEY, JsonConvert.SerializeObject(model));
                    GlobalVarible.AddErrors(ErrorList);
                    return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ERRORREPORTING.Report(ex, _REQUESTURL, _LoginUserId, _ERRORKEY, JsonConvert.SerializeObject(model));
                GlobalVarible.AddError(ex.Message);
            }
            MySession.Current.MessageResult.MessageHtml = GlobalVarible.GetMessageHTML();
            return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// This method is used for register new Distributor in application.
        /// </summary>
        [HttpPost]
        public async Task<JsonResult> DRegister(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.Mobile, Email = model.Email, EmailConfirmed = true, PhoneNumber = model.Mobile };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        try
                        {
                            await this.UserManager.AddToRoleAsync(user.Id, "distributor");
                            ENT.UserProfile objmodel = new ENT.UserProfile();
                            BAL.UserProfile objBAL = new BAL.UserProfile();
                            objmodel = new BAL.UserProfile().GetSlabName(Guid.Parse(User.Identity.GetUserId()));
                            objmodel.up_userid = new Guid(user.Id);
                            objmodel.up_username = model.UserName;
                            objmodel.up_mobile = model.Mobile;
                            objmodel.up_email = model.Email;
                            objmodel.up_address = model.Address;
                            objmodel.up_upperid = Guid.Parse(User.Identity.GetUserId());
                            objmodel.up_userlevel = COM.MyEnumration.Userlevel.SD;
                            objmodel.Status = COM.MyEnumration.MyStatus.Active;
                            objmodel.up_balance = 0;
                            objmodel.slabid = objmodel.slabid;
                            objmodel.up_imei = string.Empty;
                            objmodel.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                            objmodel.CreatedDateTime = DateTime.Now;

                            if (objBAL.Insert(objmodel))
                            {
                                GlobalVarible.AddMessage("Record Save Successfully.");
                            }
                            else
                            {
                                GlobalVarible.AddError("Internal Error...");
                                return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
                            }
                        }
                        catch (Exception ex)
                        {
                            ERRORREPORTING.Report(ex, _REQUESTURL, _LoginUserId, _ERRORKEY, JsonConvert.SerializeObject(model));
                            GlobalVarible.AddError("Contact to developer..");
                            var context = new ApplicationDbContext();
                            await UserManager.DeleteAsync(user);
                        }
                    }
                    else
                    {
                        GlobalVarible.AddErrors(result.Errors.ToList<string>());
                        return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    var Errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                    List<string> ErrorList = new List<string>();
                    string error = string.Empty;
                    foreach (var item in Errors)
                    {
                        error = error + ',' + item[0].ErrorMessage;
                        ErrorList.Add(item[0].ErrorMessage);
                    }
                    ERRORREPORTING.Report(new Exception(error), _REQUESTURL, _LoginUserId, _ERRORKEY, JsonConvert.SerializeObject(model));
                    GlobalVarible.AddErrors(ErrorList);
                    return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                GlobalVarible.AddError(ex.Message);
            }
            MySession.Current.MessageResult.MessageHtml = GlobalVarible.GetMessageHTML();
            return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This method is used for register new Retailer in application.
        /// </summary>
        [HttpPost]
        public async Task<JsonResult> RRegister(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.Mobile, Email = model.Email, EmailConfirmed = true, PhoneNumber = model.Mobile };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        try
                        {
                            await this.UserManager.AddToRoleAsync(user.Id, "retailer");
                            ENT.UserProfile objmodel = new ENT.UserProfile();
                            BAL.UserProfile objBAL = new BAL.UserProfile();
                            objmodel = new BAL.UserProfile().GetSlabName(Guid.Parse(User.Identity.GetUserId()));
                            objmodel.up_userid = new Guid(user.Id);
                            objmodel.up_username = model.UserName;
                            objmodel.up_mobile = model.Mobile;
                            objmodel.up_email = model.Email;
                            objmodel.up_address = model.Address;
                            objmodel.up_upperid = Guid.Parse(User.Identity.GetUserId());
                            objmodel.up_userlevel = COM.MyEnumration.Userlevel.R;
                            objmodel.Status = COM.MyEnumration.MyStatus.Active;
                            objmodel.up_balance = 0;
                            objmodel.slabid = objmodel.slabid;
                            objmodel.up_imei = string.Empty;
                            objmodel.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                            objmodel.CreatedDateTime = DateTime.Now;

                            if (objBAL.Insert(objmodel))
                            {
                                GlobalVarible.AddMessage("Record Save Successfully");
                            }
                            else
                            {
                                GlobalVarible.AddError("Internal Error...");
                                return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
                            }
                        }
                        catch (Exception ex)
                        {
                            ERRORREPORTING.Report(ex, _REQUESTURL, _LoginUserId, _ERRORKEY, JsonConvert.SerializeObject(model));
                            GlobalVarible.AddError("Contact to developer..");
                            var context = new ApplicationDbContext();
                            await UserManager.DeleteAsync(user);
                        }
                    }
                    else
                    {
                        GlobalVarible.AddErrors(result.Errors.ToList<string>());
                        return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    var Errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                    List<string> ErrorList = new List<string>();
                    string error = string.Empty;
                    foreach (var item in Errors)
                    {
                        error = error + ',' + item[0].ErrorMessage;
                        ErrorList.Add(item[0].ErrorMessage);
                    }
                    ERRORREPORTING.Report(new Exception(error), _REQUESTURL, _LoginUserId, _ERRORKEY, JsonConvert.SerializeObject(model));
                    GlobalVarible.AddErrors(ErrorList);
                    return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                GlobalVarible.AddError(ex.Message);
            }
            MySession.Current.MessageResult.MessageHtml = GlobalVarible.GetMessageHTML();
            return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
        }
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}