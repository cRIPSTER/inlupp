using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Inlämningsuppgift_MVC.Startup))]
namespace Inlämningsuppgift_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
