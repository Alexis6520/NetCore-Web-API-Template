using Application.Abstractions;
using Application.DTOs.Donuts;
using Application.Wrappers;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Application.Queries.Donuts
{
    public class DonutQueryHandler(ApplicationDbContext dbContext) : IHandler<GetDonutsListQuery, List<DonutItemDTO>>
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
    }
}
