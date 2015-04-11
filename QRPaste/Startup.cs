using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QRPaste.Startup))]
namespace QRPaste
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
