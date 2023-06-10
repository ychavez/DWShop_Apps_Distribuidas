using DWShop.Application.Features.Catalog.Commands.Update;
using DWShop.Application.Validators.Catalog.Commands.Create;
using FluentValidation.TestHelper;

namespace DWShop.Test.Application.Validators
{
    public class UpdateCatalogCommandValidatorTest
    {
        private readonly UpdateCatalogCommandValidator commandValidator;
        public UpdateCatalogCommandValidatorTest()
        {
            commandValidator = new();
        }

        [Fact]
        public void Given_Invalid_Command_Should_Have_Error() 
        {
            // Arrange
            var command = new UpdateCatalogCommand
            {
                Id = 0
            };

            //Act
            var result = commandValidator.TestValidate(command);

            // Assert 
            result.ShouldHaveValidationErrorFor(nameof(UpdateCatalogCommand.Id));
            result.ShouldHaveValidationErrorFor(nameof(UpdateCatalogCommand.Price));
            result.ShouldHaveValidationErrorFor(nameof(UpdateCatalogCommand.Description));
            result.ShouldHaveValidationErrorFor(nameof(UpdateCatalogCommand.Category));

        }

    }
}
