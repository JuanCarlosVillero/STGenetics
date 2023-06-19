
namespace STGenetics.ApplicationServices
{
    using STGenetics.Application.Abstraction.Queries;
    using STGenetics.Application.Abstraction.Services;
    using STGenetics.Application.Request;
    using STGenetics.Application.Response;
    using STGenetics.Domain.Abstraction;
    using STGenetics.Domain.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AnimalApplicationServices: IAnimalApplicationServices
    {
        private readonly IAnimalDomainServices animalDomainServices;
        private readonly IAnimalQueries animalQueries;

        public AnimalApplicationServices(IAnimalDomainServices animalDomainServices,
            IAnimalQueries animalQueries)
        {
            this.animalDomainServices = animalDomainServices;
            this.animalQueries = animalQueries;
        }

        public async Task<IList<AnimalResponse>> GetAllAnimalListAsync()
        {
            return await this.animalQueries.GetAllAnimalListAsync();
        }

        public async Task<int> Add(AnimalRequest animalRequest)
        {
            AnimalModel animalModel = new AnimalModel();
            animalModel.AnimalId = animalRequest.AnimalId;
            animalModel.Name = animalRequest.Name;
            animalModel.Breed = animalRequest.Breed;
            animalModel.BirthDate = animalRequest.BirthDate;
            animalModel.Sex = animalRequest.Sex;
            animalModel.Price = animalRequest.Price;
            animalModel.Status = animalRequest.Status;

            animalModel = await this.animalDomainServices.Add(animalModel);
            return animalModel.AnimalId;        }
    }
}
