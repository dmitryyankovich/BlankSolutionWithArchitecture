using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Owin;

[assembly: OwinStartupAttribute(typeof(UI.Startup))]
namespace UI
{
    public partial class Startup
    {
        internal static IDataProtectionProvider DataProtectionProvider { get; private set; }
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            DataProtectionProvider = app.GetDataProtectionProvider();
        }
    }
}
