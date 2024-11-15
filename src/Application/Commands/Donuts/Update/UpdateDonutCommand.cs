using Application.Abstractions;
using FluentValidation;
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

    public class UpdateDonutValidator : AbstractValidator<UpdateDonutCommand>
    {
        public UpdateDonutValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nombre obligatorio")
                .MaximumLength(50).WithMessage("El nombre no puede superar los {MaxLength} caracteres");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("La descripción no puede superar los {MaxLength} caracteres");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("No se aceptan precios negativos");
        }
    }
}
