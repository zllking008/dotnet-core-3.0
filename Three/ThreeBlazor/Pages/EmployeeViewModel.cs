using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ThreeBlazor.Services;

namespace ThreeBlazor.Pages
{
    public class EmployeeViewModel:ComponentBase
    {
        /// <summary>
        /// 传参只能是字符串类型
        /// </summary>
        [Parameter]
        public string DepartmentId { get; set; }

        public IEnumerable<Models.Employee> Employees;

        /// <summary>
        /// 注入
        /// </summary>
        [Inject]
        protected IEmployeeService EmployeeService { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            Employees =await EmployeeService.GetByDepartmentId(int.Parse(DepartmentId));
        }
    }
}
