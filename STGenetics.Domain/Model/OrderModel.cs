namespace STGenetics.Domain.Model
{
    public class OrderModel
    {
        public long OrderId { get; set; }
        public decimal Total { get; set; }
        public decimal Freight { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Client { get; set; } = default!;

        public bool IsNew => this.OrderId == default(long);

        public List<OrderLineModel> OrderLines { get; set; } = new List<OrderLineModel>();
    }
}
