using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coco.API.Controllers
{
    [Route("api/cart")]
    public class VirtualCartController : CocoApiBaseController<VirtualCartController>
    {
        public VirtualCartController(ILogger<VirtualCartController> logger) : base(logger)
        {

        }


    }
}
