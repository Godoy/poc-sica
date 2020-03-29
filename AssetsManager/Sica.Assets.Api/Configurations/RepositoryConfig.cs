using Microsoft.Extensions.DependencyInjection;
using Sica.Assets.Borders.Repositories;
using Sica.Assets.Borders.Repositories.Helpers;
using Sica.Assets.Repositories;
using Sica.Assets.Repositories.Helpers;
using Sica.Assets.Shared.Configurations;

namespace Sica.Assets.Configurations
{
    public static class RepositoryConfig
    {
        public static void ConfigureServices(IServiceCollection services, ApplicationConfig applicationConfig)
        {
            services.AddSingleton<IAssetRepository, AssetRepository>();
            services.AddSingleton<IRepositoryHelper, RepositoryHelper>();            
        }
    }
}