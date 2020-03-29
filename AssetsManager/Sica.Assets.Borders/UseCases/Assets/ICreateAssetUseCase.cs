using Sica.Assets.Borders.Dtos.Assets;
using Sica.Assets.Borders.Entities;
using Sica.Assets.Borders.Shared;

namespace Sica.Assets.Borders.UseCases.Assets
{
    public interface ICreateAssetUseCase : IUseCase<CreateAssetRequest, UseCaseResponse<Asset>>
    {
    }
}
