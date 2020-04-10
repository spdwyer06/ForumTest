using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ForumTest.Startup))]
namespace ForumTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
