using AutoMapper;
using DWShop.Application.Interfaces.Repositories;
using DWShop.Shared.Wrapper;
using MediatR;
using CatalogEntity = DWShop.Domain.Entities.Catalog;

namespace DWShop.Application.Features.Catalog.Commands.Create
{
    public class CreateCatalogCommandHandler
        : IRequestHandler<CreateCatalogCommand, IResult<int>>
    {
        private readonly IRepositoryAsync<CatalogEntity, int> _repository;
        private readonly IMapper _mapper;

        public CreateCatalogCommandHandler(
            IRepositoryAsync<CatalogEntity, int> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IResult<int>> Handle(CreateCatalogCommand request,
            CancellationToken cancellationToken)
        {
            var catalog = _mapper.Map<CatalogEntity>(request);

            catalog.CreatedBy = "Yael";
            catalog.CreatedOn = DateTime.UtcNow;
            catalog.LastModifiedBy = "Yael";

            await _repository.AddAsync(catalog);

            return await Result<int>.SuccessAsync(catalog.Id, "");
        }
    }
}
