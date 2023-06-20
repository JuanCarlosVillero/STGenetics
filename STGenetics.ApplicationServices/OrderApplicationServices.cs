
namespace STGenetics.ApplicationServices
{
    using STGenetics.Application.Abstraction.Queries;
    using STGenetics.Application.Abstraction.Services;
    using STGenetics.Application.Request;
    using STGenetics.Application.Response;
    using STGenetics.Domain.Abstraction;
    using STGenetics.Domain.ErrorHandling;
    using STGenetics.Domain.Model;
    using static STGenetics.Domain.ErrorHandling.MessageHandler;

    public class OrderApplicationServices : IOrderApplicationServices
    {
        private const int ORDER_LINE_QUANTITY_GREATER_FIFTY = 50;
        private const decimal ORDER_LINE_QUANTITY_GREATER_FIFTY_DESCOUNT = 0.05M;
        private const decimal NO_DICOUNT = 0M;
        private const int BUY_MORE_TWO_HUNDRED_ANIMALS = 200;
        private const int BUY_MORE_THREE_HUNDRED_ANIMALS = 300;
        private const decimal BUY_MORE_TWO_HUMDRED_ANIMALS_DISCOUNT = 0.03M;
        private const decimal CHARGE_FREIGHT = 1000.00M;
        private const decimal FREE_FREIGHT = 0.00M;
        private const int REPEAT_ANIMALS = 1;
        
        private readonly IOrderDomainServices orderDomainServices;
        private readonly IAnimalQueries animalQueries;

        public OrderApplicationServices(IOrderDomainServices orderDomainServices,
            IAnimalQueries animalQueries)
        {
            this.orderDomainServices = orderDomainServices;
            this.animalQueries = animalQueries;
        }

        public async Task<OrderResponse> SaveAsync(OrderRequest orderRequest)
        {
            ApplyValidations(orderRequest);

            OrderModel orderModel = MappingOrderRequestToModel(orderRequest);
            await ApplyDiscounts(orderModel);

            orderModel = await this.orderDomainServices.SaveAsync(orderModel);

            var orderResponse = new OrderResponse()
            {
                OrderId = orderModel.OrderId,
                TotalPurchaseAmount = orderModel.Total
            };

            return orderResponse;
        }

        private static void ApplyValidations(OrderRequest orderRequest)
        {
            if (!orderRequest.OrderLinesRequest.Any())
            {
                throw new BusinessException(MessageCodes.ORDER_HAS_NO_ORDERLINE_VALIDATION,
                    GetErrorDescription(MessageCodes.ORDER_HAS_NO_ORDERLINE_VALIDATION));
            }

            var repeatAnimals = orderRequest.OrderLinesRequest.GroupBy(x => x.AnimalId)
                .Where(y => y.Count() > REPEAT_ANIMALS)
                .Select(z => z.Key)
                .ToList();
            if (repeatAnimals != null && repeatAnimals.Any())
            {
                throw new BusinessException(MessageCodes.ORDER_HAS_DUPLICATE_ANIMALS_VALIDATION,
                    GetErrorDescription(MessageCodes.ORDER_HAS_DUPLICATE_ANIMALS_VALIDATION));
            }
        }

        private async Task ApplyDiscounts(OrderModel orderModel)
        {
            foreach (var orderLineModel in orderModel.OrderLines)
            {
                var animal = await this.animalQueries.GetAnimalByIdAsync(orderLineModel.AnimalId);
                if (animal == null)
                {
                    throw new BusinessException(MessageCodes.ANIMAL_DOES_NO_EXIST_VALIDATION,
                    GetErrorDescription(MessageCodes.ANIMAL_DOES_NO_EXIST_VALIDATION));
                }

                if (orderLineModel.Quantity > ORDER_LINE_QUANTITY_GREATER_FIFTY)
                {
                    orderLineModel.Discount = ORDER_LINE_QUANTITY_GREATER_FIFTY_DESCOUNT;
                }
                else
                {
                    orderLineModel.Discount = NO_DICOUNT;
                }

                orderLineModel.TotalLine = (animal.Price - (animal.Price * orderLineModel.Discount)) * orderLineModel.Quantity;
                orderModel.Total += orderLineModel.TotalLine;
            }

            if (orderModel.OrderLines.Count() > BUY_MORE_TWO_HUNDRED_ANIMALS)
            {
                orderModel.Total = orderModel.Total - (orderModel.Total * BUY_MORE_TWO_HUMDRED_ANIMALS_DISCOUNT);
            }

            if (orderModel.OrderLines.Count() > BUY_MORE_THREE_HUNDRED_ANIMALS)
            {
                orderModel.Freight = FREE_FREIGHT;
            }
            else
            {
                orderModel.Freight = CHARGE_FREIGHT;
            }
        }

        private static OrderModel MappingOrderRequestToModel(OrderRequest orderRequest)
        {
            OrderModel orderModel = new OrderModel();
            orderModel.OrderId = orderRequest.OrderId;
            //orderModel.Total = orderRequest.Total;
            orderModel.PurchaseDate = orderRequest.PurchaseDate;
            orderModel.Client = orderRequest.Client;

            foreach (var orderLine in orderRequest.OrderLinesRequest)
            {
                OrderLineModel orderLineModel = new OrderLineModel();
                //orderLineModel.OrderLineId = orderLine.OrderLineId;
                //orderLineModel.OrderId = orderLine.OrderId;
                orderLineModel.AnimalId = orderLine.AnimalId;
                orderLineModel.Quantity = orderLine.Quantity;
                //orderLineModel.TotalLine = orderLine.TotalLine;

                orderModel.OrderLines.Add(orderLineModel);
            }

            return orderModel;
        }
    }
}
