using DWShop.Mobile.ViewModels.Base;
using DWShop.Mobile.Views;
using System.Windows.Input;
using DWShop.Application.Features.Identity.Commands.Login;
using DWShop.Client.Infrastructure.Managers.Authentication;
using DWShop.Mobile.Models;
using static Microsoft.Maui.Controls.Application;

namespace DWShop.Mobile.ViewModels
{

    public class MainPageViewModel : BaseVieModel
    {
        private readonly Hello _hello;
        private readonly IAuthenticationManager _authenticationManager;
        private Item? _item;
        public Item? Item
        {
            get => _item;
            set => SetProperty(ref _item, value);
        }

        public ICommand IncrementCommand { get; private set; }
        public ICommand ShowMessageCommand { get; private set; }


        public MainPageViewModel(Item item, Hello hello, IAuthenticationManager authenticationManager)
        {
            _hello = hello;
            _authenticationManager = authenticationManager;
            Item = item;

            IncrementCommand = new Command(() =>
            {
                if (Item != null)
                {
                    Item.Count++;
                }
            });

            ShowMessageCommand = new Command(async () =>
            {
                if (Current?.MainPage != null)
                {
                    //    await Application.Current.MainPage.DisplayAlert("Count", Item?.Count.ToString(), "OK");
                    var result = await authenticationManager.Login(new LoginCommand()
                        { UserName = "Admin2", Password = "Admin2!" });
                    await Navigation.PushAsync(_hello);
                }
            });
        }
    }
}
