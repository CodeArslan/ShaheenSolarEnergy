using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShaheenSolarEnergy.Startup))]
namespace ShaheenSolarEnergy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
