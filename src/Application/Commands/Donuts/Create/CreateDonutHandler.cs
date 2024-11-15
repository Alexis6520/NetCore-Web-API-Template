using Application.Abstractions;
using Application.Wrappers;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Services;
using System.Net;

namespace Application.Commands.Donuts.Create
{
    public class CreateDonutHandler(ApplicationDbContext dbContext, ILogger<CreateDonutHandler> logger) : IHandler<CreateDonutCommand, int>
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly ILogger<CreateDonutHandler> _logger = logger;

        public async Task<Result<int>> Handle(CreateDonutCommand request, CancellationToken cancellationToken)
        {
            var donut = new Donut
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
            };

            await _dbContext.Donuts.AddAsync(donut, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Dona {Id} creada", donut.Id);
            return Result<int>.Success(donut.Id, HttpStatusCode.Created);
        }
    }
}
