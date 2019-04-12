using WsRecharge.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using COM = Web.Framework.Common;
using BAL = Web.Framework.BusinessLayer;
using ENT = Web.Framework.Entity;
using System;

[assembly: OwinStartupAttribute(typeof(WsRecharge.Startup))]
namespace WsRecharge
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // In Startup iam creating first Admin Role and creating a default Admin User 
            if (!roleManager.RoleExists("administrator"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "administrator";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website				

                var user = new ApplicationUser();
                user.UserName = "admin@admin.com";
                user.Email = "admin@admin.com";
                user.PhoneNumber = "9999999999";

                string userPWD = "admin@123";
                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "administrator");
                    ENT.UserProfile obj = new ENT.UserProfile();
                    BAL.UserProfile objBAL = new BAL.UserProfile();
                    obj.up_userid = new Guid(user.Id);
                    obj.up_username = "admin";
                    obj.up_mobile = "9999999999";
                    obj.up_email = "admin@admin.com";
                    obj.up_address = "rajkot";
                    obj.up_upperid = Guid.Parse("00000000-0000-0000-0000-000000000000");
                    obj.up_userlevel = COM.MyEnumration.Userlevel.ADMIN;
                    obj.Status = COM.MyEnumration.MyStatus.Active;
                    obj.up_imei = string.Empty;
                    objBAL.Insert(obj);
                }
                if (!roleManager.RoleExists("apiuser"))
                {
                    var role1 = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                    role1.Name = "apiuser";
                    roleManager.Create(role1);
                }
                if (!roleManager.RoleExists("masterdistributor"))
                {
                    var role1 = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                    role1.Name = "masterdistributor";
                    roleManager.Create(role1);
                }
                if (!roleManager.RoleExists("distributor"))
                {
                    var role1 = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                    role1.Name = "distributor";
                    roleManager.Create(role1);
                }
                if (!roleManager.RoleExists("retailer"))
                {
                    var role1 = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                    role1.Name = "retailer";
                    roleManager.Create(role1);
                }
                if (!roleManager.RoleExists("staff"))
                {
                    var role1 = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                    role1.Name = "staff";
                    roleManager.Create(role1);
                }
            }
        }
    }
}
