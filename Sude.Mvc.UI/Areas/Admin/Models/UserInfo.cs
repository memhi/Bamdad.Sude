using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sude.Mvc.UI.Admin.Models
{
    public class UserInfo
    {
        public string id { get; set; } = null;
        public string userName { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }

        public bool emailConfirmed { get; set; } = false;
        public string? phoneNumber { get; set; } = null;
        public bool phoneNumberConfirmed { get; set; } = false;
        public bool lockoutEnabled { get; set; } = true;
        public bool twoFactorEnabled { get; set; } = false;
        public int accessFailedCount { get; set; } = 0;
        public DateTime? lockoutEnd { get; set; } = null;
    }

    public class UserPassword
    {
        public string userId { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }

    }

    public class UserRoleInfo : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
