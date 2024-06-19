using FluentValidation;
using ProductService.Application.Features.Commands;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
	public CreateProductCommandValidator()
	{
		RuleFor(x => x.Name).NotEmpty().WithMessage("Nombre es requerido");
		RuleFor(x => x.Status).InclusiveBetween(0, 1).WithMessage("Estado debe ser un valor de 0 o 1");
		RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage("El stock debe ser mayor o igual a 0");
		RuleFor(x => x.Description).NotEmpty().WithMessage("Descrición es requerida");
		RuleFor(x => x.Price).GreaterThan(0).WithMessage("El precio debe ser mayor a cero");
	}
}
