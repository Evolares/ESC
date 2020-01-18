using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ESC.Startup))]
namespace ESC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
