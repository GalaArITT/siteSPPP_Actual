using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(siteSPPP.Startup))]
namespace siteSPPP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
