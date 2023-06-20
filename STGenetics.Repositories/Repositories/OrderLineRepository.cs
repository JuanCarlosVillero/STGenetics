
namespace STGenetics.Repositories.Repositories
{
    using Dapper;
    using STGenetics.Domain.Model;
    using STGenetics.DomainServices.Abstraction.Repository;
    using System.Data;
    using System.Data.SqlClient;

    public class OrderLineRepository : IOrderLineRepository
    {
        private IDbConnection db;

        public OrderLineRepository(string connString)
        {
            this.db = new SqlConnection(connString);
        }

        public async Task<OrderLineModel> AddAsync(OrderLineModel orderLineModel)
        {
            var sql =
                "INSERT INTO [dbo].[OrderLine]([OrderId], [AnimalId], [Quantity], [Discount], [TotalLine]) VALUES(@OrderId, @AnimalId, @Quantity, @Discount, @TotalLine); " +
                "SELECT CAST(SCOPE_IDENTITY() AS INT)";

            var id = await this.db.QueryAsync<int>(sql, orderLineModel);
            orderLineModel.OrderLineId = id.Single();
            return orderLineModel;
        }

        public async Task DeleteAsync(long orderLineId)
        {
            await this.db.ExecuteAsync("DELETE FROM OrderLine WHERE OrderLineId = @OrderLineId", new { OrderLineId = orderLineId });
        }

        public async Task<OrderLineModel> UpdateAsync(OrderLineModel orderLineModel)
        {
            var sql =
                "UPDATE [dbo].[OrderLine] " +
                "SET    OrderId = @OrderId, " +
                "       AnimalId = @AnimalId, " +
                "       Quantity = @Quantity, " +
                "       Discount = @Discount, " +
                "       TotalLine = @TotalLine " +
                "WHERE AnimalId = @AnimalId";

            await this.db.ExecuteAsync(sql, orderLineModel);
            return orderLineModel;
        }
    }
}
