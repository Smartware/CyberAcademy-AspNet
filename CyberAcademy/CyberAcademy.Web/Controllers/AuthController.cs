using CyberAcademy.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CyberAcademy.Web.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private UserManager<AppUser, int> _userManager;
        public AuthController()
        {
            _userManager = Startup.UserManagerFactory.Invoke();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        public ActionResult Logout()
        {
            var ctxt = this.Request.GetOwinContext();
            ctxt.Authentication.SignOut("ApplicationCookie");

            return Redirect("Login");
        }

        [HttpPost]
        public ActionResult Login(LoginInfo data)
        {
            if (this.ModelState.IsValid)
            {
                var contact = _userManager.Find(data.Username, data.Password);
                if (contact != null)
                {
                    ClaimsIdentity claimsIdentity = _userManager.CreateIdentity(contact, "ApplicationCookie");
                    claimsIdentity.AddClaim(new Claim("PassportUrl", Url.Content("~/images/profile.png")));

                    var ctxt = this.Request.GetOwinContext();
                    ctxt.Authentication.SignIn(claimsIdentity);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    this.ModelState.AddModelError("", "Username or password is invalid");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
    }
}