
namespace STGenetics.Application.Request
{
    public class OrderRequest
    {
        public long OrderId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Client { get; set; } = default!;

        public List<OrderLineRequest> OrderLinesRequest { get; set; } = new List<OrderLineRequest>();
    }
}
