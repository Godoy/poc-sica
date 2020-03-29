using FluentValidation;
using Microsoft.Extensions.Logging;
using Sica.Assets.Borders.Entities;
using Sica.Assets.Borders.Repositories;
using Sica.Assets.Borders.Shared;
using Sica.Assets.Borders.UseCases.Assets;
using Sica.Assets.Shared.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sica.Assets.UseCases.Policy
{
    public class UpdateAssetUseCase : IUpdateAssetUseCase
    {
        private readonly IAssetRepository assetRepository;
        private readonly ILogger<CreateAssetUseCase> logger;

        public UpdateAssetUseCase(IAssetRepository assetRepository,
            ILogger<CreateAssetUseCase> logger)
        {
            this.assetRepository = assetRepository;
            this.logger = logger;
        }

        public async Task<UseCaseResponse<Asset>> Execute(Asset request)
        {
            var response = new UseCaseResponse<Asset>();
            try
            {
                var updatedAsset = await assetRepository.Update(request);

                return response.SetCreated(updatedAsset);
            }
            catch (ValidationException ex)
            {
                return response.SetBadRequest("Validation exception", ex.ToErrorMessage().ToArray());
            }
            catch (Exception e)
            {                
                logger.LogError(e.Message, e);
                return response.SetInternalServerError("Unexpected error: "+ e.Message);
            }
        }
    }
}
