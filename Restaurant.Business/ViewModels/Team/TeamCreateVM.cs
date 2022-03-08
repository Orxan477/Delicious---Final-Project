using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Business.ViewModels.Team
{
    public class TeamCreateVM
    {
        public string FullName { get; set; }
        public IFormFile Photo { get; set; }
        public string About { get; set; }
        public int PositionId { get; set; }
    }
}
