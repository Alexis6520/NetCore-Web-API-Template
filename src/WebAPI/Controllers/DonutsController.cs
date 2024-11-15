using Application.Commands.Donuts.Create;
using Application.Commands.Donuts.Update;
using Application.DTOs.Donuts;
using Application.Queries;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<List<DonutItemDTO>>>> GetById(int id)
        {
            return CustomResult(await Mediator.Send(new FindQuery<int, DonutDTO>(id)));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(int id, UpdateDonutCommand command)
        {
            command.Id = id;
            return CustomResult(await Mediator.Send(command));
        }
    }
}
