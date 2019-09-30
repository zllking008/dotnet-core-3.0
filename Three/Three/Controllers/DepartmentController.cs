using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Three.Models;
using Three.Services;

namespace Three.Controllers
{
    public class DepartmentController:Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IOptions<ThreeOptions> _threeOptions;

        public DepartmentController(IDepartmentService departmentService,IOptions<ThreeOptions> threeOptions)
        {
            _departmentService = departmentService;
            _threeOptions = threeOptions;
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.Title = "部门首页";
            var departments = await _departmentService.GetAll();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Title = "添加首页";
            return View(new Department());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Department model)
        {
            if (ModelState.IsValid)
            {
                await _departmentService.Add(model);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
