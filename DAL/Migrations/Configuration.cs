using DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            var rolesManager = new RoleManager<CustomRole, int>(new RoleStore<CustomRole, int, CustomUserRole>(context));
            try
            {
                var role2 = rolesManager.FindByName("SuperAdministrator");
                if (role2 == null)
                {
                    role2 = new CustomRole
                    {
                        Name = "SuperAdministrator"
                    };
                    rolesManager.Create(role2);
                }
                var role3 = rolesManager.FindByName("RegularUser");
                if (role3 == null)
                {
                    role3 = new CustomRole
                    {
                        Name = "RegularUser"
                    };
                    rolesManager.Create(role3);
                }
                var role4 = rolesManager.FindByName("CustomerAdministrator");
                if (role4 == null)
                {
                    role4 = new CustomRole
                    {
                        Name = "CustomerAdministrator"
                    };
                    rolesManager.Create(role4);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var k = ex.Message;
            }
            finally
            {
                rolesManager.Dispose();
            }
        }

        private void SeedUsers(ApplicationDbContext context)
        {
            var userManager = new UserManager<User, int>(new UserStore<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>(context))
            {
                PasswordValidator =
                    new PasswordValidator
                    {
                        RequireNonLetterOrDigit = false,
                        RequireUppercase = false,
                        RequireLowercase = false
                    }
            };
            try
            {
                var user1 = userManager.FindByEmail("adminsitrator@reportit.com");
                if (user1 == null)
                {
                    user1 = new User
                    {
                        Email = "administrator@reportit.com",
                        UserName = "administrator@reportit.com",
                        EmailConfirmed = true
                    };
                    userManager.Create(user1, "reportit25");
                }
                if (!userManager.IsInRole(user1.Id, "SuperAdministrator"))
                {
                    userManager.AddToRole(user1.Id, "SuperAdministrator");
                }
                var user2 = userManager.FindByEmail("alavalle@reportit.com");
                if (user2 == null)
                {
                    user2 = new User
                    {
                        Email = "alavalle@reportit.com",
                        UserName = "alavalle@reportit.com",
                        EmailConfirmed = true
                    };
                    userManager.Create(user2, "reportit50");
                }
                if (!userManager.IsInRole(user2.Id, "SuperAdministrator"))
                {
                    userManager.AddToRole(user2.Id, "SuperAdministrator");
                }
                var user3 = userManager.FindByEmail("rsobieski@reportit.com");
                if (user3 == null)
                {
                    user3 = new User
                    {
                        Email = "rsobieski@reportit.com",
                        UserName = "rsobieski@reportit.com",
                        EmailConfirmed = true
                    };
                    userManager.Create(user3, "reportit99");
                }
                if (!userManager.IsInRole(user3.Id, "SuperAdministrator"))
                {
                    userManager.AddToRole(user3.Id, "SuperAdministrator");
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                userManager.Dispose();
            }
        }
        protected override void Seed(DAL.ApplicationDbContext context)
        {
            SeedRoles(context);
            SeedUsers(context);
        }
    }
}
