using DWShop.Application.Responses.Catalog;
using DWShop.Shared.Wrapper;

namespace DWShop.Client.Infrastructure.Managers.Products.Get
{
    public interface IGetproductsManager 
    {
        Task<IResult<IEnumerable<ProductResponse>>> GetAllProducts();
    }
}