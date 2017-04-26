using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BLL.Helpers
{
    public class EmailBodyHelper
    {
        public static string GetUserCreatedBody(User user, string link)
        {
            return String.Format(@"A new user account for {0} has been created<br/>
                                You can now create a password by following this <a href=""{1}"">link</a>. This link will expire in 10 days.<br/>", user.Email, link);
        }

        public static string GetForgotPasswordBody(string link)
        {
            return String.Format(@"You must now create a new password by following this <a href=""{0}"">link</a>. This link will expire in 10 days.<br/>
                                If you did not request a password reset, please contact your system administrator.<br/><br/>", link);
        }

        public static string GetContactUsBody(string name, string email, string comments)
        {
            return String.Format(@"Name: {0}<br/>
                                Email: {1}<br/>
                                Comments: {2}", name, email, comments);
        }
    }
}
