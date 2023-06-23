using DWShop.Application.Features.Catalog.Commands.Update;
using DWShop.Client.Infrastructure.Managers.Products.Edit;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DWShop.Web.Client.Pages.Products
{
    public partial class AddEditProduct
    {
        [CascadingParameter] private MudDialogInstance DialogInstance { get; set; }

        [Parameter] public UpdateCatalogCommand EditProductCommand { get; set; } = new();

        [Inject]
        public IAddEditDeleteProductManager ProductManager { get; set; }

        private async Task UpdateAsync()
        {
            var response = await ProductManager.EditProduct(EditProductCommand);

            if (response.Succeded)
                DialogInstance.Close();
            else
                foreach (var message in response.Messages) 
                    _snackBar.Add(message, Severity.Error);
        }


    }
}
