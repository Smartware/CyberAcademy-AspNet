using CyberAcademy.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CyberAcademy.Web.Controllers
{
    public class SettingsController: Controller
    {
        private RoleManager<AppRole, int> roleMgr;
        public SettingsController()
        {
            roleMgr = Startup.RoleManagerFactory.Invoke();
        }

        [HttpGet]
        public ActionResult CreateUsers()
        {
            ViewBag.Roles = roleMgr.Roles.ToList();

            return View();
        }
    }
}