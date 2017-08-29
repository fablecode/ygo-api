using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ygo.api.Auth
{
    public static class AuthConfig
    {
        public const string SuperAdminRole = "SuperAdmin";
        public const string AdminRole = "Admin";
        public const string UserRole = "User";

        public const string SuperAdminsPolicy = "SuperAdminsOnly";
        public const string AdminsOnlyPolicy = "AdminsOnly";

        public const string YgoDatabase = "ygo";

        public static IServiceCollection AddTokenAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            return AddTokenAuthenticationServices(services, configuration, YgoDatabase);
        }

        public static IServiceCollection AddTokenAuthenticationServices(this IServiceCollection services, IConfiguration configuration, string connectionStringName)
        {
            AddPolicies(services);
            AddIdentity(services);
            AddIdentityDbContext(services, configuration.GetConnectionString(connectionStringName));
            AddTokenAuthentication(services, configuration);

            return services;
        }

        public static IServiceCollection AddPolicies(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(SuperAdminsPolicy, policy => policy.RequireRole(SuperAdminRole));
                options.AddPolicy(AdminsOnlyPolicy, policy => policy.RequireRole(AdminRole));
            });

            return services;
        }
        public static IServiceCollection AddIdentity(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection AddIdentityDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection AddTokenAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(auth =>
                {
                    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;

                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        // Validate the JWT Issuer (iss) claim  
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Tokens:Issuer"],

                        // Validate the JWT Audience (aud) claim  
                        ValidateAudience = true,
                        ValidAudience = configuration["Tokens:Issuer"],

                        // The signing key must match!  
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"])),

                        // Validate the token expiry  
                        ValidateLifetime = true,

                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }
    }
}