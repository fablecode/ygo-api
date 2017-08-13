using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.Application;

namespace ygo.api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Redirect root to Swagger UI
            config.Routes.MapHttpRoute(
                name: "Swagger UI",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler(message => message.RequestUri.ToString().TrimEnd('/'), "swagger/ui/index"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            var jsonSerializerSettings = config.Formatters.JsonFormatter.SerializerSettings;

            //Remove unix epoch date handling, in favor of ISO
            jsonSerializerSettings.Converters.Add(new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff" });

            //Remove nulls from payload and save bytes
            jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;

            // Indenting
            jsonSerializerSettings.Formatting = Formatting.Indented;

            // Make json output camelCase
            jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
