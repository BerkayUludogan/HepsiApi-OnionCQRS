using HepsiApi.Application.Features.Products.Command.CreateProduct;
using HepsiApi.Application.Features.Products.Command.DeleteProduct;
using HepsiApi.Application.Features.Products.Command.UpdateProduct;
using HepsiApi.Application.Features.Products.Queries.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HepsiApi.Api.Controller
{
    [Route("api/[controller]/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await mediator.Send(new GetAllProductsQueryRequest());
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateProducts(CreateProductCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateProducts(UpdateProductCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteProducts(DeleteProductCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetById([FromQuery]int id)
        {
            await mediator.Send(id);
            return Ok();
        }
    }
}
