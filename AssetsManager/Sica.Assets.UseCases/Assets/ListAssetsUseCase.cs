using Microsoft.Extensions.Logging;
using Sica.Assets.Borders.Entities;
using Sica.Assets.Borders.Repositories;
using Sica.Assets.Borders.Shared;
using Sica.Assets.Borders.UseCases.Assets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sica.Assets.UseCases.Policy
{
    public class ListAssetsUseCase : IListAssetsUseCase
    {
        private readonly IAssetRepository assetRepository;
        private readonly ILogger<CreateAssetUseCase> logger;

        public ListAssetsUseCase(IAssetRepository assetRepository,
            ILogger<CreateAssetUseCase> logger)
        {
            this.assetRepository = assetRepository;
            this.logger = logger;
        }

        public async Task<UseCaseResponse<IEnumerable<Asset>>> Execute()
        {
            var response = new UseCaseResponse<IEnumerable<Asset>>();
            try
            {
                var assets = await assetRepository.List();
                return response.SetResult(assets);              
            }            
            catch (Exception e)
            {                
                logger.LogError(e.Message, e);
                return response.SetInternalServerError("Unexpected error: "+ e.Message);
            }
        }
    }
}
