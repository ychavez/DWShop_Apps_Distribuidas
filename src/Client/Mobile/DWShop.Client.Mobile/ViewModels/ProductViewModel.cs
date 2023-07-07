using CommunityToolkit.Mvvm.Messaging;
using DWShop.Client.Mobile.Context;
using DWShop.Client.Mobile.Messages;
using DWShop.Client.Mobile.Models;
using DWShop.Client.Mobile.ViewModels.Base;
using System.Windows.Input;

namespace DWShop.Client.Mobile.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        ProductModel productModel;
        private readonly DataContext dataContext;

        public ICommand AddToBasketCommand { get; set; }
        public ICommand TakePhotoCommand { get; set; }

        public ICommand FlashCommand { get; set; }

        public ProductModel Product
        {
            get => productModel;
            set => SetProperty(ref productModel, value);
        }
        public ProductViewModel(DataContext dataContext)
        {
            if (!WeakReferenceMessenger.Default.IsRegistered<ProductDetailMessage>(""))
                WeakReferenceMessenger.Default.Register<ProductDetailMessage>("", (o, s) =>
                {
                    Product = s.Data;
                });

            this.dataContext = dataContext;

            AddToBasketCommand = new Command(async x => await AddToBasket());
            TakePhotoCommand = new Command(async x => await TakePhoto());
            FlashCommand = new Command(async x => await Fash());

        }

        async Task Fash()
        {
            await Flashlight.Default.TurnOnAsync();
        }

        async Task AddToBasket()
        {
            var added = await dataContext.AddToBasket(Product);

            await Microsoft.Maui.Controls.Application.Current.MainPage
                .DisplayAlert("Canasta", added ?
                 "Producto agregado a canasta" : "El producto ya se encuentra en la canasta", "Ok");


            // obtenemos la respuesta del usuario
            bool answer = await Microsoft.Maui.Controls.Application.Current.MainPage
                  .DisplayAlert("Canasta", "Estas seguro", "Yes", "No");

            // obtenemos la respuesta del usuario mediante una seleccion multiple
            string acrtion = await Microsoft.Maui.Controls.Application.Current.MainPage
                .DisplayActionSheet("A donde te gustaria enviarlo", "Cancel", null,
                "Twitter", "Instragram", "Trheads", "Facebook");

            // obtenemos la respuesta del usuario mediante un textbox
            string result = await Microsoft.Maui.Controls.Application.Current.MainPage
                .DisplayPromptAsync("Pregunta 1", "Como te llamas");

            // obtenemos la respuesta del usuario mediante un textbox
            string result2 = await Microsoft.Maui.Controls.Application.Current.MainPage
                .DisplayPromptAsync("Pregunta 2", "Cuanto es 5 + 5", initialValue: "11", maxLength: 2,
                keyboard: Keyboard.Numeric);


            await Navigation.PopAsync();

            WeakReferenceMessenger.Default.Send(new RefreshBasketMessage());
        }

        async Task TakePhoto()
        {
            var photo = await MediaPicker.Default.CapturePhotoAsync();
            productModel.PhotoURL = photo.FullPath;

            var imgSource = ImageSource.FromStream(async x => await photo.OpenReadAsync());


        }


    }
}
