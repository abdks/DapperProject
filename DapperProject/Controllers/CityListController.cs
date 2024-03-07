using Microsoft.AspNetCore.Mvc;

namespace DapperProject.Controllers
{
    public class CityListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
