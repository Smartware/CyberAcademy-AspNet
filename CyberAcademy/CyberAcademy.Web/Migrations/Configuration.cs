namespace CyberAcademy.Web.Migrations
{
    using CyberAcademy.Web.DataAccess;
    using CyberAcademy.Web.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CyberAcademy.Web.DataAccess.AcademyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CyberAcademy.Web.DataAccess.AcademyDbContext context)
        {


            string[] roles = new string[2] { "ADMIN", "STAFF" };
            var roleStore = new RoleStore<AppRole, int, AppUserRole>(new AcademyDbContext());
            var roleManager = new RoleManager<AppRole, int>(roleStore);

            Array.ForEach(roles, r => 
            {
                if (roleManager.RoleExists(r))
                    return;

                roleManager.Create(new AppRole() { Name = r });
            });


            string username = "admin@cyberspace.com";
            string password = "admin";
            string role = "ADMIN";

            //IUserStore<Contact> contactStore = new UserStore<Contact>(new AcademyDbContext());
            //UserManager<Contact, string> userMgr = new UserManager<Contact, string>(contactStore);
            var userMgr = Startup.UserManagerFactory.Invoke();


            if (userMgr.FindByName(username) != null)
                return;


            var contact = new AppUser() { UserName = username };
            var result = userMgr.Create(contact, password);

           


            if (!roleManager.RoleExists(role))
            {
                var irole = new AppRole() { Name = role };
                roleManager.Create(irole);
            }

            if (!userMgr.IsInRole(contact.Id, role))
            {
                userMgr.AddToRole(contact.Id, role);
            }

            
        }
    }
}
