using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Ninject;
using ReportIt.App_Start;
using BLL.Identity;
using DAL.Interfaces;
using DAL.Models;
using System.Collections.Generic;

namespace UI.Core.Controllers
{
    public class BaseController : Controller
    {
        private Dictionary<string, CustomRole> _cachedRoles;
        private ApplicationUserManager _userManager;
        protected readonly IUnitOfWork UnitOfWork;

        protected BaseController(IUnitOfWork iUnitOfWork, ApplicationUserManager userManager)
        {
            UnitOfWork = iUnitOfWork;
            UserManager = userManager;
            _cachedRoles = new Dictionary<string, CustomRole>();
        }

        protected ApplicationUserManager UserManager
        {
            get
            {
                if (_userManager != null)
                {
                    return _userManager;
                }
                _userManager = NinjectWebCommon.Kernel.Get<ApplicationUserManager>();
                return _userManager;
            }
            private set
            {
                _userManager = value;
            }
        }

        private User _currentUser;
        private bool _isUserInitialized;

        public User CurrentUser
        {
            get
            {
                if (!_isUserInitialized && Request.IsAuthenticated)
                {
                    _currentUser = UserManager.FindById(int.Parse(User.Identity.GetUserId()));
                    if (_currentUser == null)
                    {
                        HttpContext.GetOwinContext().Authentication.SignOut(User.Identity.AuthenticationType);
                    }
                    else
                    {
                        ViewBag.LayoutModel = _currentUser;
                        _isUserInitialized = true;
                    }
                }
                return _currentUser;
            }
        }

        public CustomRole GetRoleByName(string roleName)
        {
            if (!_cachedRoles.ContainsKey(roleName))
                _cachedRoles.Add(roleName, UnitOfWork.RoleRepository.GetAll().FirstOrDefault(role => role.Name.Equals(roleName)));

            return _cachedRoles[roleName];
        }
    }
}