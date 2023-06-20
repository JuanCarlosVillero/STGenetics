
namespace STGenetics.Domain.Abstraction
{
    using STGenetics.Domain.Model;

    public interface IOrderDomainServices
    {
        Task<OrderModel> SaveAsync(OrderModel orderModel);
    }
}
