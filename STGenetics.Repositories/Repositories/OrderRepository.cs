
namespace STGenetics.Repositories.Repositories
{
    using Dapper;
    using STGenetics.Domain.Model;
    using STGenetics.DomainServices.Abstraction.Repository;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public class OrderRepository : IOrderRepository
    {
        private IDbConnection db;
        private readonly IOrderLineRepository orderLineRepository;

        public OrderRepository(string connString,
            IOrderLineRepository orderLineRepository)
        {
            this.db = new SqlConnection(connString);
            this.orderLineRepository = orderLineRepository;
        }

        public async Task<OrderModel> AddAsync(OrderModel orderModel)
        {
            var sql =
                "INSERT INTO [dbo].[Order]([Total], [Freight], [PurchaseDate], [Client]) VALUES(@Total, @Freight, @PurchaseDate, @Client); " +
                "SELECT CAST(SCOPE_IDENTITY() AS INT)";

            var id = await this.db.QueryAsync<int>(sql, orderModel);
            orderModel.OrderId = id.Single();
            return orderModel;
        }

        public async Task DeleteAsync(long orderId)
        {
            await this.db.ExecuteAsync("DELETE FROM Order WHERE OrderId = @OrderId", new { OrderId = orderId });
        }

        public async Task<OrderModel> UpdateAsync(OrderModel orderModel)
        {
            var sql =
                "UPDATE [dbo].[Order] " +
                "SET    Total = @Total, " +
                "       Freight = @Freight, " +
                "       PurchaseDate = @PurchaseDate, " +
                "       Client = @Client " +
                "WHERE OrderId = @OrderId";

            await this.db.ExecuteAsync(sql, orderModel);
            return orderModel;
        }

        public async Task<OrderModel> SaveAsync(OrderModel orderModel)
        {
            if(orderModel.IsNew)
            {
                orderModel = await this.AddAsync(orderModel);
            }
            else
            {
                orderModel = await this.UpdateAsync(orderModel);
            }

            foreach (var orderLineModel in orderModel.OrderLines)
            {
                orderLineModel.OrderId = orderModel.OrderId;

                if (orderLineModel.IsNew)
                {
                    await this.orderLineRepository.AddAsync(orderLineModel);
                }
                else
                {
                    await this.orderLineRepository.UpdateAsync(orderLineModel);
                }
            }

            return orderModel;
        }
    }
}
