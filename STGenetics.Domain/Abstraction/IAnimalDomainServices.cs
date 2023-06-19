﻿using STGenetics.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGenetics.Domain.Abstraction
{
    public interface IAnimalDomainServices
    {
        Task<AnimalModel> Add(AnimalModel animalModel);
    }
}