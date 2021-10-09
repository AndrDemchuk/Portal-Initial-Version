using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Infrastructure.Files;
using BvAcademyPortal.Infrastructure.Identity;
using BvAcademyPortal.Infrastructure.Persistence;
using BvAcademyPortal.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

namespace BvAcademyPortal.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("BvAcademyPortalDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IDomainEventService, DomainEventService>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

            services.AddScoped<IProfilePhotoManager, LocalProfilePhotoUploadService>();

            //var adConfiguration = configuration.GetSection("AzureB2C");
            //var tenantId = adConfiguration.GetValue<string>("TenantId");
            //var audience = "https://Blackthorn-vision.com/BVMeter";
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.Authority = $"https://login.microsoftonline.com/{tenantId}";
            //        options.Audience = audience;
            //    });

            // This is required to be instantiated before the OpenIdConnectOptions starts getting configured.
            // By default, the claims mapping will map claim names in the old format to accommodate older SAML applications.
            // 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role' instead of 'roles'
            // This flag ensures that the ClaimsIdentity claims collection will be built from the claims in the token
            // JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            // Adds Microsoft Identity platform (AAD v2.0) support to protect this Api
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(options =>
                    {
                        configuration.Bind("AzureAdB2C", options);

                        options.TokenValidationParameters.NameClaimType = "name";
                    },
                    options => { configuration.Bind("AzureAdB2C", options); });

            //services.Configure<OpenIdConnectOptions>(, options =>
            //{
            //    options.Authority = options.Authority + "/v2.0/";         // Microsoft identity platform
            //    options.TokenValidationParameters.ValidateIssuer = false; // accept several tenants (here simplified)
            //    options.TokenValidationParameters.ValidAudiences = new List<string> { audience };
            //    //     Indicates that the authentication session lifetime(e.g.cookies) should match
            //    //     that of the authentication token. If the token does not provide lifetime information
            //    options.UseTokenLifetime = true;
            //});


            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
            });

            return services;
        }
    }
}