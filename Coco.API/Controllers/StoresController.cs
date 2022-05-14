using AutoMapper;
using Coco.API.Dtos.Request;
using Coco.API.Dtos.Response;
using Coco.Core.Entities;
using Coco.Infraestructure.Commons;
using Coco.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Coco.API.Controllers
{
    [Route("api/stores")]
    public class StoresController : CocoApiBaseController<StoresController>
    {
        private readonly IStoreService _storeService;
        private readonly IMapper _mapper;
        public StoresController(IMapper mapper, IStoreService storeService, ILogger<StoresController> logger) : base(logger)
        {
            _mapper = mapper;
            _storeService = storeService;
        }

        /// <summary>
        /// Get All Available Stores.
        /// </summary>
        /// <example>2020-03-07T14:49Z</example>
        [HttpGet("availables")]
        [SwaggerResponse(StatusCodes.Status200OK, "Get All Available Stores", typeof(OperationResponse<IEnumerable<StoreResponseDTO>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The parameters are invalid or missing.", typeof(OperationResponse))]
        public async Task<IActionResult> GetAllAvailableStores([FromQuery] StoreAvailableRequestDTO storeAvailableRequestDTO)
        {
            _logger.LogInformation("Get All Available Products All Stores");
            var result = await _storeService.GetStoresByDate(storeAvailableRequestDTO.IsOpen);

            if (result.Succeeded)
                return FromResult(Result.Success(_mapper.Map<IEnumerable<Store>, IEnumerable<StoreResponseDTO>>(result.Value)));

            return FromResult(result);
        }

        /// <summary>
        /// Setup Data All.
        /// </summary>
        [HttpPost("SetupAll")]
        public async Task<IActionResult> Setup()
        {
            await _storeService.Setup();
            return NoContent();
        }

        /// <summary>
        /// Get Setup All.
        /// </summary>
        [HttpGet("SetupAll")]
        [SwaggerResponse(StatusCodes.Status200OK, "Get SetupAll", typeof(OperationResponse<IEnumerable<StoreResponseDTO>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The parameters are invalid or missing.", typeof(OperationResponse))]
        public async Task<IActionResult> GetSetupDataAll()
        {
            _logger.LogInformation("Get SetupAll");
            var result = await _storeService.GetSetupData();

            return FromResult(result);
        }



    }
}
