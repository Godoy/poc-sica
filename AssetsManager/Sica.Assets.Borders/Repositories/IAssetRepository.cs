using Sica.Assets.Borders.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sica.Assets.Borders.Repositories
{
    public interface IAssetRepository
    {
        Task<Asset> Create(Asset asset);
        Task<Asset> Update(Asset asset);
        Task<Asset> Get(Guid id);
        Task<IEnumerable<Asset>> List();
        Task Delete(Guid id);
    }
}
