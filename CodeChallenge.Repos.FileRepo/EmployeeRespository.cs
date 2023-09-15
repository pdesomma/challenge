using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Repos.FileRepo
{
    public class EmployeeRespository : IEmployeeRepo
    {
        private readonly EmployeeContext _employeeContext;

        public EmployeeRespository(EmployeeContext employeeContext) 
        {
            _employeeContext = employeeContext;
        }

        /// <summary>
        /// Adds an <see cref="Employee"/>.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public Employee? Add(Employee employee)
        {
            if(employee is not null)
            {
                employee.EmployeeId = Guid.NewGuid().ToString();
                _employeeContext.Employees.Add(employee);
            }
            return employee;
        }

        // Added an .Include to get all the child data to populate in the response.
        public async Task<Employee?> GetByIdAsync(string id)
        {
            return await _employeeContext.Employees.Include(e => e.DirectReports).AsQueryable().SingleOrDefaultAsync(e => e.EmployeeId == id);
        }

        /// <summary>
        /// Saves changes.
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            await _employeeContext.SaveChangesAsync();
        }

        /// <summary>
        /// Removes an <see cref="Employee" />
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }
    }
}
