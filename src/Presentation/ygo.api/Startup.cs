using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ygo.api.Startup))]
namespace ygo.api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Configure(app);
        }
    }
}
