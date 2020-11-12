using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LocalSpirits.WebMVC.Startup))]
namespace LocalSpirits.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
