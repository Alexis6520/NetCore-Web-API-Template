using Application.Abstractions;
using FluentValidation;

namespace Application.Commands.Donuts.Create
{
    public class CreateDonutCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateDonutValidator : AbstractValidator<CreateDonutCommand>
    {
        public CreateDonutValidator()
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
