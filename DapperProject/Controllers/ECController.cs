using Dapper;
using DapperProject.DapperContext;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DapperProject.Controllers
{
    public class ECController : Controller
    {
        private readonly Context _context;

        public ECController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            string query = "SELECT TOP 10 province AS sehir, COUNT(*) AS depremSayisi FROM EQAfad GROUP BY province ORDER BY depremSayisi DESC";

            var connection = _context.CreateConnection();
            var cityList = await connection.QueryAsync<dynamic>(query);

            stopwatch.Stop();
            ViewBag.QueryExecutionTime = stopwatch.Elapsed.TotalSeconds;

            ViewBag.CityList = cityList;

            return View();
        }
    }
}
