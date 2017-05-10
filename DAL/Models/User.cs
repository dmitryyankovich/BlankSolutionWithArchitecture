using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Models
{
    [Table("User")]
    public class User : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public virtual Company Company { get; set; }
        public virtual Resume Resume { get; set; }
        public virtual ICollection<CourseResponse> CourseResponses { get; set; }
    }

    public class Role : IdentityRole
    {

    }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole()
        {

        }
        public CustomRole(string name) { Name = name; }
    }

    public class CustomUserRole : IdentityUserRole<int>
    {
        public virtual CustomRole Role { get; set; }
        public virtual User User { get; set; }
    }

    public class CustomUserClaim : IdentityUserClaim<int> { }

    public class CustomUserLogin : IdentityUserLogin<int>
    {
        public int ExpiresIn { get; set; }
    }
}
