using Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Abstractions
{
    [Route("[controller]")]
    [ApiController]
    public abstract class CustomController(IMediator mediator) : ControllerBase
    {
        protected IMediator Mediator { get; } = mediator;

        protected ObjectResult CustomResult(Result result)
        {
            var statusCode = (int)result.StatusCode;
            if (!result.Succeeded) return StatusCode(statusCode, result);
            return StatusCode(statusCode, null);
        }

        protected ObjectResult CustomResult<TValue>(Result<TValue> result)
        {
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
