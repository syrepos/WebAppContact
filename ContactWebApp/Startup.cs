using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ContactWebApp.Startup))]
namespace ContactWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
