using Microsoft.AspNetCore.Mvc;

namespace RandomDataGenerator.Controllers
{
    public class RandomDataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

}
