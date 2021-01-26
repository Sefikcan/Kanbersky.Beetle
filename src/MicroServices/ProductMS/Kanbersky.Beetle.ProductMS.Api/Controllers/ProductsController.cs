using Kanbersky.Beetle.ProductMS.Services.Commands;
using Kanbersky.Beetle.ProductMS.Services.DTO.Request;
using Kanbersky.Beetle.ProductMS.Services.DTO.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Kanbersky.Beetle.ProductMS.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mediator"></param>
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create Product Operations
        /// </summary>
        /// <param name="createProductRequestModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreateProductResponseModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequestModel createProductRequestModel)
        {
            var result = await _mediator.Send(new CreateProductCommand(createProductRequestModel));
            return Created("{id}", new { id = result.Id });
        }
    }
}
