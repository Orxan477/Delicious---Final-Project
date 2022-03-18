using Restaurant.Business.ViewModels.Account;
using Restaurant.Business.ViewModels.Footer;
using Restaurant.Business.ViewModels.Home;
using Restaurant.Business.ViewModels.Menu;
using Restaurant.Core.Models;
using System.Collections.Generic;

namespace Restaurant.Business.ViewModels
{
    public class HomeVM
    {
        public List<HomeIntro> HomeIntro { get; set; }
        public About About { get; set; }
        public List<AboutOption> AboutOptions { get; set; }
        public List<ChooseRestaurant> ChooseRestaurants { get; set; }
        public List<Special> Specials { get; set; }
        public List<RestaurantPhotos> RestaurantsPhotos { get; set;}
        public List<Feedback> Feedbacks { get; set; }
        public MenuVM MenuVM { get; set; }
        public ContactUsVM ContactUsVM { get; set; }
        public SubscribeVM SubscribeVM { get; set; }
        public List<BasketItemVM> BasketItemVM { get; set; }
        public List<Product> Product { get; set; }
        public List<Core.Models.Team> Teams { get; set; }
        public List<FullOrder> FullOrders { get; set; }
        public BillingAdressVM BillingAdressesVM { get; set; }
        public Core.Models.Team Team { get; set; }
        public Paginate<HomeFullOrderListVM> Paginate { get; set; }

    }
}
