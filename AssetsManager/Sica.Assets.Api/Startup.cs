using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Serilog;
using Sica.Assets.Configurations;
using Sica.Assets.Extensions;
using Sica.Assets.Models;
using System.Globalization;
using System.IO.Compression;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Sica.Assets
{
    public class Startup
    {
        private readonly IHostEnvironment Env;
        private readonly IConfiguration Configuration;
        private readonly string CorsPolicy = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Env = env;
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            Log.Information("Assets service started.");
        }

        private bool IsDevEnvironment
        {
            get => Env.IsDevelopment() || Env.IsEnvironment("Local");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var applicationConfig = Configuration.LoadConfiguration();
           
            services.AddSingleton(applicationConfig);
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IActionResultConverter, ActionResultConverter>();

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });

            if (applicationConfig.CorsOrigins?.Length > 0)
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(CorsPolicy,
                    builder =>
                    {
                        builder.SetIsOriginAllowedToAllowWildcardSubdomains()
                        .WithOrigins(applicationConfig.CorsOrigins)
                        .AllowAnyHeader()
                        .WithExposedHeaders("X-Total-Count")
                        .AllowAnyMethod();
                    });
                });
            }

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sica.Assets API v1", Version = "v1" });
            });

            RepositoryConfig.ConfigureServices(services, applicationConfig);
            ValidatorConfig.ConfigureServices(services);
            UseCaseConfig.ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (IsDevEnvironment)
                app.UseDeveloperExceptionPage();

            //var supportedCultures = new[] { new CultureInfo("pt-BR") };
            //app.UseRequestLocalization(new RequestLocalizationOptions
            //{
            //    DefaultRequestCulture = new RequestCulture("pt-BR"),
            //    SupportedCultures = supportedCultures,
            //    SupportedUICultures = supportedCultures,
            //});

            app.UseSerilogRequestLogging();
            app.UseStaticFiles();
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            }).UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sica.Assets API v1");
                c.RoutePrefix = "swagger";
            });

            app.UseRouting();
            app.UseCors(CorsPolicy);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            Log.Information($"{Assembly.GetExecutingAssembly().GetName().Name} started");
        }
    }
}
