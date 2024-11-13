using Application.Abstractions;

namespace Application.Commands.Donuts.Create
{
    public class CreateDonutCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
