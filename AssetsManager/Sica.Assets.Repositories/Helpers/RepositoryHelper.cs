using Sica.Assets.Borders.Repositories.Helpers;
using Sica.Assets.Repositories.Factories;
using Sica.Assets.Shared.Configurations;
using System.Data;
using System.Data.Common;

namespace Sica.Assets.Repositories.Helpers
{
    public class RepositoryHelper : IRepositoryHelper
    {
        private readonly DbProviderFactory dbProviderFactory;
        private readonly string connectionString;

        public RepositoryHelper(ApplicationConfig configuration)
        {
            dbProviderFactory = DatabaseFactory.GetDbProviderFactory(configuration.Database.DbFactoryName, configuration.Database.AssemblyName);
            connectionString = configuration.Database.ConnectionString;
        }

        public IDbConnection GetConnection()
        {
            var connection = dbProviderFactory.CreateConnection();
            connection.ConnectionString = connectionString;

            return connection;
        }

    }
}
