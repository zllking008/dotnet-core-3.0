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

        // ajax���� ��/Department/AddDepartment?handler=Add
        public async Task<IActionResult> OnPostAdd([FromBody]Three.Models.Department model)
        {
            if(model==null)
                return new JsonResult(new { isSuccess = false, msg = "��Ӹ�ʽ����ȷ" });
            await _departmentService.Add(model);
            return  new JsonResult(new { isSuccess = true, msg = "��ӳɹ�" });//���ص�json���ݱ���������ʽ���շ���ʽ��Ϊ�˱���һ�º�̨������ʱ��Ҳ�������շ���ʽ
        }
    }
}
