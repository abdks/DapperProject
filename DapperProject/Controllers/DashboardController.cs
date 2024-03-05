using Microsoft.AspNetCore.Mvc;

namespace DapperProject.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
