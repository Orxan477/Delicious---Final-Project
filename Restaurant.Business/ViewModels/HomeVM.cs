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
    }
}
