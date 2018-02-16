using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Abeced.UI.Web.Startup))]
namespace Abeced.UI.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
