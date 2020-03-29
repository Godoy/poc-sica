using Sica.Assets.Borders.Entities;
using Sica.Assets.Borders.Shared;
using System.Collections.Generic;

namespace Sica.Assets.Borders.UseCases.Assets
{
    public interface IListAssetsUseCase : IUseCaseOnlyResponse<UseCaseResponse<IEnumerable<Asset>>>
    {
    }
}
