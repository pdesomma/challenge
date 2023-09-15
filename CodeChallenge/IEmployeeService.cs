using CodeChallenge.Models;

namespace CodeChallenge
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Creates a <see cref="Compensation"/> item for an employee.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<Compensation?> CreateCompensationAsync(Compensation compensation);

        /// <summary>
        /// Creates an <see cref="Employee"/> in the system.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Task<Employee?> CreateEmployeeAsync(Employee employee);

        /// <summary>
        /// Get all <see cref="Compensation"/>s for an employee.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        IAsyncEnumerable<Compensation> GetCompensationsAsync(string employeeId);

        /// <summary>
        /// Returns the direct <see cref="ReportingStructure"/> for an <see cref="Employee"/>.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<ReportingStructure?> GetDirectReportStructureAsync(string employeeId);

        /// <summary>
        /// Gets an <see cref="Employee"/> from the system.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Employee?> GetEmployeeAsync(string id);

        /// <summary>
        /// Returns the full <see cref="ReportingStructure"/> for an <see cref="Employee"/>.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<ReportingStructure?> GetFullReportStructureAsync(string employeeId);

        /// <summary>
        /// Removes <paramref name="originalEmployee"/> from the system and adds <paramref name="newEmployee" />.
        /// </summary>
        /// <param name="originalEmployee"></param>
        /// <param name="newEmployee"></param>
        /// <returns></returns>
        Task<Employee?> ReplaceEmployeeAsync(Employee originalEmployee, Employee newEmployee);
    }
}
