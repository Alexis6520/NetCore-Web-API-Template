using Application.Abstractions;
using Application.DTOs.Donuts;

namespace Application.Queries.Donuts
{
    public class GetDonutsListQuery : IRequest<List<DonutItemDTO>>
    {
    }
}
