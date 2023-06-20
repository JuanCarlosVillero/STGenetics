
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

        public async Task<IList<AnimalResponse>> GetFilterAnimalsAsync(long animalId, string? name, string? sex, string? status)
        {
            return await this.animalQueries.GetFilterAnimalsAsync(animalId, name, sex, status);
        }

        public async Task<AnimalResponse?> GetAnimalByIdAsync(long animalId)
        {
            return await this.animalQueries.GetAnimalByIdAsync(animalId);
        }

        public async Task<long> AddAsync(AnimalRequest animalRequest)
        {
            AnimalModel animalModel = new AnimalModel();
            animalModel.AnimalId = animalRequest.AnimalId;
            animalModel.Name = animalRequest.Name;
            animalModel.Breed = animalRequest.Breed;
            animalModel.BirthDate = animalRequest.BirthDate;
            animalModel.Sex = animalRequest.Sex;
            animalModel.Price = animalRequest.Price;
            animalModel.Status = animalRequest.Status;

            animalModel = await this.animalDomainServices.AddAsync(animalModel);
            return animalModel.AnimalId;        }

        public async Task<AnimalResponse?> UpdateAsync(AnimalRequest animalRequest)
        {
            var animalDb = await this.animalQueries.GetAnimalByIdAsync(animalRequest.AnimalId);
            if(animalDb == null)
            {
                return animalDb;
            }

            AnimalModel animalModel = new AnimalModel();
            animalModel.AnimalId = animalRequest.AnimalId;
            animalModel.Name = animalRequest.Name;
            animalModel.Breed = animalRequest.Breed;
            animalModel.BirthDate = animalRequest.BirthDate;
            animalModel.Sex = animalRequest.Sex;
            animalModel.Price = animalRequest.Price;
            animalModel.Status = animalRequest.Status;

            animalModel = await this.animalDomainServices.UpdateAsync(animalModel);

            if (animalModel == null)
            {
                return null;
            }
            else
            {
                AnimalResponse animalResponse = new AnimalResponse();
                animalResponse.AnimalId = animalModel.AnimalId;
                animalResponse.Name = animalModel.Name;
                animalResponse.Breed = animalModel.Breed;
                animalResponse.BirthDate = animalModel.BirthDate;
                animalResponse.Sex = animalModel.Sex;
                animalResponse.Price = animalModel.Price;
                animalResponse.Status = animalModel.Status;

                return animalResponse;
            }
        }

        public async Task<bool> DeleteAsync(long animalId)
        {
            var animalDb = await this.animalQueries.GetAnimalByIdAsync(animalId);
            if (animalDb == null)
            {
                return false;
            }

            await this.animalDomainServices.DeleteAsync(animalId);
            return true;
        }
    }
}
