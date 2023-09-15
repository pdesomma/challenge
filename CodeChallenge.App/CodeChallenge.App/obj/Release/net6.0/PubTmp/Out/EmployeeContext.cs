using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Data.FileData
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }
}
