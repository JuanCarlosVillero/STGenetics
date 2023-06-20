
namespace STGenetics.Application.Abstraction.Services
{
    using STGenetics.Application.Request;
    using STGenetics.Application.Response;

    public interface IOrderApplicationServices
    {
        Task<OrderResponse> SaveAsync(OrderRequest orderRequest);
    }
}
