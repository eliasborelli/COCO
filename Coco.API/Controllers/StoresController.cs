using Coco.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coco.API.Controllers
{
    [Route("api/stores")]
    public class StoresController : CocoApiBaseController<StoresController>
    {
        private readonly IStoreService _storeService;
        public StoresController(IStoreService storeService, ILogger<StoresController> logger) : base(logger)
        {
            _storeService = storeService;
        }

        [HttpPost]
        public async Task<IActionResult> Setup()
        {
            //await _storeService.GetStoresByDateAsync(new DateTime(2022, 05, 9, 5, 30, 5));
            await _storeService.SetupAsync();
            return NoContent();
        }





    }
}
