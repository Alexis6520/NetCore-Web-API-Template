using Application.Abstractions;
using Application.Wrappers;
using Services;
using System.Net;

namespace Application.Commands.Donuts.Update
{
    public class UpdateDonutHandler(ApplicationDbContext dbContext) : IHandler<UpdateDonutCommand>
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<Result> Handle(UpdateDonutCommand request, CancellationToken cancellationToken)
        {
            var donut = await _dbContext.Donuts
                .FindAsync([request.Id], cancellationToken);

            if (donut == null) return Result.Fail(HttpStatusCode.NotFound, "Dona no encontrada");
            donut.Name = request.Name;
            donut.Description = request.Description;
            donut.Price = request.Price;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
