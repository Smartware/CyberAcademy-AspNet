using CyberAcademy.Web.Messaging;
using CyberAcademy.Web.Models;
using Microsoft.AspNet.Identity;
using MoboPay.Helpers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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

        [Route("Contact")]
        [HttpPost]

        public HttpResponseMessage SendContact(ContactViewModel cvm)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, "your fields are not valid");
                }

                string key = ConfigurationManager.AppSettings["Sendgrid.Key"];
                SendGridEmailService messageSvc = new SendGridEmailService(key);

                string htmlBody = $@"<ul>
                                <li>Name: {cvm.Name}</li>    
                                <li>Email: {cvm.Email}</li>
                                <li>Phone Number: {cvm.Phone}</li>
                                <li>Message Deatails: {cvm.Message}</li>
                            </ul>";

                EmailMessage msg = new EmailMessage()
                {
                    Body = htmlBody,
                    Subject = "Cyberspace Test",
                    From = cvm.Name,
                    Recipient = "seanadeyemi@gmail.com"
                };



                //if (messageSvc.SendMail(msg, true).Result != string.Empty)
                //{
                //    return this.Request.CreateResponse(HttpStatusCode.BadRequest, "Error sending mail");

                //}
                //var context = Request.Properties["MS_HttpContext"] as HttpContext;
                string envPath = HttpRuntime.AppDomainAppPath;
                string fileName  = $"{envPath}\\Content\\assets\\images\\logo_dark.png";
                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);

                messageSvc.SendMail(msg, true, fileName, imageData);
                
                //return this.Request.CreateResponse(HttpStatusCode.BadRequest, "Error sending mail");

                

                //if(messageSvc.SendMail(msg, true).Result != string.Empty)
                //{
                //    return this.Request.CreateResponse(HttpStatusCode.BadRequest, "Error sending mail");

                //}

                return Request.CreateResponse(HttpStatusCode.OK, "Successfully sent mail");

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
               
            }
        }

        [Route("ContactSmtp")]
        [HttpPost]

        public HttpResponseMessage SendContactSmtp(ContactViewModel cvm)
        {
            try
            {
                //C:\repos\CyberAcademy-AspNet\CyberAcademy\CyberAcademy.Web\Content\template\Email.txt
                if (!ModelState.IsValid)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "your fields are not valid");
                }
                SmtpEmailService messageSvc = new SmtpEmailService();


                string envPath = HttpRuntime.AppDomainAppPath;
                string htmlBody = File.ReadAllText($"{envPath}\\Messaging\\Email.html");
                Random r = new Random();
                int number = r.Next(5000);

                htmlBody = htmlBody.Replace("$customerName$", cvm.Name).Replace("$PIN$", number.ToString()).Replace("$emailAddress$", cvm.Email).Replace("$phoneNumber$", cvm.Phone);

                string attachmentPath = $"{envPath}\\Content\\assets\\images\\bg.jpg";

                EmailMessage msg = new EmailMessage()
                {
                    Body = htmlBody,
                    Subject = "Cyberspace Smtp Test",
                    From = cvm.Name,
                    Recipient = "seanadeyemi@gmail.com"
                };

                if (messageSvc.SendEmail(msg, attachmentPath) == string.Empty)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Error sending mail");

                }
                return Request.CreateResponse(HttpStatusCode.OK, "Successfully sent mail via smtp");
            }
            catch (Exception ex)
            {

                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [Route("SendSms")]
        [HttpPost]
        public HttpResponseMessage SendSms(SMSViewModel svm)
        {

            
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "your fields are not valid");
                }
                string infobipKey = ConfigurationManager.AppSettings["Infobip.Key"];
                string auth = "Basic " + Base64.Base64Encode(infobipKey);

                string responseText = "";
                int number;

                if (string.IsNullOrEmpty(svm.From))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Sender");
                }
                else if ((string.IsNullOrEmpty(svm.To)) || (int.TryParse(svm.To, out number)))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Recipient");
                }
                else
                {
                    var client = new RestClient("https://api.infobip.com/sms/1/text/single");
                   
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("accept", "application/json");
                    request.AddHeader("content-type", "application/json");
                    request.AddHeader("authorization", auth);
                    request.AddParameter("application/json", "{\"from\":\""+svm.From+"\", \"to\":\"" + svm.To + "\",\"text\":\"" + svm.Message + "\"}", ParameterType.RequestBody);

                    IRestResponse response = client.Execute(request);

                    responseText = response.Content;
                }

                return Request.CreateResponse(HttpStatusCode.OK, responseText);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}