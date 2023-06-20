
namespace STGenetics.DomainServices.Abstraction.Repository
{
    using STGenetics.Domain.Model;
    public interface IOrderLineRepository
    {
        Task<OrderLineModel> AddAsync(OrderLineModel orderLineModel);

        Task<OrderLineModel> UpdateAsync(OrderLineModel orderLineModel);

        Task DeleteAsync(long orderLineId);
    }
}
