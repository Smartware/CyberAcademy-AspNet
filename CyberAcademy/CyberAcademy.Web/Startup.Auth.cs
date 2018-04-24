using CyberAcademy.Web.DataAccess;
using CyberAcademy.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyberAcademy.Web
{
    public partial class Startup
    {
        public static Func<UserManager<Contact>> UserManagerFactory { get; private set; } = Create;
        public static void ConfigureAuth(IAppBuilder app)
        {
            var option = new CookieAuthenticationOptions()
            {
                AuthenticationType = "ApplicationCookie",
                CookieName = "CyberAcademy",
                LoginPath = new PathString("/Auth/Login")
            };
            app.UseCookieAuthentication(option);
        }

        public static UserManager<AppUser, Guid> Create()
        {
            var dbContext = new AcademyDbContext();
            var store = new UserStore<AppUser,>(dbContext);
            var usermanager = new UserManager<AppUser>(store);
            // allow alphanumeric characters in username
            usermanager.UserValidator = new UserValidator<AppUser>(usermanager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false,
            };

            usermanager.PasswordValidator = new PasswordValidator()
            {
                RequiredLength = 4,
                RequireDigit = false,
                RequireUppercase = false
            };

            return usermanager;
        }
    }
}