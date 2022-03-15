using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Restaurant.Core.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        public bool IsActivated { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
