
namespace STGenetics.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using STGenetics.Application.Abstraction.Services;
    using STGenetics.Application.Request;

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

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(AnimalRequest animalRequest)
        {
            var animalId = await animalApplicationServices.Add(animalRequest).ConfigureAwait(false);

            if (animalId == default(int))
            {
                return StatusCode(ERROR500);
            }

            return Created("Post", "Animal created with ID " + animalId);
        }
    }
}
