using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MentorWeb.Startup))]
namespace MentorWeb
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
