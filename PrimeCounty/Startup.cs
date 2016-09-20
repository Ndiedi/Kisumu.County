using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PrimeCounty.Startup))]
namespace PrimeCounty
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
