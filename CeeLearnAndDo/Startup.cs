using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CeeLearnAndDo.Startup))]
namespace CeeLearnAndDo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
