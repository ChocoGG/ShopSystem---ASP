using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopSystem.WebUI.Startup))]
namespace ShopSystem.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
