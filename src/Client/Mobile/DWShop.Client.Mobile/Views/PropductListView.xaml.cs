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
}