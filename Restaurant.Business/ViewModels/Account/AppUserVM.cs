using Restaurant.Core.Models;

namespace Restaurant.Business.ViewModels.Account
{
    public class AppUserVM
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string NewRole { get; set; }
    }
}
