using Microsoft.AspNetCore.Http;

namespace Restaurant.Business.ViewModels.Team
{
    public class UpdateTeamVM
    {
        public string FullName { get; set; }
        public IFormFile Photo { get; set; }
        public string About { get; set; }
        public int PositionId { get; set; }
    }
}
