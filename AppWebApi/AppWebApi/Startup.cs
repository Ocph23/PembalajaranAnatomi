using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppWebApi.Startup))]
namespace AppWebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
