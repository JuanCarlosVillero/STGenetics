
namespace STGenetics.DomainServices
{
    using STGenetics.Domain.Abstraction;
    using STGenetics.Domain.Model;
    using STGenetics.DomainServices.Abstraction.Repository;

    public class OrderDomainServices : IOrderDomainServices
    {
        private readonly IOrderRepository orderRepository;

        public OrderDomainServices(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<OrderModel> SaveAsync(OrderModel orderModel)
        {
            return await this.orderRepository.SaveAsync(orderModel);
        }
    }
}
