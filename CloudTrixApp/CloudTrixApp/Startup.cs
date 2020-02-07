using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CloudTrixApp.Startup))]
namespace CloudTrixApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
 
