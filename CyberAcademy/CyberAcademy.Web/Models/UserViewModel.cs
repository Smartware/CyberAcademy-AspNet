using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CyberAcademy.Web.Models
{
    public class UserViewModel
    {
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
        public string RPassword { get; set; }
        public string Email { get; set; }

        public int RoleId { get; set; }
    }

}