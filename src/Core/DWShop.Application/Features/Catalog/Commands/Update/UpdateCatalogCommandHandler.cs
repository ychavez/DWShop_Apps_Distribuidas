using DWShop.Application.Interfaces.Repositories;
using DWShop.Shared.Wrapper;
using MediatR;
using CatalogEntity = DWShop.Domain.Entities.Catalog;

namespace DWShop.Application.Features.Catalog.Commands.Update
{
    public class UpdateCatalogCommandHandler : IRequestHandler<UpdateCatalogCommand, IResult>
    {
        private readonly IRepositoryAsync<CatalogEntity, int> repositoryAsync;

        public UpdateCatalogCommandHandler(IRepositoryAsync<CatalogEntity,int> repositoryAsync)
        {
            this.repositoryAsync = repositoryAsync;
        }

        public async Task<IResult> Handle(UpdateCatalogCommand request, CancellationToken cancellationToken)
        {
            CatalogEntity? entity = await repositoryAsync.GetByIdAsync(request.Id);

            if (entity is null)
                return await Result.FailAsync("Producto no encontrado");

            entity.PhotoURL = request.PhotoURL;
            entity.Summary = request.Summary;
            entity.Price = request.Price;
            entity.Name = request.Name;

            await repositoryAsync.UpdateAsync(entity);
            await repositoryAsync.SaveChangesAsync();

            return await Result.SuccessAsync();
        }
    }
}
