using Owin;
using BankTwo.Application.Logic;

namespace BankTwo.Web
{
    public partial class Startup
    {
       // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            configureAuth.ConfigureAuth(app);
        }
    }

}