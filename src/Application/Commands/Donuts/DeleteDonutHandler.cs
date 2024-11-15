using Application.Abstractions;
using Application.Wrappers;
using Domain.Entities;
using Services;
using System.Net;

namespace Application.Commands.Donuts
{
    public class DeleteDonutHandler(ApplicationDbContext dbContext) : IHandler<DeleteCommand<int, Donut>>
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<Result> Handle(DeleteCommand<int, Donut> request, CancellationToken cancellationToken)
        {
            var donut = await _dbContext.Donuts
                .FindAsync([request.Key], cancellationToken);

            if (donut == null) return Result.Fail(HttpStatusCode.NotFound, "Dona no encontrada");
            _dbContext.Donuts.Remove(donut);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
