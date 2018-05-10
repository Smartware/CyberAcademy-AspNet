using CyberAcademy.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyberAcademy.Web.DataAccess
{
    public class AcademyDbContext:IdentityDbContext<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public AcademyDbContext(): base($"name={nameof(AcademyDbContext)}")
        {

        }
    }
}