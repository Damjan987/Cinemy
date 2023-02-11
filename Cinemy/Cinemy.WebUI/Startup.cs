using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cinemy.WebUI.Startup))]
namespace Cinemy.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
