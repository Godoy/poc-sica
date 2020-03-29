using System.Threading.Tasks;

namespace Sica.Assets.Borders.Shared
{
    public interface IUseCaseOnlyResponse<TResponse>
    {
        Task<TResponse> Execute();
    }
}
