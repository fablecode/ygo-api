using System.Web.Http;
using Owin;

namespace ygo.api
{
    public partial class Startup
    {
        public void Configure(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            app.UseWebApi(config);

            WebApiConfig.Register(config);
            JsonConfig.Register(config);
            SwaggerConfig.Register(config);
        }
    }
}