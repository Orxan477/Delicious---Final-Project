using Microsoft.AspNetCore.Http;

namespace Restaurant.Business.ViewModels.Home.Feedback
{
    public class UpdateFeedbackVM
    {
        public int Id { get; set; }
        public IFormFile Photo { get; set; }
        public string FullName { get; set; }
        public int PositionId { get; set; }
        public string Comment { get; set; }
    }
}
