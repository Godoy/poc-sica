using System.Data;

namespace Sica.Assets.Borders.Repositories.Helpers
{
    public interface IRepositoryHelper
    {
        IDbConnection GetConnection();
    }
}
