using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DelStreet.Startup))]
namespace DelStreet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
