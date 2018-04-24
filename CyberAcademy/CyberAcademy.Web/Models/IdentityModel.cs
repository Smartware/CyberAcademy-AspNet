using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyberAcademy.Web.Models
{
    public class AppUser: IdentityUser<Guid, AppUserLogin, AppUserRole, AppUserClaim>
    {
    }

    public class AppRole: IdentityRole<Guid, AppUserRole>
    {

    }
    public class AppUserLogin: IdentityUserLogin<Guid>
    {
    }

    public class AppUserRole: IdentityUserRole<Guid>
    {

    }

    public class AppUserClaim: IdentityUserClaim<Guid>
    {

    }
}