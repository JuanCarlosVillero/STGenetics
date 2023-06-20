
namespace STGenetics.ApplicationServices
{
    using STGenetics.Application.Abstraction.Queries;
    using STGenetics.Application.Abstraction.Services;
    using STGenetics.Application.Request;
    using STGenetics.Domain.Abstraction;
    using STGenetics.Domain.ErrorHandling;
    using STGenetics.Domain.Model;
    using static STGenetics.Domain.ErrorHandling.MessageHandler;

    public class OrderApplicationServices : IOrderApplicationServices
    {
        private readonly IOrderDomainServices orderDomainServices;

        public OrderApplicationServices(IOrderDomainServices orderDomainServices)
        {
            this.orderDomainServices = orderDomainServices;
        }

        public async Task<long> SaveAsync(OrderRequest orderRequest)
        {
            OrderModel orderModel = new OrderModel();
            orderModel.OrderId = orderRequest.OrderId;
            orderModel.Total = orderRequest.Total;
            orderModel.PurchaseDate = orderRequest.PurchaseDate;
            orderModel.Client = orderRequest.Client;

            if (!orderRequest.OrderLinesRequest.Any())
            {
                throw new BusinessException(MessageCodes.ORDER_HAS_NO_ORDERLINE_VALIDATION,
                    GetErrorDescription(MessageCodes.ORDER_HAS_NO_ORDERLINE_VALIDATION));
            }

            foreach (var orderLine in orderRequest.OrderLinesRequest)
            {
                OrderLineModel orderLineModel = new OrderLineModel();
                orderLineModel.OrderLineId = orderLine.OrderLineId;
                orderLineModel.OrderId = orderLine.OrderId;
                orderLineModel.AnimalId = orderLine.AnimalId;
                orderLineModel.Quantity = orderLine.Quantity;
                orderLineModel.TotalLine = orderLine.TotalLine;

                orderModel.OrderLines.Add(orderLineModel);
            }

            orderModel = await this.orderDomainServices.SaveAsync(orderModel);
            return orderModel.OrderId;
        }
    }
}
