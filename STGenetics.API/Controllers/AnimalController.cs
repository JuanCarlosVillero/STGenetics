
namespace STGenetics.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using STGenetics.Application.Abstraction.Services;
    using STGenetics.Application.Request;
    using System.ComponentModel.DataAnnotations;

    [ApiController]
    [Route("api/v1/[controller]")]
    public class AnimalController : ControllerBase
    {
        private const int ERROR500 = 500;
        private readonly IAnimalApplicationServices animalApplicationServices;

        public AnimalController(IAnimalApplicationServices animalApplicationServices)
        {
            this.animalApplicationServices = animalApplicationServices;
        }

        [HttpGet]
        [Route("~/api/v1/[controller]", Name = "GetAllAnimals")]
        public async Task<IActionResult> GetAllAnimals()
        {
            var response = await this.animalApplicationServices.GetAllAnimalListAsync();

            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet]
        [Route("~/api/v1/[controller]/{animalId}", Name = "GetAnimal")]
        public async Task<IActionResult> GetAnimalById([Required] int animalId)
        {
            var response = await this.animalApplicationServices.GetAnimalByIdAsync(animalId);

            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(AnimalRequest animalRequest)
        {
            var animalId = await animalApplicationServices.AddAsync(animalRequest).ConfigureAwait(false);

            if (animalId == default)
            {
                return StatusCode(ERROR500);
            }

            return Created("Post", "Animal created with ID " + animalId);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(AnimalRequest animalRequest)
        {
            //await Task.Delay(100);
            var animal = await animalApplicationServices.UpdateAsync(animalRequest).ConfigureAwait(false);

            if (animal == default)
            {
                return NotFound();
            }
            return Ok("Animal with ID " + animal.AnimalId + " was update");
        }
    }
}
