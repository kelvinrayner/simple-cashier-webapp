using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppBootcamp32.Startup))]
namespace WebAppBootcamp32
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
