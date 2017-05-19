using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WxWeb.Startup))]
namespace WxWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
