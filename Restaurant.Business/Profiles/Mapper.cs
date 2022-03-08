using AutoMapper;
using Restaurant.Business.ViewModels.Menu;
using Restaurant.Business.ViewModels.Position;
using Restaurant.Business.ViewModels.Team;
using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
        }
    }
}
