
using CodeChallenge.Models;
using CodeChallenge.Repos;
using Microsoft.Extensions.Logging;

namespace CodeChallenge
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo _employeeRepo;
        private readonly ICompensationRepo _compensationRepo;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(
            ILogger<EmployeeService> logger, 
            IEmployeeRepo employeeRepo,
            ICompensationRepo compensationRepo)
        {
            _compensationRepo = compensationRepo;
            _employeeRepo = employeeRepo;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new <see cref="Compensation"/> for an employee.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Compensation?> CreateCompensationAsync(Compensation compensation)
        {
            if(await GetEmployeeAsync(compensation.EmployeeId) is not null)
                return _compensationRepo.Add(compensation);

            return null;
        }

        /// <summary>
        /// Creates an <see cref="Employee"/> in the system.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<Employee?> CreateEmployeeAsync(Employee employee)
        {
            if(employee is not null)
            {
                _employeeRepo.Add(employee);
                await _employeeRepo.SaveAsync();
            }
            return employee;
        }

        /// <summary>
        /// Gets all <see cref="Compensation"/>s for an employee.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IAsyncEnumerable<Compensation> GetCompensationsAsync(string employeeId) => _compensationRepo.GetAllAsync(employeeId);

        /// <summary>
        /// Gets an <see cref="Employee"/> from the system.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Employee?> GetEmployeeAsync(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                return await _employeeRepo.GetByIdAsync(id);
            }

            return null;
        }

        /// <summary>
        /// Returns the direct <see cref="ReportingStructure"/> for an <see cref="Employee"/>.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<ReportingStructure?> GetDirectReportStructureAsync(string employeeId)
        {
            var employee = await GetEmployeeAsync(employeeId);

            if (employee is null) return null;
            return new ReportingStructure
            {
                Employee = employee,
                NumberOfReports = employee.DirectReports?.Count ?? 0
            };
        }

        /// <summary>
        /// Returns the full <see cref="ReportingStructure"/> for an <see cref="Employee"/>.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<ReportingStructure?> GetFullReportStructureAsync(string employeeId)
        {
            var employee = await GetEmployeeAsync(employeeId);

            if (employee is null) return null;

            int reportCount = await BuildFullReportStructureAsync(employee);
            return new ReportingStructure
            {
                Employee = employee,
                NumberOfReports = reportCount
            };
        }

        /// <summary>
        /// Removes <paramref name="originalEmployee"/> from the system and adds <paramref name="newEmployee" />.
        /// </summary>
        /// <param name="originalEmployee"></param>
        /// <param name="newEmployee"></param>
        /// <returns></returns>
        public async Task<Employee?> ReplaceEmployeeAsync(Employee originalEmployee, Employee newEmployee)
        {
            // i'm changing this method because you shouldn't be able to remove the original employee
            // if there isn't a new employee to take it's place and you're calling the thing "Replace".
            if(originalEmployee is not null && newEmployee is not null)
            {
                _employeeRepo.Remove(originalEmployee);

                // ensure the original has been removed, otherwise EF will complain another entity w/ same id already exists
                // Pat:   _employeeRepo is an interface but the consuming class is being forced to have knowledge of its implementation.
                //        If at all possible, fix the implementation and don't make it EmployeeService's problem.
                await _employeeRepo.SaveAsync();
                _employeeRepo.Add(newEmployee);
                
                // overwrite the new id with previous employee id
                newEmployee.EmployeeId = originalEmployee.EmployeeId;

                await _employeeRepo.SaveAsync();
            }

            return newEmployee;
        }

        /// <summary>
        /// Recursively builds the full reporting structure for an <see cref="Employee"/> and returns the number of total reports.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        private async Task<int> BuildFullReportStructureAsync(Employee employee)
        {
            if (employee is null) return 0;

            int total = 0;
            for (int i = 0; i < (employee!.DirectReports?.Count ?? 0); i++)
            {
                employee.DirectReports![i] = await GetEmployeeAsync(employee!.DirectReports![i].EmployeeId)!;

                /*
                 * BuildFullReportStructure returns the total reports under DirectReports[i], 
                 * but we need to add 1 to include DirectReports[i] themselves
                 */
                total += 1 + await BuildFullReportStructureAsync(employee.DirectReports[i]);
            }
            return total;
        }
    }
}
