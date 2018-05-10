using CyberAcademy.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CyberAcademy.Web.Controllers.api
{
    [RoutePrefix("api/settingsapi")]
    public class SettingsApiController:ApiController
    {
        private UserManager<AppUser, int> userMgr;
            private RoleManager<AppRole, int> roleMgr;
        public SettingsApiController()
        {
            userMgr = Startup.UserManagerFactory.Invoke();
            roleMgr = Startup.RoleManagerFactory.Invoke();
        }

        [Route("create_user")]
        [HttpPost]
        public HttpResponseMessage CreateUser(UserViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, "your fields are not valid");
                }

                var user = new AppUser()
                {
                    UserName = vm.Username,
                    Email = vm.Email,
                };

                var result = userMgr.Create(user, vm.Password);

                if (!result.Succeeded)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, result.Errors.FirstOrDefault());
                }

                return this.Request.CreateResponse(HttpStatusCode.Created, "Successful");
            }
            catch (Exception ex)
            {
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}