using ApplicationLogic;
using ApplicationLogic.Users.Messages;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using AspNetCore.Identity.MongoDbCore.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using JobBoard.Service.Configs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace JobBoard
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }
        private ILogger<DefaultCorsPolicyService> _loggerFactory { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            DIConfig.Configure(services, Configuration);
            AutoMapperConfig.Configure(services);

            AddIdentity(services, Configuration);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            services.AddAuthentication("Bearer").AddIdentityServerAuthentication(options =>
            {
                options.Authority = Configuration["Authority"].ToString();
                options.RequireHttpsMetadata = false;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Candidate",
                    policy => policy.RequireClaim("scope", "Candidate"));
                options.AddPolicy("Company",
                    policy => policy.RequireClaim("scope", "Company"));
            });

            services.AddCors();

            return services.BuildServiceProvider(validateScopes: true);
        }
       
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseCors(policy =>
            {
                policy.WithOrigins(
                    "http://localhost:4200",
                    "http://localhost:5000",
                    "http://103.207.37.134:4200",
                    "http://103.207.37.134:5000");

                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowCredentials();
            });

            app.UseAuthentication();

            app.UseMiddleware<ApiMiddleWare>();

            app.UseIdentityServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        private static void AddIdentity(IServiceCollection services, IConfiguration Configuration)
        {
            var mongoSettings = Configuration.GetSection(nameof(MongoDbSettings));
            var settings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
            services.AddSingleton<MongoDbSettings>(settings);
            services.AddIdentity<ApplicationUser, MongoIdentityRole>()
                    .AddMongoDbStores<ApplicationUser, MongoIdentityRole, Guid>(settings.ConnectionString, settings.DatabaseName)
                    .AddDefaultTokenProviders();


            services.Configure(new Action<IdentityOptions>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
            }));
            services.AddIdentityServer()
            .AddDeveloperSigningCredential()
            .AddInMemoryPersistedGrants()
            .AddInMemoryApiResources(Resources.GetApiResources())
            .AddInMemoryClients(Clients.Get())
            .AddAspNetIdentity<ApplicationUser>()
            .AddSecretParser<JwtBearerClientAssertionSecretParser>()
            .AddSecretValidator<PrivateKeyJwtSecretValidator>();
        }

    }
}
