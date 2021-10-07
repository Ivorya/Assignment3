using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SportDay.Startup))]
namespace SportDay
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
