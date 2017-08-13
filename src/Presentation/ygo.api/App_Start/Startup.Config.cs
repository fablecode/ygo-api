using System;
using System.IO;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;

namespace ygo.api
{
    public partial class Startup
    {
        public void Configure(IAppBuilder app)
        {
            var config = new HttpConfiguration();

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

            app.UseWebApi(config);

            config.EnableSwagger(c =>
            {
                // Use "SingleApiVersion" to describe a single version API. Swagger 2.0 includes an "Info" object to
                // hold additional metadata for an API. Version and title are required but you can also provide
                // additional fields by chaining methods off SingleApiVersion.
                //
                c.SingleApiVersion("v1", "ygo.api");

                // If you want the output Swagger docs to be indented properly, enable the "PrettyPrint" option.
                //
                c.PrettyPrint();

                // Set this flag to omit descriptions for any actions decorated with the Obsolete attribute
                c.IgnoreObsoleteActions();

                // If you annotate Controllers and API Types with
                // Xml comments (http://msdn.microsoft.com/en-us/library/b2s063f7(v=vs.110).aspx), you can incorporate
                // those comments into the generated docs and UI. You can enable this by providing the path to one or
                // more Xml comment files.

                //Set the comments path for the swagger json and ui.
                c.IncludeXmlComments(GetXmlCommentsPath());
            })
            .EnableSwaggerUi(c =>
            {
                // Use the "DocumentTitle" option to change the Document title.
                // Very helpful when you have multiple Swagger pages open, to tell them apart.
                //
                c.DocumentTitle("Ygo Api");
            });
        }

        private string GetXmlCommentsPath()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(basePath, @"bin\ygo.api.xml");
        }
    }
}