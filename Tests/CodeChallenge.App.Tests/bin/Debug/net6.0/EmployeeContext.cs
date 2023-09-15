using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Repos.FileRepo
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }
}
