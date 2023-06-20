using System;
using System.Collections.Generic;
using System.Linq;

namespace STGenetics.Domain.Model
{
    public class OrderLineModel
    {
        public long OrderLineId { get; set; }
        public long OrderId { get; set; }
        public long AnimalId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalLine { get; set; }

        public bool IsNew => this.OrderLineId == default(long);
    }
}
