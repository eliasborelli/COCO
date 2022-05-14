using AutoMapper;
using Coco.API.Dtos.Request;
using Coco.API.Dtos.Response;
using Coco.Core.Models.Request;
using Coco.Core.Models.Response;
using Coco.Infraestructure.Commons;
using Coco.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Coco.API.Controllers
{
    [Route("api/products")]
    public class ProductsController : CocoApiBaseController<ProductsController>
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductsController(IMapper mapper, IProductService productService, ILogger<ProductsController> logger) : base(logger)
        {
            _mapper = mapper;
            _productService = productService;
        }

        /// <summary>
        /// Get All Available Products All Stores.
        /// </summary>
        [HttpGet("availables")]
        [SwaggerResponse(StatusCodes.Status200OK, "Get All Available Products All Stores", typeof(OperationResponse<IEnumerable<ProductStockResponseDTO>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The parameters are invalid or missing.", typeof(OperationResponse))]
        public async Task<IActionResult> GetAllAvailableProducts()
        {
            _logger.LogInformation("Get All Available Products All Stores");
            var result = await _productService.GetAllAvailableProducts();

            if (result.Succeeded)
                return FromResult(Result.Success(_mapper.Map<IEnumerable<ProductModelResponse>, IEnumerable<ProductStockResponseDTO>>(result.Value)));
           
            return FromResult(result);
        }

        /// <summary>
        /// Get Available products by Store.
        /// </summary>
        [HttpGet("availables/store")]
        [SwaggerResponse(StatusCodes.Status200OK, "Get Available products by Store", typeof(OperationResponse<IEnumerable<ProductStockResponseDTO>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The parameters are invalid or missing.", typeof(OperationResponse))]
        public async Task<IActionResult> GetProductAvailableByStore([FromQuery] string nameStore)
        {
            _logger.LogInformation("Get Available products by Store");
            var result = await _productService.GetAllProductsByStore(nameStore);

            if (result.Succeeded)
                return FromResult(Result.Success(_mapper.Map<IEnumerable<ProductModelResponse>, IEnumerable<ProductStockResponseDTO>>(result.Value)));

            return FromResult(result);
        }

        /// <summary>
        /// Get Available product by Filter.
        /// </summary>
        [HttpGet("available/store")]
        [SwaggerResponse(StatusCodes.Status200OK, "Get Available product by Filter", typeof(OperationResponse<ProductResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The parameters are invalid or missing.", typeof(OperationResponse))]
        public async Task<IActionResult> GetProductAvailableByFilter([FromQuery] ProductFilterRequestDTO filter)
        {
            _logger.LogInformation("Get Available product by Filter");
            var result = await _productService.GetProductByFilter(_mapper.Map<ProductFilter>(filter));

            if (result.Succeeded)
                return FromResult(Result.Success(_mapper.Map<ProductResponseDTO>(result.Value)));

            return FromResult(result);
        }

    }
}
