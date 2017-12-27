using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CryptoWeb.Startup))]
namespace CryptoWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
