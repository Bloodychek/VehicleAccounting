using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VehicleAccounting.Models;

namespace VehicleAccounting.Controllers
{
    public class HomeController : Controller
    {
        private readonly MainContext mainContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, MainContext mainContext)
        {
            this.mainContext = mainContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var a = mainContext.transports.AsEnumerable();
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