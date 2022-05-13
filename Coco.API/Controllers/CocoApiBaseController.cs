using Coco.Infraestructure.Commons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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


        // <summary>
        /// Create an <seealso cref="OperationResponse"/> con <see cref="Microsoft.AspNetCore.Http.StatusCodes.Status200OK"/> Type value <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Type current value</typeparam>
        /// <param name="resultData">Current value</param>
        /// <param name="code">Internal Message Code</param>
        /// <param name="message">Success Message</param>
        /// <returns>A new instance of <see cref="OperationResponse"/></returns>        
        protected OkObjectResult Success<T>(T resultData, string code = "", string message = null) =>
            Ok(OperationResponseFactory.CreateResponse(resultData, code, message));

        /// <summary>
        /// Create an <seealso cref="OperationResponse"/> con <see cref="Microsoft.AspNetCore.Http.StatusCodes.Status200OK"/> type value <typeparamref name="T"/>
        /// </summary>
        /// <param name="code">Internal Message Code</param>
        /// <param name="message">Success Message</param>
        /// <returns>A new instance of <see cref="OperationResponse"/></returns> 
        protected OkObjectResult Success(string code = "", string message = null) =>
            Ok(OperationResponseFactory.CreateResponse(code, message));

        // <summary>
        /// Create an <seealso cref="OperationResponse"/> con <see cref="Microsoft.AspNetCore.Http.StatusCodes.Status200OK"/> Type value <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Type current value</typeparam>
        /// <param name="resultData">Current value</param>
        /// <param name="code">Internal Message Code</param>
        /// <param name="message">Success Message</param>
        /// <returns>A new instance of <see cref="OperationResponse"/></returns>        
        protected OkObjectResult Fail<T>(T resultData, string code = "", string message = null) =>
            Ok(OperationResponseFactory.CreateResponse(resultData, code, message));


        /// <summary>
        /// Create an <seealso cref="OperationResponse"/> with <see cref="Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest"/>
        /// </summary>
        /// <param name="code">Internal Message Code</param>
        /// <param name="message">Error Message</param>
        /// <returns>A new instance of <see cref="OperationResponse"/></returns>        
        protected BadRequestObjectResult Fail(string errorCode, string errorMessage) =>
            BadRequest(OperationResponseFactory.CreateResponse(errorCode, errorMessage));

        /// <summary>
        /// Create an <seealso cref="OperationResponse"/> with <see cref="Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest"/> type value <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Type current value</typeparam>
        /// <param name="resultData">Current value</param>
        /// <param name="code">Internal Message Code</param>
        /// <param name="message">Error Message</param>
        /// <returns>A new instance of <see cref="OperationResponse"/></returns>        
        protected BadRequestObjectResult Fail(ModelStateDictionary state, string errorCode = "")
        {
            var errorMessage = string.Join(" ,", state.AllErrors());
            return BadRequest(OperationResponseFactory.CreateResponse(errorCode, errorMessage));
        }

        /// <summary>
        /// Create an <seealso cref="OperationResponse"/> with <see cref="Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound"/>
        /// </summary>
        /// <param name="code">Internal Message Code</param>
        /// <param name="message">Error Message</param>
        /// <returns>A new instance of <see cref="OperationResponse"/></returns>        
        protected NotFoundObjectResult NotFound(string errorCode, string errorMessage) =>
            NotFound(OperationResponseFactory.CreateResponse(errorCode, errorMessage));

        /// <summary>
        /// the response message is return
        /// </summary>
        /// <typeparam name="TResult">result type</typeparam>
        /// <param name="result">result</param>
        /// <returns>response message</returns>
        protected IActionResult FromResult<TResult>(Result<TResult> result)
        {
            if (result.Succeeded)
                return Success(result.Value, "COCO_OK", "Success");

            return Fail("COCO_ERROR", result.Error);
        }

        /// <summary>
        /// the response message is return
        /// </summary>
        /// <param name="result">result</param>
        /// <returns>response message</returns>
        protected IActionResult FromResult(Result result)
        {
            if (result.Succeeded)
                return Success("COCO_OK", "Success");

            return Fail("COCO_ERROR", result.Error);
        }
    }
}
