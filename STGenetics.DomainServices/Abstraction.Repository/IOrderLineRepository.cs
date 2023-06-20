using STGenetics.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGenetics.DomainServices.Abstraction.Repository
{
    public interface IOrderLineRepository
    {
        Task<OrderLineModel> AddAsync(OrderLineModel orderLineModel);

        Task<OrderLineModel> UpdateAsync(OrderLineModel orderLineModel);

        Task DeleteAsync(long orderLineId);
    }
}
