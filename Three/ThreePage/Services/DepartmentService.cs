using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Three.Models;

namespace Three.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly List<Department> _departments = new List<Department>();
        public int ticket { get; set; }

        public DepartmentService()
        {
            _departments.Add(new Department
            {
                Id=1,
                Name="HR",
                EmployeeCount=10,
                Location="shanghai"
            });

            _departments.Add(new Department
            {
                Id = 2,
                Name = "总经办",
                EmployeeCount = 3,
                Location = "北京"
            });

            _departments.Add(new Department
            {
                Id = 3,
                Name = "C++",
                EmployeeCount = 665,
                Location = "成都"
            });

            _departments.Add(new Department
            {
                Id = 4,
                Name = "架构",
                EmployeeCount = 23,
                Location = "成都"
            });

            _departments.Add(new Department
            {
                Id = 5,
                Name = "手游",
                EmployeeCount = 44,
                Location = "深圳"
            });
        }
        public Task Add(Department department)
        {
            if (department==null) return null;
            department.Id = _departments.Max(m => m.Id) + 1;
            _departments.Add(department);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Department>> GetAll()
        {
            return Task.Run(function: () => _departments.AsEnumerable());
        }

        public Task<Department> GetById(int id)
        {
            return Task.Run(() => _departments.FirstOrDefault(m => m.Id == id));
        }

        public Task<CompanySummary> GetCompanySummary()
        {
            return Task.Run(() =>
            {
                return new CompanySummary
                {
                    EmployeeCount = _departments.Sum(m => m.EmployeeCount),
                    AverageDepartmentEmployeeCount = (int)_departments.Average(m => m.EmployeeCount)
                };
            });
        }

        public Task<int> TicketAdd()
        {
            return Task.FromResult(++ticket);
        }
    }
}
