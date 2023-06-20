
namespace STGenetics.DomainServices.Abstraction.Repository
{
    using STGenetics.Domain.Model;
    using System.Threading.Tasks;
    public interface IOrderRepository

    {
        Task<OrderModel> AddAsync(OrderModel orderModel);

        Task<OrderModel> UpdateAsync(OrderModel orderModel);

        Task DeleteAsync(long orderId);

        Task<OrderModel> SaveAsync(OrderModel orderModel);
    }
}
