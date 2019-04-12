using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;
using WEBAPI.Models;

namespace WEBAPI.Controllers
{
    public class BaseApiController : ApiController
    {
        public Guid _LOGINUSERID
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
                return HttpContext.Current.Request.Url.ToString();
            }
        }

        public String _ERRORKEY
        {
            get
            {
                return "BANSIMOBI_API";
            }
        }

        public bool checkpassword(string password)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var user = UserManager.FindById(User.Identity.GetUserId());

            return (user == null ? false : UserManager.CheckPassword(user, password));
        }
    }
}
