using AutoMapper;
using Coco.API.Dtos.Request;
using Coco.Core.Models.Request;
using Coco.Core.Models.Response;
using Coco.Infraestructure.Commons;
using Coco.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Coco.API.Controllers
{
    [Route("api/cart")]
    public class VirtualCartController : CocoApiBaseController<VirtualCartController>
    {
        private readonly IVirtualCartService _virtualCartService;
        private readonly IMapper _mapper;
        public VirtualCartController(IMapper mapper, IVirtualCartService virtualCartService, ILogger<VirtualCartController> logger) : base(logger)
        {
            _mapper = mapper;
            _virtualCartService = virtualCartService;
        }

        /// <summary>
        /// Get All Virtual Carts
        /// </summary>
        [HttpGet("all")]
        [SwaggerResponse(StatusCodes.Status200OK, "Get All Virtual Carts", typeof(OperationResponse<IEnumerable<VirtualCartResponse>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The parameters are invalid or missing.", typeof(OperationResponse))]
        public async Task<IActionResult> GetAllAvailableProducts()
        {
            _logger.LogInformation("Get All Virtual Carts");

            return FromResult(await _virtualCartService.GetAll());
        }

        /// <summary>
        /// Create Virtual Cart
        /// </summary>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Create Virtual Cart", typeof(OperationResponse<IEnumerable<VirtualCartResponse>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The parameters are invalid or missing.", typeof(OperationResponse))]
        public async Task<IActionResult> Create(VirtualCartFilterRequestDTO request)
        {
            _logger.LogInformation("Create Virtual Cart");

            return FromResult(await _virtualCartService.Create(_mapper.Map<VirtualCartFilter>(request)));
        }

        /// <summary>
        /// Add products to current virtual cart
        /// </summary>
        [HttpPost("AddItems")]
        [SwaggerResponse(StatusCodes.Status200OK, "Add products to current virtual cart", typeof(OperationResponse<IEnumerable<VirtualCartResponse>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The parameters are invalid or missing.", typeof(OperationResponse))]
        public async Task<IActionResult> AddItems(VirtualCartEditFilterRequestDTO request)
        {
            _logger.LogInformation("Add products to current virtual cart");

            return FromResult(await _virtualCartService.AddItems(_mapper.Map<VirtualCartEditFilter>(request)));
        }

        /// <summary>
        /// Remove products to current virtual cart
        /// </summary>
        [HttpPost("RemoveItems")]
        [SwaggerResponse(StatusCodes.Status200OK, "Remove products to current virtual cart", typeof(OperationResponse<IEnumerable<VirtualCartResponse>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The parameters are invalid or missing.", typeof(OperationResponse))]
        public async Task<IActionResult> RemoveItems(VirtualCartRemoveFilterRequestDTO request)
        {
            _logger.LogInformation("Remove products to current virtual cart");

            return FromResult(await _virtualCartService.RemoveItems(_mapper.Map<VirtualCartProductsRemoveFilter>(request)));
        }

        /// <summary>
        /// Apply voucher to current virtual Cart
        /// </summary>
        [HttpPost("voucher")]
        [SwaggerResponse(StatusCodes.Status200OK, "Apply voucher to current virtual Cart", typeof(OperationResponse<IEnumerable<VirtualCartResponse>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The parameters are invalid or missing.", typeof(OperationResponse))]
        public async Task<IActionResult> ApplyVoucher(VoucherFilterRequestDTO request)
        {
            _logger.LogInformation("Apply voucher to current virtual Cart");

            return FromResult(await _virtualCartService.ApplyVourcher(_mapper.Map<VoucherFilter>(request)));
        }


    }
}
