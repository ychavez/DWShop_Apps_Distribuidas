using DWShop.Client.Mobile.ViewModels;

namespace DWShop.Client.Mobile.Views;

public partial class LoginView : ContentPage
{
    private readonly LoginViewModel loginViewModel;

    public LoginView(LoginViewModel loginViewModel)
	{
		InitializeComponent();

		BindingContext = loginViewModel;
		loginViewModel.Navigation = Navigation;
        this.loginViewModel = loginViewModel;
    }
}