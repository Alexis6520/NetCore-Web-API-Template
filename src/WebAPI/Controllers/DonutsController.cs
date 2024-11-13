using Application.Commands.Donuts.Create;
using Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;

namespace WebAPI.Controllers
{
    public class DonutsController(IMediator mediator) : CustomController(mediator)
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Result<int>>> Create(CreateDonutCommand command)
        {
            return CustomResult(await Mediator.Send(command));
        }
    }
}
