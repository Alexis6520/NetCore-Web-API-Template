using Application.Abstractions;
using Application.Wrappers;
using Microsoft.Extensions.Logging;
using Services;
using System.Net;

namespace Application.Commands.Donuts.Update
{
    public class UpdateDonutHandler(ApplicationDbContext dbContext, ILogger<UpdateDonutHandler> logger) : IHandler<UpdateDonutCommand>
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly ILogger<UpdateDonutHandler> _logger = logger;

        public async Task<Result> Handle(UpdateDonutCommand request, CancellationToken cancellationToken)
        {
            var donut = await _dbContext.Donuts
                .FindAsync([request.Id], cancellationToken);

            if (donut == null) return Result.Fail(HttpStatusCode.NotFound, "Dona no encontrada");
            donut.Name = request.Name;
            donut.Description = request.Description;
            donut.Price = request.Price;
            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Dona {Id} actualizada", donut.Id);
            return Result.Success();
        }
    }
}
