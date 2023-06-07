using System.Security.Cryptography.X509Certificates;
using DWShop.Application.Interfaces.Repositories;
using DWShop.Application.Responses;
using DWShop.Shared.Wrapper;
using MediatR;
using CatalogEntity = DWShop.Domain.Entities.Catalog;

namespace DWShop.Application.Features.Catalog.Queries
{
    public class GetCatalogQueryHandler : IRequestHandler<GetCatalogQuery, 
        IResult<IEnumerable<ProductResponse>>>
    {
        private readonly IRepositoryAsync<CatalogEntity, int> _repository;

        public GetCatalogQueryHandler(IRepositoryAsync<CatalogEntity, int> repository)
        {
            _repository = repository;
        }
        public async Task<IResult<IEnumerable<ProductResponse>>> Handle(GetCatalogQuery request, CancellationToken cancellationToken)
        {
            var products  = await _repository.GetAllAsync();

            return await Result<List<ProductResponse>>.SuccessAsync(products
                .Select(x => new ProductResponse
                {
                    Category = x.Category,
                    Description = x.Description,
                    Name = x.Name,
                    PhotoURL = x.PhotoURL,
                    Price = x.Price,
                    Summary = x.Summary
                }).ToList(), "");
        }
    }
}
