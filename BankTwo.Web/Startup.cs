using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BankTwo.Web.Startup))]
namespace BankTwo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
