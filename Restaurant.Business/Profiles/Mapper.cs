using AutoMapper;
using Restaurant.Business.ViewModels.Home.About;
using Restaurant.Business.ViewModels.Home.Choose;
using Restaurant.Business.ViewModels.Home.HomeIntro;
using Restaurant.Business.ViewModels.Menu;
using Restaurant.Business.ViewModels.Position;
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
        }
    }
}
