using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyATM.Startup))]
namespace MyATM
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
