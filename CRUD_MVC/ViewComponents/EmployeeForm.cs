using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_MVC.ViewComponents
{
    public class EmployeeForm : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(CRUD_MVC.Models.Employee employee = null)
        {
            var model = new CRUD_MVC.Models.EmployeeViewModel();
            model.operation = CRUD_MVC.Models.EmployeeViewModel.Action.Create;
            if (employee != null) {
                model.id = employee.id;
                model.name = employee.name;
                model.email = employee.email;
                model.operation = CRUD_MVC.Models.EmployeeViewModel.Action.Update;
            }
            return await Task.FromResult((IViewComponentResult)View(model));
        }

    }
}
