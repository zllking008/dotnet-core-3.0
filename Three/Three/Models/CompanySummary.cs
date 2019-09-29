using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Three.Models
{
    public class CompanySummary
    {
        public int EmployeeCount { get; set; }

        /// <summary>
        /// 每个部门平均员工数量
        /// </summary>
        public int AverageDepartmentEmployeeCount { get; set; }
    }
}
