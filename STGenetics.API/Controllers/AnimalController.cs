
namespace STGenetics.API.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using STGenetics.Application.Abstraction.Services;
    using STGenetics.Application.Request;
    using System.ComponentModel.DataAnnotations;

    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class AnimalController : ControllerBase
    {
        private const int ERROR500 = 500;
        private readonly IAnimalApplicationServices animalApplicationServices;

        public AnimalController(IAnimalApplicationServices animalApplicationServices)
        {
            this.animalApplicationServices = animalApplicationServices;
        }

        /// <summary>
        /// Filters animals by AnimalId or Name or Sex or Status (ACTIVE or INACTIVE).
        /// </summary>
        /// <param name="animalId"></param>
        /// <param name="name"></param>
        /// <param name="sex"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/api/v1/[controller]", Name = "GetAllAnimals")]
        public async Task<IActionResult> GetFilterAnimals(long animalId, string? name, string? sex, string? status)
        {
            var response = await this.animalApplicationServices.GetFilterAnimalsAsync(
                animalId, name, sex, status);

            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Get Animal by AnimalId
        /// </summary>
        /// <param name="animalId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/api/v1/[controller]/{animalId}", Name = "GetAnimal")]
        public async Task<IActionResult> GetAnimalById([Required] long animalId)
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

        /// <summary>
        /// Create Animal
        /// </summary>
        /// <param name="animalRequest"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Update Animal
        /// </summary>
        /// <param name="animalRequest"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Delete Animal
        /// </summary>
        /// <param name="animalId"></param>
        /// <returns></returns>
        [HttpDelete("{animalId}")]
        public async Task<IActionResult> Delete(long animalId)
        {
            var result = await animalApplicationServices.DeleteAsync(animalId).ConfigureAwait(false);

            if (!result)
            {
                return NotFound();
            }
            return Ok("Animal with ID " + animalId + " was delete");
        }
    }
}
