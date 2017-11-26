using System;
using System.IO;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using ygo.api.Auth;
using ygo.api.Auth.Swagger;
using ygo.application;
using ygo.application.Ioc;
using ygo.infrastructure.Ioc;

namespace ygo.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApplicationSettings>(Configuration.GetSection("AppSettings"));
            services.AddMvc(setupAction =>
            {
                // 406 Not Acceptable response, if accept header not supported.
                setupAction.ReturnHttpNotAcceptable = true;

                // Xml Formatters support
                setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Ygo API", Version = "v1" });

                // To add an extra token input field in the Swagger UI
                c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();

                var fileName = GetType().GetTypeInfo().Module.Name.Replace(".dll", ".xml").Replace(".exe", ".xml");
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, fileName));
            });

            services.AddTokenAuthenticationServices(Configuration);
            services.AddInfrastructureServices(Configuration.GetConnectionString(AuthConfig.YgoDatabase));
            services.AddApplicationServices();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });
            }

            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ygo API V1");
                c.DocExpansion("none");
            });
            app.UseMvc();
            app.UseRewriter(new RewriteOptions()
                 .AddRedirect(@"^$", "swagger", (int)HttpStatusCode.Redirect));
        }
    }
}
