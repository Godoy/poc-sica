using Microsoft.Extensions.Logging;
using Sica.Assets.Borders.Entities;
using Sica.Assets.Borders.Repositories;
using Sica.Assets.Borders.Shared;
using Sica.Assets.Borders.UseCases.Assets;
using System;
using System.Threading.Tasks;

namespace Sica.Assets.UseCases.Policy
{
    public class GetAssetUseCase : IGetAssetUseCase
    {
        private readonly IAssetRepository assetRepository;
        private readonly ILogger<CreateAssetUseCase> logger;

        public GetAssetUseCase(IAssetRepository assetRepository,
            ILogger<CreateAssetUseCase> logger)
        {
            this.assetRepository = assetRepository;
            this.logger = logger;
        }

        public async Task<UseCaseResponse<Asset>> Execute(Guid id)
        {
            var response = new UseCaseResponse<Asset>();
            try
            {
                var asset = await assetRepository.Get(id);
                return response.SetResult(asset);              
            }            
            catch (Exception e)
            {                
                logger.LogError(e.Message, e);
                return response.SetInternalServerError("Unexpected error: "+ e.Message);
            }
        }
    }
}
