using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace CyberAcademy.Web.App_Start
{
    public class FilterConfig
    {
        public static void Configure(GlobalFilterCollection filterCol)
        {
            filterCol.Add(new AuthorizeAttribute());
        }
    }
}