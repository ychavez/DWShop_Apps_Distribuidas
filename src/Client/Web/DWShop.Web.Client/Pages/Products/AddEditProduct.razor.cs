using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DWShop.Web.Client.Pages.Products
{
    public partial class AddEditProduct
    {
        [CascadingParameter] private MudDialogInstance DialogInstance { get; set; }

    }
}
