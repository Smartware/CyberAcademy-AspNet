using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CyberAcademy.Web.Models
{
    public class RegisterInfo
    {
        public string EmailAddress { get; set; }
        [Compare("ConfirmPassword")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}