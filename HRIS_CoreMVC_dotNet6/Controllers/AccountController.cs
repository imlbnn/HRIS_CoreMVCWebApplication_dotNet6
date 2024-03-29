﻿using HRIS.Application.Common.Models;
using HRIS.Infrastructure;
using HRIS.Infrastructure.Identity;
using HRIS.Net6_CQRSApproach.Model;
using HRIS_CoreMVC_dotNet6.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System;
using System.Security.Claims;

namespace HRIS_CoreMVC_dotNet6.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
                UserManager<ApplicationUser> userManager,
                RoleManager<IdentityRole> roleManager,
                ILogger<AccountController> logger
                )
        {

            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (ModelState.IsValid)
            {
                var entity = await _userManager.FindByNameAsync(loginRequest.Username);

                if (entity == null)
                {
                    ViewBag._isError = true;
                    ViewBag._message = "User does not exist";


                    return View();
                }

                var isCorrect = await _userManager.CheckPasswordAsync(entity, loginRequest.Password);

                if (!isCorrect)
                {
                    ViewBag._isError = true;
                    ViewBag._message = "Invalid password";


                    return View();
                }

                List<Claim> claimsIdentities =  new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, entity.Id),
                    new Claim(ClaimTypes.GivenName, entity.UserName),
                    new Claim(ClaimTypes.Email, entity.Email)
                };

                ClaimsIdentity identity = new ClaimsIdentity(claimsIdentities, CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                HttpContext.Session.SetString("UserId", entity.Id);

                HttpContext.Request.Headers.Add("X-Authenticated-Client-User", entity.UserName);

                ViewBag._isError = false;

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);

                return new RedirectResult("/home");
            }


            ViewBag._isError = true;
            ViewBag._message = "Invalid payload";

            return View();
        }


        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync();

            return new RedirectResult("/");
        }

        [Route("accessdenied")]
        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }

    }
}
