using CodeChallenge.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace CodeChallenge.Repos.FileRepo
{

    public class EmployeeDataSeeder
    {
        private EmployeeContext _employeeContext;
        private const string EMPLOYEE_SEED_DATA_FILE = "/resources/EmployeeSeedData.json";

        public EmployeeDataSeeder(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        public async Task Seed()
        {
            if (!_employeeContext.Employees.Any())
            {
                List<Employee> employees = LoadEmployees();
                _employeeContext.Employees.AddRange(employees);

                await _employeeContext.SaveChangesAsync();
            }
        }

        private List<Employee> LoadEmployees()
        {
            using (FileStream fs = new FileStream(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + EMPLOYEE_SEED_DATA_FILE, FileMode.Open))
            using (StreamReader sr = new StreamReader(fs))
            using (JsonReader jr = new JsonTextReader(sr))
            {
                List<Employee> employees = new JsonSerializer().Deserialize<List<Employee>>(jr);
                FixUpReferences(employees);

                return employees;
            }
        }

        private void FixUpReferences(List<Employee> employees)
        {
            var employeeIdRefMap = from employee in employees
                                select new { Id = employee.EmployeeId, EmployeeRef = employee };

            foreach( Employee employee in employees)
            {
                if (employee.DirectReports != null)
                {
                    var referencedEmployees = new List<Employee>(employee.DirectReports.Count);
                    employee.DirectReports.ForEach(report =>
                    {
                        var referencedEmployee = employeeIdRefMap.First(e => e.Id == report.EmployeeId).EmployeeRef;
                        referencedEmployees.Add(referencedEmployee);
                    });
                    employee.DirectReports = referencedEmployees;
                }
            }
        }
    }
}
