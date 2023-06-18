
namespace STGenetics.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using STGenetics.Application.Abstraction.Services;

    [ApiController]
    [Route("api/v1/[controller]")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalApplicationServices animalApplicationServices;

        public AnimalController(IAnimalApplicationServices animalApplicationServices)
        {
            this.animalApplicationServices = animalApplicationServices;
        }

        [HttpGet]
        [Route("~/api/v1/[controller]", Name = "GetAnimalList")]
        public async Task<IActionResult> GetAnimalList()
        {
            var response = await this.animalApplicationServices.GetAllAsync();

            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
