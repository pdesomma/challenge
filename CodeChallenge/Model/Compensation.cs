using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public double Salary { get; set; }
        public DateTime EffectiveDate {get; set; }
    }
}
