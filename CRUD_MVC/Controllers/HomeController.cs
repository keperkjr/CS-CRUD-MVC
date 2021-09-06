using CRUD_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static List<CRUD_MVC.Models.Employee> employees = null;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (employees == null)
            {
                FetchEmployees();
            }

            return View(employees);
        }
        
        private static void FetchEmployees()
        {
            var url = "https://jsonplaceholder.typicode.com/users";
            var response = Utils.WebRequest.Get(url);
            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                employees = Utils.Json.Deserialize<List<CRUD_MVC.Models.Employee>>(response.Body);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ShowEmployees()
        {
            return ViewComponent("EmployeeTable", new { employees = new List<CRUD_MVC.Models.Employee> { new CRUD_MVC.Models.Employee { id = 1 } } });
        }
    }
}
