using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IoTWeb.Startup))]
namespace IoTWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
