using DWShop.Client.Mobile.ViewModels;

namespace DWShop.Client.Mobile.Views;

public partial class ProductView : ContentPage
{
	public ProductView(ProductViewModel productViewModel)
	{
		InitializeComponent();
		BindingContext = productViewModel;
		productViewModel.Navigation = Navigation;
	}
}