﻿using Microsoft.AspNetCore.Identity;

namespace Restaurant.Core.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        public bool IsActivated { get; set; }
    }
}
