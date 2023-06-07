using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public GetCatalogQueryHandler(IRepositoryAsync<CatalogEntity, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IResult<IEnumerable<ProductResponse>>> Handle(GetCatalogQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync();

            var productsResponse = _mapper.Map<List<ProductResponse>>(products);

            return await Result<List<ProductResponse>>.SuccessAsync(productsResponse, "");
        }
    }
}
