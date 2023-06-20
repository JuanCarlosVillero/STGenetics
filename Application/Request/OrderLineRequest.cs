using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGenetics.Application.Request
{
    public class OrderLineRequest
    {
        //public long OrderLineId { get; set; }
        //public long OrderId { get; set; }
        public long AnimalId { get; set; }
        public int Quantity { get; set; }
        //public decimal TotalLine { get; set; }
    }
}
