using Application.Commands;
using Application.Commands.Donuts.Create;
using Application.Commands.Donuts.Update;
using Application.DTOs.Donuts;
using Application.Queries;
using Application.Queries.Donuts;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;

namespace WebAPI.Controllers
{
    public class DonutsController(IMediator mediator) : CustomController(mediator)
    {
        /// <summary>
        /// Crea una dona
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Result<int>>> Create(CreateDonutCommand command)
        {
            return CustomResult(await Mediator.Send(command));
        }

        /// <summary>
        /// Lista de donas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Result<List<DonutItemDTO>>>> GetAll()
        {
            return CustomResult(await Mediator.Send(new GetDonutsListQuery()));
        }

        /// <summary>
        /// Obtiene dona por Id
        /// </summary>
        /// <param name="id">Id de la dona</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Result<List<DonutItemDTO>>>> GetById(int id)
        {
            return CustomResult(await Mediator.Send(new FindQuery<int, DonutDTO>(id)));
        }

        /// <summary>
        /// Actualiza una dona
        /// </summary>
        /// <param name="id">Id de la dona</param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(int id, UpdateDonutCommand command)
        {
            command.Id = id;
            return CustomResult(await Mediator.Send(command));
        }

        /// <summary>
        /// Elimina una dona
        /// </summary>
        /// <param name="id">Id de la dona</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            return CustomResult(await Mediator.Send(new DeleteCommand<int, Donut>(id)));
        }
    }
}
