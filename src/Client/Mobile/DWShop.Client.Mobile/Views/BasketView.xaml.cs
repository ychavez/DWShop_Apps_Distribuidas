using DWShop.Client.Mobile.ViewModels;

namespace DWShop.Client.Mobile.Views;

public partial class BasketView : ContentPage
{
	public BasketView(BasketViewModel basketViewModel)
	{
		InitializeComponent();
		BindingContext = basketViewModel;
		basketViewModel.Navigation = Navigation;
	}
}