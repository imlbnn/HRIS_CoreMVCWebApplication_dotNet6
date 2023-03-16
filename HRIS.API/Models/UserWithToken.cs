using HRIS.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRIS.API.Models
{
    public class UserWithToken : ApplicationUser
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

    }
}
