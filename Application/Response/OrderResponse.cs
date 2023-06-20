using STGenetics.Application.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGenetics.Application.Response
{
    public class OrderResponse
    {
        public long OrderId { get; set; }
        public decimal TotalPurchaseAmount { get; set; }
    }
}
