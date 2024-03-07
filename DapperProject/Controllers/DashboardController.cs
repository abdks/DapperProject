using Dapper;
using DapperProject.DapperContext;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DapperProject.Controllers
{
    public class DashboardController : Controller
    {
        private readonly Context _context;

        public DashboardController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // SQL sorgusunu kullanarak en çok deprem olan şehiri bul
            string query = "SELECT TOP 1 province FROM EQAfad GROUP BY province ORDER BY COUNT(*) DESC";
            var connection = _context.CreateConnection();
            var mostAffectedCity = await connection.QueryFirstOrDefaultAsync<string>(query);

            string query2 = "SELECT TOP 1 province FROM EQAfad GROUP BY province ORDER BY COUNT(*) ASC";
            var connection2 = _context.CreateConnection();
            var mostAffectedCity2 = await connection2.QueryFirstOrDefaultAsync<string>(query2);

            // Viewbage'e şehir adını aktar
            ViewBag.MostAffectedCity = mostAffectedCity;
            ViewBag.MostAffectedCity2 = mostAffectedCity2;  
            return View();
        }
    }
}
