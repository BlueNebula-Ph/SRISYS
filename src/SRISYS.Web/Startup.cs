namespace Srisys.Web
{
    using System.IdentityModel.Tokens.Jwt;
    using System.IO;
    using AspNet.Security.OpenIdConnect.Primitives;
    using AutoMapper;
    using BlueNebula.Common.Helpers;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.PlatformAbstractions;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Srisys.Web.Models;
    using Srisys.Web.Services;
    using Srisys.Web.Services.Interfaces;
    using Swashbuckle.AspNetCore.Swagger;

    /// <summary>
    /// <see cref="Startup"/> class API configuration.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">Hosting environment</param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        /// <summary>
        /// Gets read-only property configuration <see cref="IConfigurationRoot"/> class.
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "SrisysCorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            // Add framework services.
            services.AddMvc()
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services.AddLogging();

            services.AddAutoMapper();

            services.AddDbContext<SrisysDbContext>(opt =>
            {
                opt.UseInMemoryDatabase();

                opt.UseOpenIddict();
            });

            // Register the Identity services.
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<SrisysDbContext>()
                .AddDefaultTokenProviders();

            // Configure Identity to use the same JWT claims as OpenIddict instead
            // of the legacy WS-Federation claims it uses by default (ClaimTypes),
            // which saves you from doing the mapping in your authorization controller.
            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            services.AddOpenIddict(options =>
                {
                    // Register the Entity Framework stores.
                    options.AddEntityFrameworkCoreStores<SrisysDbContext>();

                    // Register the ASP.NET Core MVC binder used by OpenIddict.
                    // Note: if you don't call this method, you won't be able to
                    // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
                    options.AddMvcBinders();

                    // Enable the token endpoint.
                    options.EnableTokenEndpoint("/connect/token");

                    // Enable the password flow.
                    options.AllowPasswordFlow();

                    // Enable the use of refresh tokens();
                    options.AllowRefreshTokenFlow();

                    // During development, you can disable the HTTPS requirement.
                    options.DisableHttpsRequirement();

                    options.AddEphemeralSigningKey();

                    options.UseJsonWebTokens();
                });

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new Info { Title = "SRISYS API", Version = "v1", Description = "Leased Materials System API" });

                // Set comments path for swagger
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Srisys.Web.xml");
                opt.IncludeXmlComments(xmlPath);
            });

            // Add application services
            services.AddTransient(typeof(ISummaryListBuilder<,>), typeof(SummaryListBuilder<,>));
            services.AddScoped<IAdjustmentService, AdjustmentService>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="env">IHostingEnvironment</param>
        /// <param name="loggerFactory">ILoggerFactory</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Register the CORS middleware.
            app.UseCors("SrisysCorsPolicy");

            // Register the validation middleware, that is used to decrypt
            // the access tokens and populate the HttpContext.User property.
            app.UseOAuthValidation();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                Authority = "http://localhost:55822/",
                Audience = "resource_server",
                RequireHttpsMetadata = false,
                TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = OpenIdConnectConstants.Claims.Subject,
                    RoleClaimType = OpenIdConnectConstants.Claims.Role,
                },
            });

            // Register the openiddict middleware.
            app.UseOpenIddict();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "SRISYS API V1");
                opt.RoutePrefix = "info";
            });

            SrisysDbContext.Seed(app);
        }
    }
}
