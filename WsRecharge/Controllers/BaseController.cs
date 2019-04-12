using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WsRecharge.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;

namespace WsRecharge.Controllers
{
    public class BaseController : Controller
    {
        public int _ProcessStep;

        public Guid _LoginUserId
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    return new Guid(User.Identity.GetUserId());
                }
                else
                {
                    return Guid.Parse("00000000-0000-0000-0000-000000000000");
                }
            }
        }

        public String _REQUESTURL
        {
            get
            {
                return Request.Url.AbsoluteUri.ToString();
            }
        }

        public String _ERRORKEY
        {
            get
            {
                return "BANSIMOBI";
            }
        }

        public bool checkpassword(string password)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var user = UserManager.FindById(User.Identity.GetUserId());

            return (user == null ? false : UserManager.CheckPassword(user, password));
        }

        public bool CheckTwofactorEnabled(string userid,Boolean flag)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            UserManager.SetTwoFactorEnabledAsync(userid, flag);
            return flag;

        }

        protected string SaveGenralImages(string base64)
        {
            string NewFilename = DateTime.Now.Ticks.ToString() + ".JPG";
            string furl = string.Empty;
            furl = Path.Combine(Server.MapPath("~/Uploads/Operator/"), NewFilename);

            base64 = base64.Replace("data:image/jpeg;base64,", "");
            base64 = base64.Replace("data:image/jpg;base64,", "");
            base64 = base64.Replace("data:image/png;base64,", "");
            base64 = base64.Replace("data:image/gif;base64,", "");
            byte[] bytes = Convert.FromBase64String(base64);
            if (bytes.Length <= 2097152)
            {
                MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(furl, System.Drawing.Imaging.ImageFormat.Png);

                furl = Request.Url.Scheme + "://" + Request.Url.Authority + Url.Content("~/Uploads/Operator/") + NewFilename;
                return furl;
            }
            else
            {
                return string.Empty;
            }
        }

        protected string SaveDmtDocument(string base64)
        {
            string NewFilename = DateTime.Now.Ticks.ToString() + ".JPG";
            string furl = string.Empty;
            furl = Path.Combine(Server.MapPath("~/Uploads/dmtdocuments/"), NewFilename);

            base64 = base64.Replace("data:image/jpeg;base64,", "");
            base64 = base64.Replace("data:image/jpg;base64,", "");
            base64 = base64.Replace("data:image/png;base64,", "");
            base64 = base64.Replace("data:image/gif;base64,", "");
            byte[] bytes = Convert.FromBase64String(base64);

            MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            image.Save(furl, System.Drawing.Imaging.ImageFormat.Png);

            furl = Request.Url.Scheme + "://" + Request.Url.Authority + Url.Content("~/Uploads/dmtdocuments/") + NewFilename;
            return furl;
        }
    }
}