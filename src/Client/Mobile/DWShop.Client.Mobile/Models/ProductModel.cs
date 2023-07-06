using DWShop.Client.Mobile.ViewModels.Base;
using SQLite;

namespace DWShop.Client.Mobile.Models
{
    public class ProductModel: ObservableObject
    {
        private int id;
        [PrimaryKey]
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        private string productName;
        public string ProductName
        {
            get => productName;
            set => SetProperty(ref productName, value);
        }

        private string photoURL;
        public string PhotoURL
        {
            get => photoURL;
            set => SetProperty(ref photoURL, value);
        }

        private decimal price;
        public decimal Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
    }
}
