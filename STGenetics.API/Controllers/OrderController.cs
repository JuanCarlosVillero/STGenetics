
namespace STGenetics.API.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using STGenetics.Application.Abstraction.Services;
    using STGenetics.Application.Request;

    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private const int ERROR500 = 500;
        private readonly IOrderApplicationServices orderApplicationServices;

        public OrderController(IOrderApplicationServices orderApplicationServices)
        {
            this.orderApplicationServices = orderApplicationServices;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(OrderRequest orderRequest)
        {
            var orderResponse = await orderApplicationServices.SaveAsync(orderRequest).ConfigureAwait(false);

            if (orderResponse.OrderId == default)
            {
                return StatusCode(ERROR500);
            }

            return Created("Post", orderResponse);
        }
    }
}
