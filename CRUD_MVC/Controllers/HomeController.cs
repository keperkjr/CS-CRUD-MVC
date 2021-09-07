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
            return ShowIndex();
        }

        private IActionResult ShowIndex(int? id = null)
        {
            if (employees == null)
            {
                FetchEmployees();
            }
            var model = new CRUD_MVC.Models.IndexViewModel()
            {
                employees = employees,
                employee = id.HasValue ? employees.FirstOrDefault(x => x.id == id) : null
            };
            return View(nameof(Index), model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Index(CRUD_MVC.Models.EmployeeViewModel employeeView)
        {
            ViewData["FormMessage"] = "";
            ViewData["FormSubmit_Status"] = ViewComponents.EmployeeForm.Status.None;

            if (string.IsNullOrWhiteSpace(employeeView.name))
            {
                ModelState.AddModelError(nameof(employeeView.name), "A valid name is required");
            }
            if (string.IsNullOrWhiteSpace(employeeView.email))
            {
                ModelState.AddModelError(nameof(employeeView.email), "A valid email is required");
            }

            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToList();

            if (ModelState.IsValid)
            {
                CRUD_MVC.Models.Employee employee = null;
                switch (employeeView.action)
                {
                    case CRUD_MVC.Models.EmployeeViewModel.Action.Add:
                        int lastId = employees.Count > 0 ? employees.Max((x) => x.id) : 0;
                        employee = new CRUD_MVC.Models.Employee();
                        employee.id = lastId + 1;
                        employee.name = employeeView.name;
                        employee.email = employeeView.email;
                        employees.Add(employee);
                        ViewData["FormMessage"] = "Employee successfully added";
                        break;
                    case CRUD_MVC.Models.EmployeeViewModel.Action.Update:
                        employee = employees.FirstOrDefault((x) => x.id == employeeView.id);
                        employee.name = employeeView.name;
                        employee.email = employeeView.email;
                        ViewData["FormMessage"] = "Employee successfully updated";
                        ViewData["FormSubmit_Status"] = true;
                        break;
                }

                // If returning back to the same view, clear model entries
                ModelState.Clear();
                ViewData["FormSubmit_Status"] = ViewComponents.EmployeeForm.Status.Success;
            } 
            else
            {
                ViewData["FormSubmit_Status"] = ViewComponents.EmployeeForm.Status.Error;
                ViewData["FormMessage"] = "Please fill out all required fields";
            }

            //return RedirectToAction(nameof(Index));
            return ShowIndex();
        }

        public IActionResult Edit(int id)
        {
            //return RedirectToAction(nameof(Index), new { id = id });
            return ShowIndex(id);
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
