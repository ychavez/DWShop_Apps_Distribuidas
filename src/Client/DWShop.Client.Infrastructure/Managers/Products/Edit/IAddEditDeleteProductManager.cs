using DWShop.Application.Features.Catalog.Commands.Update;
using DWShop.Shared.Wrapper;

namespace DWShop.Client.Infrastructure.Managers.Products.Edit
{
    public interface IAddEditDeleteProductManager : IManager
    {
        Task<IResult> EditProduct(UpdateCatalogCommand command);
    }
}