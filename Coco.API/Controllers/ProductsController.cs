using AutoMapper;
using Coco.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Coco.API.Controllers
{
    [Route("api/products")]
    public class ProductsController : CocoApiBaseController<ProductsController>
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductsController(IProductService productService, ILogger<ProductsController> logger) : base(logger)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productService.GetAllAvailableProducts());
        }
    }
}
