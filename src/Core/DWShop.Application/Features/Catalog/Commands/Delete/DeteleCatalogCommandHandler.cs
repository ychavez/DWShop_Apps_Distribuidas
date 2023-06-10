using DWShop.Application.Interfaces.Repositories;
using DWShop.Shared.Wrapper;
using MediatR;
using CatalogEntity = DWShop.Domain.Entities.Catalog;

namespace DWShop.Application.Features.Catalog.Commands.Delete
{
    public class DeleteCatalogCommandHandler : IRequestHandler<DeleteCatalogCommand, IResult>
    {
        private readonly IRepositoryAsync<CatalogEntity, int> repository;

        public DeleteCatalogCommandHandler(IRepositoryAsync<CatalogEntity, int> repository)
        {
            this.repository = repository;
        }
        public async Task<IResult> Handle(DeleteCatalogCommand request, CancellationToken cancellationToken)
        {
            var catalog = await repository.GetByIdAsync(request.Id);

            if (catalog is null)
                return await Result.FailAsync("Producto no encontrado");

            await repository.DeleteAsync(catalog);

            await repository.SaveChangesAsync();

            return await Result.SuccessAsync();
        }
    }
}
