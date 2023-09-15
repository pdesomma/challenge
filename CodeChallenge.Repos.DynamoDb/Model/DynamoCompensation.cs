using Amazon.DynamoDBv2.DataModel;

namespace CodeChallenge.Repos.DynamoDB.Model
{
    [DynamoDBTable("challenge_compensation")]
    public class DynamoCompensation
    {
        public DynamoCompensation() { } 

        public DynamoCompensation(string id, string employee, double salary, DateTime effectiveDate)
        {
            Id = id;
            EmployeeId = employee;
            Salary = salary;
            EffectiveDate = effectiveDate;
        }

        [DynamoDBHashKey("id")]
        public string Id { get; set; }

        [DynamoDBProperty("employee")]
        public string EmployeeId { get; set; }

        [DynamoDBProperty("salary")]
        public double Salary { get; set; }

        [DynamoDBProperty("date")]
        public DateTime EffectiveDate {get; set; }
    }
}
