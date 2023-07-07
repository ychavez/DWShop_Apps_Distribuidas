using CommunityToolkit.Mvvm.Messaging;
using DWShop.Client.Mobile.Context;
using DWShop.Client.Mobile.Messages;
using DWShop.Client.Mobile.Models;
using DWShop.Client.Mobile.ViewModels.Base;
using System.Collections.ObjectModel;

namespace DWShop.Client.Mobile.ViewModels
{
    public class BasketViewModel : BaseViewModel
    {
        private ObservableCollection<ProductModel> products = new();
        private readonly DataContext dataContext;

        public ObservableCollection<ProductModel> Products
        {
            get => products;
            set => SetProperty(ref products, value);
        }


        public BasketViewModel(DataContext dataContext)
        {
            this.dataContext = dataContext;
            if (!WeakReferenceMessenger.Default.IsRegistered<RefreshBasketMessage>(""))
                WeakReferenceMessenger.Default.Register<RefreshBasketMessage>("", async (o, s) =>
                {
                    await FillBasket();
                });
        }


        private async Task FillBasket()
        {
            Products = new ObservableCollection<ProductModel>(await dataContext.GetBasket());
        }

    }
}
