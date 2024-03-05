using Dapper;
using DapperProject.DapperContext;
using DapperProject.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DapperProject.Controllers
{
    public class DefaultController : Controller
    {
        private readonly Context _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public DefaultController(Context context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

      
        


        public async Task<IActionResult> Index(int page = 1, int pageSize = 100)
        {
            int offset = (page - 1) * pageSize;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            string query = $"SELECT * FROM EQAfad ORDER BY id OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";

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
