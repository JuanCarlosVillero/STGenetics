
namespace STGenetics.Application.Abstraction.Services
{
    using STGenetics.Application.Response;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAnimalApplicationServices
    {
        Task<IList<AnimalResponse>> GetAllAsync();
    }
}
