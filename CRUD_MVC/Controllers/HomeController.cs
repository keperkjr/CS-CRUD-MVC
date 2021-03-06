// https://www.taniarascia.com/getting-started-with-vue/
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
            Debug.Print(Utils.Methods.GetIPv4Address(HttpContext));

            return ShowIndex();
        }

        private IActionResult ShowIndex(int? id = null)
        {
            if (employees == null)
            {
                FetchEmployees();
            }

            foreach (var key in TempData.Keys)
            {
                ViewData[key] = TempData[key];
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
            ViewData["PageMessage"] = "";
            ViewData["PageSubmit_Status"] = CRUD_MVC.ViewComponents.PageStatus.None;

            if (string.IsNullOrWhiteSpace(employeeView.name))
            {
                ModelState.AddModelError(nameof(employeeView.name), "A valid name is required");
            }
            if (string.IsNullOrWhiteSpace(employeeView.email))
            {
                ModelState.AddModelError(nameof(employeeView.email), "A valid email is required");
            }

            if (ModelState.IsValid)
            {
                switch (employeeView.action)
                {
                    case CRUD_MVC.Models.EmployeeViewModel.Action.Create:
                        AddEmployee(employeeView);
                        ViewData["PageMessage"] = $"{employeeView.name} successfully created";
                        break;
                    case CRUD_MVC.Models.EmployeeViewModel.Action.Update:
                        UpdateEmployee(employeeView);
                        ViewData["PageMessage"] = $"{employeeView.name} successfully updated";
                        break;
                }

                // If returning back to the same view, clear model entries
                ModelState.Clear();
                ViewData["PageSubmit_Status"] = CRUD_MVC.ViewComponents.PageStatus.Success;
            } 
            else
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors })
                    .ToList();

                ViewData["PageSubmit_Status"] = CRUD_MVC.ViewComponents.PageStatus.Error;
                ViewData["PageMessage"] = "Please fill out all required fields";
            }

            //return RedirectToAction(nameof(Index));
            return ShowIndex();
        }

        private static void AddEmployee(CRUD_MVC.Models.EmployeeViewModel employeeView)
        {
            int lastId = employees.Count > 0 ? employees.Max((x) => x.id) : 0;
            //var employee = new CRUD_MVC.Models.Employee();
            //employee.id = lastId + 1;
            //employee.name = employeeView.name;
            //employee.email = employeeView.email;
            //employees.Add(employee);

            var payload = Utils.Json.Serialize(employeeView);
            var url = "https://jsonplaceholder.typicode.com/users";
            var response = Utils.WebRequest.Post(url, payload, new Utils.WebRequest.Options() {
                ContentType = Utils.WebRequest.ContentType.ApplicationJson
            });

            var newEmployee = Utils.Json.Deserialize<CRUD_MVC.Models.Employee>(response.Body);
            newEmployee.id = lastId + 1;
            employees.Add(newEmployee);

        }

        private static void UpdateEmployee(CRUD_MVC.Models.EmployeeViewModel employeeView)
        {
            if (employeeView.id < 11)
            {
                var payload = Utils.Json.Serialize(employeeView);
                var url = $"https://jsonplaceholder.typicode.com/users/{employeeView.id}";
                var response = Utils.WebRequest.Put(url, payload, new Utils.WebRequest.Options()
                {
                    ContentType = Utils.WebRequest.ContentType.ApplicationJson
                });

                var newEmployee = Utils.Json.Deserialize<CRUD_MVC.Models.Employee>(response.Body);
                var index = employees.FindIndex(x => x.id == employeeView.id);
                employees[index] = newEmployee;
            } 
            else
            {
                var employee = employees.FirstOrDefault((x) => x.id == employeeView.id);
                employee.name = employeeView.name;
                employee.email = employeeView.email;
            }
        }

        private static void DeleteEmployee(int id)
        {
            var url = $"https://jsonplaceholder.typicode.com/users/{id}";
            var response = Utils.WebRequest.Delete(url);

            var index = employees.FindIndex(x => x.id == id);
            employees.RemoveAt(index);
        }

        public IActionResult Edit(int id)
        {
            //return RedirectToAction(nameof(Index), new { id = id });
            return ShowIndex(id);
        }

        public IActionResult Delete(int id)
        {
            var employee = employees.FirstOrDefault((x) => x.id == id);
            DeleteEmployee(id);

            TempData["PageMessage"] = $"{employee.name} successfully removed";
            TempData["PageSubmit_Status"] = CRUD_MVC.ViewComponents.PageStatus.Success;

            //return ShowIndex();
            return RedirectToAction(nameof(Index));
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
