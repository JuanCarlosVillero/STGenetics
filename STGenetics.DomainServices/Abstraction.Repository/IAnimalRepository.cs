
namespace STGenetics.DomainServices.Abstraction.Repository
{
    using STGenetics.Domain.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAnimalRepository
    {
        Task<AnimalModel> Add(AnimalModel animal);
    }
}
