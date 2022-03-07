using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Business.ViewModels.Team
{
    public class TeamCreateVM
    {
        [Required,MaxLength(100)]
        public string FullName { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
        public string About { get; set; }
        [Required]
        public int PositionId { get; set; }
    }
}
