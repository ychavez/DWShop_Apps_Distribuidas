using DWShop.Client.Mobile.Models;
using DWShop.Client.Mobile.ViewModels;

namespace DWShop.Client.Mobile.Views;

public partial class PropductListView : ContentPage
{
    public PropductListView(ProductListViewmodel productListViewmodel)
    {
        InitializeComponent();
        BindingContext = productListViewmodel;
        productListViewmodel.Navigation = Navigation;
    }

    private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is not null && BindingContext is ProductListViewmodel viewModel)
        {
            var selectedItem = e.Item as ProductModel;

            viewModel.DetailCommand.Execute(selectedItem);
        }
    }
}