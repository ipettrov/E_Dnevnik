using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(E_Dnevnik.Startup))]
namespace E_Dnevnik
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
