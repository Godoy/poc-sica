using Microsoft.Extensions.Configuration;
using Sica.Assets.Shared.Configurations;

namespace Sica.Assets.Extensions
{
    public static class ConfigurationExtensions
    {
        public static ApplicationConfig LoadConfiguration(this IConfiguration source)
        {
            var applicationConfig = source.Get<ApplicationConfig>();

            applicationConfig.Database.ConnectionString = source.GetConnectionString("DefaultConnection");
            applicationConfig.Database.DbFactoryName = source.GetValue<string>("Database:DbFactoryName");
            applicationConfig.Database.AssemblyName = source.GetValue<string>("Database:AssemblyName");
            applicationConfig.CorsOrigins = source.GetSection("CorsOrigins").Get<string[]>();

            applicationConfig.Product.ProtocolNumber = source.GetValue<string>("Product:ProtocolNumber");

            return applicationConfig;
        }
    }
}
