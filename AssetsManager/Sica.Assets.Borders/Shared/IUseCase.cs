using System.Threading.Tasks;

namespace Sica.Assets.Borders.Shared
{
    public interface IUseCase<TRequest, TResponse>
    {
        Task<TResponse> Execute(TRequest request);
    }
}
