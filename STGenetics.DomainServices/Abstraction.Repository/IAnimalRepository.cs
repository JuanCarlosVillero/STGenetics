
namespace STGenetics.DomainServices.Abstraction.Repository
{
    using STGenetics.Domain.Model;
    using System.Threading.Tasks;

    public interface IAnimalRepository
    {
        Task<AnimalModel> AddAsync(AnimalModel animal);

        Task<AnimalModel> UpdateAsync(AnimalModel animal);

        Task DeleteAsync(long animalId);
    }
}
