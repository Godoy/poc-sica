using Sica.Assets.Borders.Entities;
using Sica.Assets.Borders.Shared;
using System;

namespace Sica.Assets.Borders.UseCases.Assets
{
    public interface IGetAssetUseCase : IUseCase<Guid, UseCaseResponse<Asset>>
    {
    }
}
