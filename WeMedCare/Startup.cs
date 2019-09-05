using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeMedCare.Startup))]
namespace WeMedCare
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
