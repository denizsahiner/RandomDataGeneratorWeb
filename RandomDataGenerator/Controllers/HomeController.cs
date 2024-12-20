using Microsoft.AspNetCore.Mvc;
using RandomDataGenerator.Services;  // DataGeneratorService için doðru namespace
using System.Collections.Generic;

namespace RandomDataGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataGeneratorService _dataGeneratorService;

        public HomeController(DataGeneratorService dataGeneratorService)
        {
            _dataGeneratorService = dataGeneratorService;
        }

        // Ana sayfa (Index) aksiyonu
        public IActionResult Index()
        {
            return View();
        }

        // Privacy aksiyonu, string türünde rastgele veri üretip Privacy view'ine gönderir
        public IActionResult Privacy()
        {
            var randomString = _dataGeneratorService.GenerateRandomData("string");
            ViewData["RandomData"] = randomString; // ViewData ile gönderiyoruz
            return View();
        }

        // Error aksiyonu, string türünde rastgele veri üretip Error view'ine gönderir
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var randomString = _dataGeneratorService.GenerateRandomData("string");
            ViewData["RandomData"] = randomString; // ViewData ile gönderiyoruz
            return View();
        }
    }
}