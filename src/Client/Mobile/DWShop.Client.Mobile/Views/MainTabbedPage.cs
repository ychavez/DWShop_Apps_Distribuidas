namespace DWShop.Client.Mobile.Views
{
    public class MainTabbedPage : TabbedPage
    {
        public MainTabbedPage(PropductListView propductList, BasketView basketView )
        {
            Children.Add(propductList);
            Children.Add(basketView);
            Children.Add(basketView);
        }
    }
}
