using DWShop.Application.Features.Identity.Commands.Login;
using DWShop.Client.Infrastructure.Managers.Authentication;
using DWShop.Client.Mobile.ViewModels.Base;
using System.Windows.Input;

namespace DWShop.Client.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthenticationManager authenticationManager;

        public LoginViewModel(IAuthenticationManager authenticationManager)
        {
            this.authenticationManager = authenticationManager;

            LoginCommand = new Command(async () =>
            {
                var result =
                await authenticationManager.Login(new LoginCommand {
                    UserName = "Admin",
                    Password = "Admin"
                });
            });
        }


        public ICommand LoginCommand { get; private set; }

    }
}
