using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_MVC.ViewComponents
{
    public class EmployeeTableViewComponent : ViewComponent
    {
        public EmployeeTableViewComponent()  { }

        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<CRUD_MVC.Models.Employee> employees)
        {
            return await Task.FromResult((IViewComponentResult)View("EmployeeTable", employees));
        }

    }
}
