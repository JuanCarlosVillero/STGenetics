
namespace STGenetics.Domain.Abstraction
{
    using STGenetics.Domain.Model;

    public interface IAnimalDomainServices
    {
        Task<AnimalModel> AddAsync(AnimalModel animalModel);

        Task<AnimalModel> UpdateAsync(AnimalModel animalModel);

        Task DeleteAsync(long animalId);
    }
}
