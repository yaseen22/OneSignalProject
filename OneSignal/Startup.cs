using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using OneSignal.Models;
using Owin;
using System.Configuration;

[assembly: OwinStartupAttribute(typeof(OneSignal.Startup))]
namespace OneSignal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //Create Admin Role then default Admin User     
            if (!roleManager.RoleExists("Admin"))
            {

                //Create Admin role    
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Create Admin super user             
                var user = new ApplicationUser();
                user.UserName = ConfigurationManager.AppSettings["DefaultAdminUserName"];
                user.Email = ConfigurationManager.AppSettings["DefaultAdminUserEmail"];
                string userPWD = ConfigurationManager.AppSettings["DefaultAdminUserPassword"];
                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Admin role    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            // Create Data Entry Operator role     
            if (!roleManager.RoleExists("Data Entry Operator"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Data Entry Operator";
                roleManager.Create(role);
            }
        }
    }

   
}
