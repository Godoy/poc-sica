using Microsoft.Extensions.DependencyInjection;
using Sica.Assets.Borders.UseCases.Assets;
using Sica.Assets.UseCases.Policy;

namespace Sica.Assets.Configurations
{
    public static class UseCaseConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICreateAssetUseCase, CreateAssetUseCase>();
            services.AddSingleton<IListAssetsUseCase, ListAssetsUseCase>();
            services.AddSingleton<IGetAssetUseCase, GetAssetUseCase>();
            services.AddSingleton<IUpdateAssetUseCase, UpdateAssetUseCase>();
            services.AddSingleton<IDeleteAssetUseCase, DeleteAssetUseCase>();

        }
    }
}
