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
using System.Xml.Linq;

namespace HRISBlazorServerApp.Models
{
    public class TokenConfig
    {
        public string CurrentAccessToken { get; private set; }

        public event Func<Task> Notify;

        public void SetToken(string name)
        {
            CurrentAccessToken = name;
            Notify?.Invoke();
        }
    }

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
