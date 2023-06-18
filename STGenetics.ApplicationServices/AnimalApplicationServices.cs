
namespace STGenetics.ApplicationServices
{
    using STGenetics.Application.Abstraction.Queries;
    using STGenetics.Application.Abstraction.Services;
    using STGenetics.Application.Response;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AnimalApplicationServices: IAnimalApplicationServices
    {
        private readonly IAnimalQueries animalQueries;

        public AnimalApplicationServices(IAnimalQueries animalQueries)
        {
            this.animalQueries = animalQueries;
        }

        public async Task<IList<AnimalResponse>> GetAllAsync()
        {
            return await this.animalQueries.GetAnimalListAsync();
        }
    }
}
