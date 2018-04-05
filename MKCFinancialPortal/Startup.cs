using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MKCFinancialPortal.Startup))]
namespace MKCFinancialPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
