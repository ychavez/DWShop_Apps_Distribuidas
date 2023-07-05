﻿using DWShop.Application.Features.Identity.Commands.Login;
using DWShop.Client.Infrastructure.Managers.Authentication;
using DWShop.Client.Mobile.Models;
using DWShop.Client.Mobile.ViewModels.Base;
using DWShop.Shared.Constants;
using Microsoft.Maui.Controls;
using System.Net.Http.Headers;
using System.Windows.Input;

namespace DWShop.Client.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthenticationManager authenticationManager;
        private LoginModel loginModel;

        public ICommand LoginCommand { get; private set; }

        public LoginModel LoginModel
        {
            get => loginModel;
            set => SetProperty(ref loginModel, value);
        }


        public LoginViewModel(IAuthenticationManager authenticationManager, LoginModel loginModel,
            HttpClient httpClient)
        {
            this.authenticationManager = authenticationManager;
            this.loginModel = loginModel;
            LoginCommand = new Command(async () =>
            {
                var result =
                await authenticationManager.Login(new LoginCommand
                {
                    UserName = loginModel.UserName,
                    Password = loginModel.Password
                });

                if (result.Succeded)
                {
                    await SecureStorage.Default.SetAsync("Token", result.Data.Token);

                    httpClient.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue(StorageConstants.Local.Scheme, result.Data.Token);
                    return;
                }

                await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("Error al iniciar",
                   result.Messages.First(),"Ok");

            });
        }
    }
}
