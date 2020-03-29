using Sica.Assets.Borders.Dtos.Assets;
using Sica.Assets.Borders.Entities;
using Sica.Assets.Borders.Shared;
using System;

namespace Sica.Assets.Borders.UseCases.Assets
{
    public interface IDeleteAssetUseCase : IUseCase<Guid, UseCaseResponse<bool>>
    {
    }
}
