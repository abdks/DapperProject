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

            string query = $"SELECT * FROM EQAfad WHERE province = 'Bursa' ORDER BY id OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";

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
