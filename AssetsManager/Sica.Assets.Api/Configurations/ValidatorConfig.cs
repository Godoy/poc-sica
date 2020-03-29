using Microsoft.Extensions.DependencyInjection;
using Sica.Assets.Borders.Validators;

namespace Sica.Assets.Configurations
{
    public static class ValidatorConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            
            services.AddSingleton<CreateAssetRequestValidator>();            
        }
    }
}
