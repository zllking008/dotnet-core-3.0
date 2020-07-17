using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Three.Services;

namespace ThreePage.Pages.Department
{
    public class AddDepartmentModel : PageModel
    {
        private readonly IDepartmentService _departmentService;

        [BindProperty]
        public Three.Models.Department Department { get; set; }
        //public void OnGet()
        //{
        //}
        public AddDepartmentModel(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _departmentService.Add(Department);
            return RedirectToPage("/Index");

        }

        // ajax访问 ：/Department/AddDepartment?handler=Add
        public async Task<IActionResult> OnPostAdd([FromBody]Three.Models.Department model)
        {
            if(model==null)
                return new JsonResult(new { isSuccess = false, msg = "添加格式不正确" });
            await _departmentService.Add(model);
            return  new JsonResult(new { isSuccess = true, msg = "添加成功" });//返回的json数据变量命名格式是驼峰形式，为了保持一致后台命名的时候也尽量是驼峰形式
        }
    }
}
