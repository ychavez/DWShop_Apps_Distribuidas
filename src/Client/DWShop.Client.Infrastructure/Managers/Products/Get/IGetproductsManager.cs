using DWShop.Application.Responses.Catalog;
using DWShop.Shared.Wrapper;

namespace DWShop.Client.Infrastructure.Managers.Products.Get
{
    public interface IGetProductsManager : IManager
    {
        Task<IResult<IEnumerable<ProductResponse>>> GetAllProducts();
    }
}