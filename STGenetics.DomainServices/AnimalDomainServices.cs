﻿
namespace STGenetics.DomainServices
{
    using STGenetics.Domain.Abstraction;
    using STGenetics.Domain.Model;
    using STGenetics.DomainServices.Abstraction.Repository;
    using System.Threading.Tasks;

    public class AnimalDomainServices: IAnimalDomainServices
    {
        private readonly IAnimalRepository animalRepository;

        public AnimalDomainServices(IAnimalRepository animalRepository)
        {
            this.animalRepository = animalRepository;
        }

        public async Task<AnimalModel> AddAsync(AnimalModel animalModel)
        {
            return await this.animalRepository.AddAsync(animalModel);
        }

        public async Task DeleteAsync(long animalId)
        {
            await this.animalRepository.DeleteAsync(animalId);
        }

        public async Task<AnimalModel> UpdateAsync(AnimalModel animalModel)
        {
            return await this.animalRepository.UpdateAsync(animalModel);
        }
    }
}
