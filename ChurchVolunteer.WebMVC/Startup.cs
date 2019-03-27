using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChurchVolunteer.WebMVC.Startup))]
namespace ChurchVolunteer.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
