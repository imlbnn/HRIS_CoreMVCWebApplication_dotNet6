using HRIS_CoreMVC_dotNet6.Helpers;
using HRIS_CoreMVC_dotNet6.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing.Text;

namespace HRIS_CoreMVC_dotNet6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            var data = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            if (data == null)
            {
                ViewBag._isError = true;
                ViewBag._message = "Session Expired";
                await HttpContext.SignOutAsync();

                return View("../Account/Login");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}