using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CyberAcademy.Web.Models
{
    public class ContactViewModel
    {
        [Required]
        public string Name { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }


    }
}