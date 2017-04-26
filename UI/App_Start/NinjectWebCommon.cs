using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using BLL.Identity;
using DAL;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Ninject.Syntax;
using UI;
using ApplicationSignInManager = BLL.Identity.ApplicationSignInManager;
using ApplicationUserManager = BLL.Identity.ApplicationUserManager;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ReportIt.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ReportIt.App_Start.NinjectWebCommon), "Stop")]

namespace ReportIt.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();
        public static IKernel Kernel
        {
            get { return bootstrapper.Kernel; }
        }
        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<DbContext>().To<ApplicationDbContext>().InRequestScope();
            kernel.Bind<ApplicationSignInManager>().ToSelf().InRequestScope();
            kernel.Bind<ApplicationRoleManager>().ToSelf().InRequestScope();
            kernel.Bind<ApplicationUserManager>().ToSelf().InRequestScope();
            kernel.Bind<IDataProtectionProvider>().ToMethod<IDataProtectionProvider>(c =>
            {
                System.Diagnostics.Debug.WriteLine("ninject {0}", Startup.DataProtectionProvider != null);
                return Startup.DataProtectionProvider;
            }).InRequestScope();
            kernel.Bind<UserManager<User, int>>()
                .To<ApplicationUserManager>()
                .InRequestScope();
            //.WithConstructorArgument<IDataProtectionProvider>(Startup.DataProtectionProvider);
            kernel.Bind<RoleManager<CustomRole, int>>().To<ApplicationRoleManager>().InRequestScope();
            kernel.Bind<IUserStore<User, int>>()
                .To<UserStore<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>>()
                .InRequestScope();
            kernel.Bind<IRoleStore<CustomRole, int>>()
                .To<RoleStore<CustomRole, int, CustomUserRole>>()
                .InRequestScope();
            kernel.Bind<IAuthenticationManager>().ToMethod<IAuthenticationManager>(context =>
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            });
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
        }
    }
    // Provides a Ninject implementation of IDependencyScope
    // which resolves services using the Ninject container.
    public class NinjectDependencyScope : IDependencyScope
    {
        IResolutionRoot resolver;

        public NinjectDependencyScope(IResolutionRoot resolver)
        {
            this.resolver = resolver;
        }

        public object GetService(Type serviceType)
        {
            if (resolver == null)
                throw new ObjectDisposedException("this", "This scope has been disposed");

            return resolver.TryGet(serviceType);
        }

        public System.Collections.Generic.IEnumerable<object> GetServices(Type serviceType)
        {
            if (resolver == null)
                throw new ObjectDisposedException("this", "This scope has been disposed");

            return resolver.GetAll(serviceType);
        }

        public void Dispose()
        {
            IDisposable disposable = resolver as IDisposable;
            if (disposable != null)
                disposable.Dispose();

            resolver = null;
        }
    }

    // This class is the resolver, but it is also the global scope
    // so we derive from NinjectScope.
    public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver
    {
        IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            this.kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(kernel.BeginBlock());
        }
    }
}
