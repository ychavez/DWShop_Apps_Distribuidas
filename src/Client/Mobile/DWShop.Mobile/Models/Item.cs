using DWShop.Mobile.ViewModels.Base;

namespace DWShop.Mobile.Models
{
    public class Item : ObservableObject
    {
        private int _count;
        public int Count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }
    }
}
