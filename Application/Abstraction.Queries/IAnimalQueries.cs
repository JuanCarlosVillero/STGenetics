
namespace STGenetics.Application.Abstraction.Queries
{
    using STGenetics.Application.Response;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAnimalQueries
    {
        Task<IList<AnimalResponse>> GetFilterAnimalsAsync(
            long animalId, string? name, string? sex, string? status);

        Task<AnimalResponse?> GetAnimalByIdAsync(long animalId);
    }
}
