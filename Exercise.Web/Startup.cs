using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Exercise.Web.Startup))]
namespace Exercise.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
