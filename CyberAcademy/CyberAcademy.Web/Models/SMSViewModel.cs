using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyberAcademy.Web.Models
{
    public class SMSViewModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Message { get; set; }

    }
}