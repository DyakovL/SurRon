using Microsoft.AspNetCore.Mvc;
using SurRon.Core.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace SurRon.Controllers
{
    public class HomeController : AdminBaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {

            if (statusCode == 400)
            {
                return View("Error404");
            }

            if (statusCode == 401)
            {
                return View("Error500");
            }

            return View();
        }
    }
}
