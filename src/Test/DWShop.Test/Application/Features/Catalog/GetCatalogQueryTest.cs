using AutoMapper;
using DWShop.Application.Features.Catalog.Queries;
using DWShop.Application.Interfaces.Repositories;
using DWShop.Application.Responses.Catalog;
using Moq;
using CatalogEntity = DWShop.Domain.Entities.Catalog;

namespace DWShop.Test.Application.Features.Catalog
{
    public class GetCatalogQueryTest
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRepositoryAsync<CatalogEntity, int>> _repositoryMock;

        public GetCatalogQueryTest()
        {
            _mapperMock = new Mock<IMapper>();
            _repositoryMock = new Mock<IRepositoryAsync<CatalogEntity, int>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_Result()
        {
            // Arrange 
            var query = new GetCatalogQuery();
            var catalogs = new List<CatalogEntity>()
            {
                new ()
                {
                    Name = "Teclado",
                    Category = "Computacion",
                    Id = 1,
                    PhotoURL = "photo",
                    Price = 12.5M,
                    Summary = "Bonito teclado mecanico iluminado",
                    Description = "Teclado marca corsair mecanico teclas azules"
                },
                new ()
                {
                    Name = "Mouse",
                    Category = "Computacion",
                    Id = 2,
                    PhotoURL = "photo",
                    Price = 120.5M,
                    Summary = "Bonito mouse iluminado",
                    Description = "Mouse"
                }
            };

            var response = new List<ProductResponse> {
                new ()
                {
                    Name = "Teclado",
                    Category = "Computacion",
                    PhotoURL = "photo",
                    Price = 12.5M,
                    Summary = "Bonito teclado mecanico iluminado",
                    Description = "Teclado marca corsair mecanico teclas azules"
                },
                new ()
                {
                    Name = "Mouse",
                    Category = "Computacion",
                    PhotoURL = "photo",
                    Price = 120.5M,
                    Summary = "Bonito mouse iluminado",
                    Description = "Mouse"
                }
            };

            _mapperMock.Setup(x => x.Map<List<ProductResponse>>(catalogs)).Returns(response);

            _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(catalogs);

            var handler = new GetCatalogQueryHandler(_repositoryMock.Object, _mapperMock.Object);

            // Act
            var result = await handler.Handle(query, default);


            // Assert
            Assert.NotNull(result);
            Assert.Equal(response, result.Data);
            Assert.True(result.Succeded);
            _mapperMock.Verify(x => x.Map<List<ProductResponse>>(It.IsAny<List<CatalogEntity>>()), Times.Once);
            _repositoryMock.Verify(x => x.GetAllAsync(), Times.Once);

        }
    }
}
