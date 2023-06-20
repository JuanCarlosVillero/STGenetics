
namespace STGenetics.Application.Abstraction.Services
{
    using STGenetics.Application.Request;
    using STGenetics.Application.Response;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAnimalApplicationServices
    {
        Task<IList<AnimalResponse>> GetFilterAnimalsAsync(
            long animalId, string? name, string? sex, string? status);

        Task<AnimalResponse?> GetAnimalByIdAsync(long animalId);

        Task<long> AddAsync(AnimalRequest animalRequest);

        Task<AnimalResponse?> UpdateAsync(AnimalRequest animalRequest);

        Task<bool> DeleteAsync(long animalId);
    }
}
