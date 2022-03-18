using AutoMapper;
using Restaurant.Business.ViewModels.Account;
using Restaurant.Business.ViewModels.Footer;
using Restaurant.Business.ViewModels.Home.About;
using Restaurant.Business.ViewModels.Home.Choose;
using Restaurant.Business.ViewModels.Home.ContactUs;
using Restaurant.Business.ViewModels.Home.Feedback;
using Restaurant.Business.ViewModels.Home.Gallery;
using Restaurant.Business.ViewModels.Home.HomeIntro;
using Restaurant.Business.ViewModels.Home.Special;
using Restaurant.Business.ViewModels.Menu;
using Restaurant.Business.ViewModels.Position;
using Restaurant.Business.ViewModels.Reservation;
using Restaurant.Business.ViewModels.Setting;
using Restaurant.Business.ViewModels.Team;
using Restaurant.Core.Models;

namespace Restaurant.Business.Profiles
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<Team, UpdateTeamVM>();
            CreateMap<Product, UpdateMenuVM>();
            CreateMap<CategoryCreateVM, Category>();
            CreateMap<Category, UpdateCategoryVM>();
            CreateMap<CreatePositionVM, Position>();
            CreateMap<Position, UpdatePositionVM>();
            CreateMap<HomeIntro, HomeIntroUpdateVM>();
            CreateMap<About, AboutUpdateVM>();
            CreateMap<AboutOptionCreateVM, AboutOption>();
            CreateMap<AboutOption, AboutOptionUpdateVM>();
            CreateMap<ChooseRestaurant, ChooseUpdateVM>();
            CreateMap<RestaurantPhotos, UpdateRestaurantPhotoVM>();
            CreateMap<Feedback, UpdateFeedbackVM>();
            CreateMap<Special, CreateUpdateSpecialVM>();
            CreateMap<Product, ProductListVM>().ForMember(o => o.Image, m => m.MapFrom(x => x.MenuImage.Image));
            CreateMap<AboutOption, AboutOptionListVM>();
            CreateMap<Team, TeamListVM>().ForMember(o => o.Position, m => m.MapFrom(x => x.Position.Name));
            CreateMap<AppUser, UserListVM>();
            CreateMap<ContactUs, ContactUsListVM>();
            CreateMap<Feedback, FeedbackListVM>().ForMember(o => o.Position, m => m.MapFrom(x => x.Position.Name));
            CreateMap<HomeIntro, IntroListVM>();
            CreateMap<Reservation, ReservationListVM>();
            CreateMap<Setting, SettingListVM>();
            CreateMap<Subscribe, SubscribeListVM>();
            CreateMap<FullOrder, FullOrderListVM>().ForMember(o=>o.BillingAdress,m=>m.MapFrom(x=>x.BillingAdress.Adress));
        }
    }
}
