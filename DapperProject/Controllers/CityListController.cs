﻿using Dapper;
using DapperProject.DapperContext;
using DapperProject.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DapperProject.Controllers
{
    public class CityListController : Controller
    {
        private readonly Context _context;

        public CityListController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            string query = "SELECT province AS sehir, COUNT(*) AS depremSayisi FROM EQAfad WHERE YEAR(date) = 2023 GROUP BY province ORDER BY province";

            var connection = _context.CreateConnection();
            var cityList = await connection.QueryAsync<dynamic>(query);

            stopwatch.Stop();
            ViewBag.QueryExecutionTime = stopwatch.Elapsed.TotalSeconds;

            ViewBag.CityList = cityList;

            return View();
        }
    }
}
