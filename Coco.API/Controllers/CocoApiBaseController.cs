using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Coco.API.Controllers
{
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Request input parameters are invalid.")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Authorization failed.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Something bad happened.")]
    [Route("api/[controller]")]
    public class CocoApiBaseController<T> : ControllerBase where T : CocoApiBaseController<T>
    {
        protected readonly ILogger<T> _logger;
        public CocoApiBaseController(ILogger<T> logger)
        {
            _logger = logger;
        }
    }
}
