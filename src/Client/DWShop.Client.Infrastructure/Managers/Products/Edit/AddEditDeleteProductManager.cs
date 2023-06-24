using DWShop.Application.Features.Catalog.Commands.Delete;
using DWShop.Application.Features.Catalog.Commands.Update;
using DWShop.Client.Infrastructure.Extensions;
using DWShop.Client.Infrastructure.Routes.Products.AddEditDelete;
using DWShop.Shared.Wrapper;
using System.Net.Http.Json;

namespace DWShop.Client.Infrastructure.Managers.Products.Edit
{
    public class AddEditDeleteProductManager : IAddEditDeleteProductManager
    {
        private readonly HttpClient _httpClient;

        public AddEditDeleteProductManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult> EditProduct(UpdateCatalogCommand command)
        {
            var response = await _httpClient.PutAsJsonAsync(AddEditDeleteProductsEndpoints.Edit, command);

            return await response.ToResult();
        }
    }
}
