using CyberAcademy.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CyberAcademy.Web.Controllers
{
    [Authorize()]
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        public string GetDate()
        {
            return DateTime.Now.ToLongDateString();
        }
        // GET: Home
        public ActionResult Index()
        {
            var claimsPrincipal = this.User.Identity as ClaimsIdentity;

           var claim = claimsPrincipal.FindFirst("PassportUrl");

            ViewBag.ProfileUrl = claim.Value;
            ViewData["SalesAnalyticsCaptions"] = "Sales Analytics";
            return View();
        }
    }
}