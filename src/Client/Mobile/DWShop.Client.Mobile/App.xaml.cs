using DWShop.Client.Mobile.Views;

namespace DWShop.Client.Mobile
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginView();
        }
    }
}