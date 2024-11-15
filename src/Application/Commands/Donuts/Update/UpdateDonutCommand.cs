using Application.Abstractions;
using System.Text.Json.Serialization;

namespace Application.Commands.Donuts.Update
{
    public class UpdateDonutCommand : IRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
