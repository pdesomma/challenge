using CodeChallenge.Models;

namespace CodeChallenge.Repos
{
    /// <summary>
    /// Repository that performs data storage actions on <see cref="Compensation"/> objects.
    /// </summary>
    public interface ICompensationRepo
    {
        /// <summary>
        /// Creates a new <see cref="Compensation"/> for an employee.
        /// </summary>
        /// <param name="compensation"></param>
        /// <returns></returns>
        Compensation? Add(Compensation compensation);

        /// <summary>
        /// Gets all <see cref="Compensation"/> objects for an employee.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        IAsyncEnumerable<Compensation> GetAllAsync(string employeeId);
    }
}
