using Dapper;
using Sica.Assets.Borders.Entities;
using Sica.Assets.Borders.Repositories;
using Sica.Assets.Borders.Repositories.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Sica.Assets.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly IRepositoryHelper helper;

        public AssetRepository(IRepositoryHelper helper)
        {
            this.helper = helper;
        }


        public async Task<Asset> Create(Asset asset)
        {
            const string sql = @"INSERT INTO assets (id, model, description, purchased_at, maintenance_on) 
                             VALUES (@Id, @Model, @Description, @PurchasedAt, @MaintenanceOn)";

            using var connection = helper.GetConnection();

            await connection.QueryAsync(sql, SqlParameters(asset));
            
            return asset;
        }

        public async Task<Asset> Get(Guid id)
        {
            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Guid);
            return (await InternalList(" WHERE id = @Id", param)).FirstOrDefault();
        }

        public async Task<IEnumerable<Asset>> List()
        {
            return await InternalList();
        }

        public async Task<Asset> Update(Asset asset)
        {
            const string sql = @"UPDATE assets SET model = @Model, description = @Description, purchased_at = @PurchasedAt, 
                                maintenance_on = @MaintenanceOn WHERE id = @Id";

            using var connection = helper.GetConnection();

            await connection.QueryAsync(sql, SqlParameters(asset));

            return asset;
        }

        public async Task Delete(Guid id)
        {
            const string sql = @"DELETE FROM assets WHERE id = @Id";

            using var connection = helper.GetConnection();
            await connection.QueryAsync<bool>(sql, new { Id = id });
        }

        #region Private methods
        private DynamicParameters SqlParameters(Asset asset)
        {
            var param = new DynamicParameters();
            param.Add("@Id", asset.Id, DbType.Guid);
            param.Add("@Model", asset.Model, DbType.String);
            param.Add("@Description", asset.Description, DbType.String);
            param.Add("@PurchasedAt", asset.PurchasedAt, DbType.DateTime);
            param.Add("@MaintenanceOn", asset.MaintenanceOn, DbType.DateTime);

            return param;
        }

        private async Task<IEnumerable<Asset>> InternalList(string where = null, object param = null)
        {
            const string sql = @"SELECT id, model, description, purchased_at purchasedAt, maintenance_on maintenanceOn FROM assets ";

            using var connection = helper.GetConnection();
            return await connection.QueryAsync<Asset>(sql, param: param);
        }
        #endregion
    }
}
