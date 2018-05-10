using CyberAcademy.Web.Messaging;
using CyberAcademy.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;



namespace CyberAcademy.Web.Controllers.api
{
    [RoutePrefix("api/settingsapi")]
    public class SettingsApiController: ApiController
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

        [Route("contact")]
        [HttpPost]
        public HttpResponseMessage Contact(ContactViewModel cvm)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, "your fields are not valid");

                }

                string key = ConfigurationManager.AppSettings["Sendgrid.key"];

                SendGridEmailService svc = new SendGridEmailService(key);

                string htmlBody = $"<ul>Name :{cvm.Name}<li>Phone: {cvm.Phone}</li><li>Email: {cvm.Email}</li><li>Meassage: {cvm.Message}</li></ul>";

                EmailMessage msg = new EmailMessage
                {
                     Body = htmlBody,
                      From = "Chuks",
                      Subject = "Cyberspace TEST",
                       Recipient = "oriahie@gmail.com"
                      

                };

                if(svc.SendMail(msg, true).Result == string.Empty)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, "Error sending mail, pls try again later.");
                }

                return this.Request.CreateResponse(HttpStatusCode.OK, "Successfully sent your mail!");

            }
            catch (Exception ex)
            {

                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);

            }
        }
    }
}