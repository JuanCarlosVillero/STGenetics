
namespace STGenetics.ApplicationServices
{
    using STGenetics.Application.Abstraction.Queries;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AnimalApplicationServices
    {
        private readonly IAnimalQueries animalQueries;

        public AnimalApplicationServices(IAnimalQueries animalQueries)
        {
            this.animalQueries = animalQueries;
        }
    }
}
