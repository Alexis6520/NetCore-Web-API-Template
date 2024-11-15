using Application.Abstractions;
using Application.DTOs.Donuts;
using Application.Wrappers;
using Microsoft.EntityFrameworkCore;
using Services;
using System.Net;

namespace Application.Queries.Donuts
{
    public class DonutQueryHandler(ApplicationDbContext dbContext) :
        IHandler<GetDonutsListQuery, List<DonutItemDTO>>,
        IHandler<FindQuery<int, DonutDTO>, DonutDTO>
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<Result<List<DonutItemDTO>>> Handle(GetDonutsListQuery request, CancellationToken cancellationToken)
        {
            var donuts = await _dbContext.Donuts
                .Select(x => new DonutItemDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync(cancellationToken);

            return Result<List<DonutItemDTO>>.Success(donuts);
        }

        public async Task<Result<DonutDTO>> Handle(FindQuery<int, DonutDTO> request, CancellationToken cancellationToken)
        {
            var donut = await _dbContext.Donuts
                .Where(x => x.Id == request.Key)
                .Select(x => new DonutDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (donut == null) return Result<DonutDTO>.Fail(HttpStatusCode.NotFound, "Dona no encontrada");
            return Result<DonutDTO>.Success(donut);
        }
    }
}
