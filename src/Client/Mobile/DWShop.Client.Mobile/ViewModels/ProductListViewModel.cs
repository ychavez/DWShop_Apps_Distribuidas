using DWShop.Client.Infrastructure.Managers.Products.Get;
using DWShop.Client.Mobile.Models;
using DWShop.Client.Mobile.ViewModels.Base;
using System.Collections.ObjectModel;

namespace DWShop.Client.Mobile.ViewModels
{
    public class ProductListViewmodel : BaseViewModel
    {
        private ProductModel productModel;
        private readonly IGetProductsManager productsManager;

        private ObservableCollection<ProductModel> products = new();
        public ObservableCollection<ProductModel> Products
        {
            get => products;
            set => SetProperty(ref products, value);
        }

        public ProductListViewmodel(ProductModel productModel, IGetProductsManager productsManager)
        {
            this.productModel = productModel;
            this.productsManager = productsManager;
            LoadProducts();
        }


        public async void LoadProducts()
        {
          
            IsBusy = true;

            var response = await productsManager.GetAllProducts();

            if (response.Succeded)
            {
                //TODO: map
                Products = new ObservableCollection<ProductModel>(response
                    .Data
                    .Select(x => new ProductModel
                    {
                        Id = x.Id,
                        PhotoURL = x.PhotoURL,
                        Price = x.Price,
                        ProductName = x.Name
                    }));

                IsBusy = false;
            }

        }
    }
}
