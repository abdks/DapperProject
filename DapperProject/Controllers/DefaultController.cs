using Dapper;
using DapperProject.DapperContext;
using DapperProject.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DapperProject.Controllers
{
    public class DefaultController : Controller
    {
        private readonly Context _context;

        public DefaultController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 20)
        {
            int offset = (page - 1) * pageSize;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // string query = $"SELECT * FROM EQAfad WHERE province = 'Bursa' ORDER BY id OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY"; BU BURSADAKİ TÜM DEPREMLERİ LİSTELİYOR
            //string query = $"SELECT TOP 10 * FROM EQAfad ORDER BY magnitude DESC";  BU EN YÜKSEK 10 DEPREMİ LİSTELİYOR
            // string query = "SELECT TOP 1 province, COUNT(*) as EarthquakeCount FROM EQAfad GROUP BY province ORDER BY EarthquakeCount DESC";    EN ÇOK DEPREM OLAN ŞEHİR
            //string query = "SELECT TOP 1 province, COUNT(*) as EarthquakeCount FROM EQAfad GROUP BY province ORDER BY EarthquakeCount ASC";   EN AZ DEPREM OLAN ŞEHİR
            string query = $"SELECT * FROM EQAfad ORDER BY id OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY"; BU TÜM VERİLERİ LİSTELİYOR

            var connection = _context.CreateConnection();
            var values = await connection.QueryAsync<ResultProjectDto>(query);

            stopwatch.Stop();
            ViewBag.QueryExecutionTime = stopwatch.Elapsed.TotalSeconds;

            var totalCount = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM EQAfad");

            var model = new PaginatedResult<ResultProjectDto>(values.ToList(), page, pageSize, totalCount);

            return View(model);
        }
    }
}
