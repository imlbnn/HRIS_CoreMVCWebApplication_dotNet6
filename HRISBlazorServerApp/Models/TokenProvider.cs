using HRISBlazorServerApp.Interfaces;
using HRISBlazorServerApp.Interfaces.Services;
using HRISBlazorServerApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Threading.Tasks;

namespace HRISBlazorServerApp.Models
{
    public class TokenConfig
    {
        public string CurrentAccessToken { get; set; }
    }

    public class TokenProvider : PageModel
    {
        private readonly ITokenProviderService _tokenProviderService;
        public TokenProvider(ITokenProviderService tokenProviderService)
        {
            _tokenProviderService= tokenProviderService;
        }

        public async Task<IActionResult> OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                
                AccessToken = token;
            }

            if (string.IsNullOrEmpty(AccessToken))
            {
                var res = await _tokenProviderService.IsValidToken(CurrentAccessToken);

                if (res)
                    AccessToken = CurrentAccessToken;
                else
                    CurrentAccessToken = string.Empty;
            }

            return Page();
        }

        public string AccessToken { get; set; }

        public string CurrentAccessToken { get; set; }
    }
}
