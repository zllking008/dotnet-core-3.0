using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Three.Models;
using Three.Services;

namespace Three.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IDepartmentService departmentService,IEmployeeService employeeService)
        {
            _departmentService = departmentService;
            _employeeService = employeeService;
        }


        public async Task<IActionResult> Index(int departmentId)
        {
            var department = await _departmentService.GetById(departmentId).ConfigureAwait(false);
            ViewBag.Title = $"部门-{department.Name}";
            ViewBag.DepartmentId = departmentId;
            var employees = await _employeeService.GetByDepartmentId(departmentId);
            return View(employees);
        }

        public IActionResult Add(int departmentId)
        {
            ViewBag.Title = "添加员工";
            return View(new Employee
            {
                DepartmentId=departmentId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(Employee model)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.Add(model);
            }
            return RedirectToAction(nameof(Index), new { departmentId = model.DepartmentId });
        }

        public async Task<IActionResult> Fire(int employeeId)
        {
            var employee = await _employeeService.Fire(employeeId);
            return RedirectToAction(nameof(Index), new { departmentId = employee.DepartmentId });
        }
    }
}
