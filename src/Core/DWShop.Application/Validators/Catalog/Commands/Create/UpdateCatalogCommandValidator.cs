using DWShop.Application.Features.Catalog.Commands.Update;
using FluentValidation;

namespace DWShop.Application.Validators.Catalog.Commands.Create
{
    public class UpdateCatalogCommandValidator : AbstractValidator<UpdateCatalogCommand>
    {
        public UpdateCatalogCommandValidator()
        {
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Description).MinimumLength(10).MaximumLength(100).NotNull();
            RuleFor(x => x.Category).MinimumLength(10).MaximumLength(20).NotNull();
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
