using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGenetics.Application.Response
{
    public class AnimalResponse
    {
        public int AnimalId { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public DateTime BirthDate { get; set; }
        public string Sex { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
    }
}
