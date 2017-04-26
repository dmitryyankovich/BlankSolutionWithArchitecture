using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BLL.Services;
using DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace BLL.Identity
{
    public class ApplicationUserManager : UserManager<User, int>
    {
        public ApplicationUserManager(IUserStore<User, int> store, IDataProtectionProvider provider)
            : base(store)
        {
            // Configure validation logic for usernames
            this.UserValidator = new UserValidator<User, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                //RequireUniqueEmail = true
            };

            this.PasswordValidator = new CustomPasswordValidator(8);

            this.EmailService = new IdentityEmailService();

            this.UserLockoutEnabledByDefault = true;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(15);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;

            if (provider != null)
            {
                this.UserTokenProvider = new DataProtectorTokenProvider<User, int>(provider.Create("PasswordRestore"))
                {
                    TokenLifespan = TimeSpan.FromDays(10)
                };
            }
            // Register two factor authe ntication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            //manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<User,Guid>
            //{
            //    MessageFormat = "Your security code is {0}"
            //});
            //manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<User,Guid>
            //{
            //    Subject = "Security Code",
            //    BodyFormat = "Your security code is {0}"
            //});
            //manager.EmailService = new EmailService();
            //manager.SmsService = new SmsService();

            //HttpContext.Current.GetOwinContext().Authentication.
            //var dataProtectionProvider = options.DataProtectionProvider;
            //if (dataProtectionProvider != null)
            //{
            //    this.UserTokenProvider =
            //        new DataProtectorTokenProvider<User, Guid>(dataProtectionProvider.Create("ASP.NET Identity"));
            //}
        }
    }

    public class CustomPasswordValidator : IIdentityValidator<string>
    {

        public int RequiredLength { get; set; }

        public CustomPasswordValidator(int length)
        {
            RequiredLength = length;
        }

        public Task<IdentityResult> ValidateAsync(string item)
        {

            if (String.IsNullOrEmpty(item) || item.Length < RequiredLength)
            {
                return Task.FromResult(IdentityResult.Failed(String.Format("Password should be of length {0}", RequiredLength)));

            }

            const string numericPattern = @"\d";
            const string upperCasePattern = @"[A-Z]";
            const string lowerCasePattern = @"[a-z]";
            const string specialSymbolPattern = @"[~!@#$%^&*()_+=\-'[\]\/\?><]";

            var numericPatternMatch = Regex.IsMatch(item, numericPattern);
            var upperCasePatternMatch = Regex.IsMatch(item, upperCasePattern);
            var lowerCasePatternMatch = Regex.IsMatch(item, lowerCasePattern);
            var specialSymbolPatternMatch = Regex.IsMatch(item, specialSymbolPattern);

            if (!(numericPatternMatch && upperCasePatternMatch) && !(numericPatternMatch && lowerCasePatternMatch) && !(numericPatternMatch && specialSymbolPatternMatch)
                && !(upperCasePatternMatch && lowerCasePatternMatch) && !(upperCasePatternMatch && specialSymbolPatternMatch) && !(lowerCasePatternMatch && specialSymbolPatternMatch))
            {
                return Task.FromResult(IdentityResult.Failed("Password must be composed of representatives of at least two of the following character sets: Upper-case; Lower-case; Numeric; Special characters (e.g. ~ ! @ # $ % ^ & * ( ) _ + = - ‘ [] / ? > <)."));
            }
            return Task.FromResult(IdentityResult.Success);

        }
    }
}
