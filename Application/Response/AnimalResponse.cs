using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGenetics.Application.Response
{
    public class AnimalResponse
    {
        public long AnimalId { get; set; }
        public string Name { get; set; } = default!;
        public string Breed { get; set; } = default!;
        public DateTime BirthDate { get; set; }
        public string Sex { get; set; } = default!;
        public decimal Price { get; set; }
        public string Status { get; set; } = default!;
    }
}
