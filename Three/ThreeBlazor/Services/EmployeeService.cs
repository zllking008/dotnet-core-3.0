using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeBlazor.Models;

namespace ThreeBlazor.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly List<Employee> _employees = new List<Employee>();

        public EmployeeService()
        {
            _employees.Add(new Employee
            {
                 Id=1,
                 DepartmentId=1,
                 FirstName="zhao",
                 LastName="kunming",
                  Gender=Gender.男
            });
            _employees.Add(new Employee
            {
                Id = 2,
                DepartmentId = 2,
                FirstName = "zhang",
                LastName = "tianai",
                Gender = Gender.女
            });
            _employees.Add(new Employee
            {
                Id = 3,
                DepartmentId = 1,
                FirstName = "zhou",
                LastName = "jay",
                Gender = Gender.男
            });
            _employees.Add(new Employee
            {
                Id = 4,
                DepartmentId = 1,
                FirstName = "lon",
                LastName = "bbbbaaa",
                Gender = Gender.男
            });
        }
        public Task Add(Employee employee)
        {
            employee.Id = _employees.Max(m => m.Id) + 1;
            _employees.Add(employee);
            return Task.CompletedTask;
        }

        public Task<Employee> Fire(int id)
        {
            return Task.Run(() =>
            {
                var employee = _employees.FirstOrDefault(m => m.Id == id);
                if (employee != null)
                {
                    employee.Fired = true;
                    return employee;
                }
                return null;
            });
        }

        public Task<IEnumerable<Employee>> GetByDepartmentId(int departmentId)
        {
            return Task.Run(() => _employees.Where(m => m.DepartmentId == departmentId));
        }
    }
}
