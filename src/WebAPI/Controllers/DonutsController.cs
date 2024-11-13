using Application.Commands.Donuts.Create;
using Application.DTOs.Donuts;
using Application.Queries.Donuts;
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

        [HttpGet]
        public async Task<ActionResult<Result<List<DonutItemDTO>>>> GetAll()
        {
            return CustomResult(await Mediator.Send(new GetDonutsListQuery()));
        }
    }
}
