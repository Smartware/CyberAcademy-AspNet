using CyberAcademy.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CyberAcademy.Web.Controllers
{

    public class HomeController : Controller
    {
       public ViewResult Index()
        {
            var product = new Product()
            {
                Name = "5 Alive",
                Description = "Citrus Flavour."
            };
            return View(product);
        }

    }
}