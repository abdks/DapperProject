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
            // SQL sorgusunu kullanarak en az deprem olan şehiri bul
            string query = "SELECT TOP 1 province FROM EQAfad GROUP BY province ORDER BY COUNT(*) DESC";
            var connection = _context.CreateConnection();
            var mostAffectedCity = await connection.QueryFirstOrDefaultAsync<string>(query);
            // SQL sorgusunu kullanarak en çok deprem olan şehiri bul
            string query2 = "SELECT TOP 1 province FROM EQAfad GROUP BY province ORDER BY COUNT(*) ASC";
            var connection2 = _context.CreateConnection();
            var mostAffectedCity2 = await connection2.QueryFirstOrDefaultAsync<string>(query2);
            // 2023 yılındaki en yüksek depremin bilgilerini getir
            string query3 = "SELECT TOP 1 province, magnitude FROM EQAfad WHERE YEAR(date) = 2023 ORDER BY magnitude DESC";
            var connection3 = _context.CreateConnection();
            var highestMagnitudeEarthquake = await connection3.QueryFirstOrDefaultAsync<dynamic>(query3);

            // Viewbage'e şehir adını ve derecesini aktar
            ViewBag.HighestMagnitudeCity = highestMagnitudeEarthquake?.province;
            ViewBag.HighestMagnitude = highestMagnitudeEarthquake?.magnitude;




            // Viewbage'e şehir adını aktar
            ViewBag.MostAffectedCity = mostAffectedCity;
            ViewBag.MostAffectedCity2 = mostAffectedCity2;  
            return View();
        }
    }
}
