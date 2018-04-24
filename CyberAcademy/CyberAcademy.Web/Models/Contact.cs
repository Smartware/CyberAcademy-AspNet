using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyberAcademy.Web.Models
{
    public class Contact
    {
        private Contact()
        {

        }

        public static Contact Create(string username, DateTime createdOn)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException("username is empty");


            return new Contact()
            {
                UserName = username,
                Name = "Prolifik Lexzy",
                Email = username,
                IsActive = true,
                EmailConfirmed = true,
                CreatedOn = createdOn
            };
        }

        public string ProfileImage { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public bool IsActive { get; private set; }
    }
}