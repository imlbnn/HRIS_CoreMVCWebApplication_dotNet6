using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRISBlazorServerApp.Model
{
    //public class TokenProvider
    //{
    //    public string AccessToken { get; set; }
    //    public string RefreshToken { get; set; }
    //}
    public class TokenProvider : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                AccessToken = token;

            }
            return Page();
        }

        public string AccessToken { get; set; }
    }
}
