using CommunityToolkit.Mvvm.Messaging;
using DWShop.Client.Mobile.Messages;
using DWShop.Client.Mobile.Models;
using DWShop.Client.Mobile.ViewModels.Base;

namespace DWShop.Client.Mobile.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        ProductModel productModel;
        public ProductModel Product
        {
            get => productModel;
            set => SetProperty(ref productModel, value);
        }
        public ProductViewModel()
        {
            if (!WeakReferenceMessenger.Default.IsRegistered<ProductDetailMessage>(""))
                WeakReferenceMessenger.Default.Register<ProductDetailMessage>("", (o, s) =>
                {
                    Product = s.Data;
                });

        }

    }
}
