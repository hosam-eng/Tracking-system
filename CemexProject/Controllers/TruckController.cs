using Microsoft.AspNetCore.Mvc;

namespace CemexProject.Controllers
{
    public class TruckController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
