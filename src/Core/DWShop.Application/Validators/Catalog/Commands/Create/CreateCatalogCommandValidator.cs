using DWShop.Application.Features.Catalog.Commands.Create;
using FluentValidation;

namespace DWShop.Application.Validators.Catalog.Commands.Create
{
    public class CreateCatalogCommandValidator :
        AbstractValidator<CreateCatalogCommand>
    {
        public CreateCatalogCommandValidator()
        {
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("El producto no puede ser menor o ygual a 0")
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull();
        }
    }
}
