using DWShop.Client.Mobile.ViewModels.Base;

namespace DWShop.Client.Mobile.Models
{
    public class LoginModel : ObservableObject
    {
        private string userName;
        public string UserName 
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
    }
}
