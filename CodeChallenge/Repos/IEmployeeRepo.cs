using CodeChallenge.Models;

namespace CodeChallenge.Repos
{
    /// <summary>
    /// Repository that performs data storage actions on <see cref="Employee"/> objects.
    /// </summary>
    public interface IEmployeeRepo
    {
        /// <summary>
        /// Adds an <see cref="Employee"/>.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Employee? Add(Employee employee);

        /// <summary>
        /// Gets a specific <see cref="Employee"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Employee?> GetByIdAsync(String id);

        /// <summary>
        /// Removes an <see cref="Employee"/>
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Employee? Remove(Employee employee);

        /// <summary>
        /// Commits changes
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}