using AutoMapper;
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
        }
    }
}
