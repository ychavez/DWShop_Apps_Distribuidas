using DWShop.Mobile.ViewModels;

namespace DWShop.Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
            viewModel.Navigation = Navigation;
        }
    }
}