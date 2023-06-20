
namespace STGenetics.Application.Abstraction.Services
{
    using STGenetics.Application.Request;

    public interface IOrderApplicationServices
    {
        Task<long> SaveAsync(OrderRequest orderRequest);
    }
}
