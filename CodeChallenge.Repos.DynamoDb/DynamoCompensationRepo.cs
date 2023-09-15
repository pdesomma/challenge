using Amazon.DynamoDBv2.DataModel;
using CodeChallenge.Models;
using CodeChallenge.Repos.DynamoDB.Model;

namespace CodeChallenge.Repos.DynamoDb
{
    public class DynamoCompensationRepo : ICompensationRepo
    {
        private IDynamoDBContext _context;

        public DynamoCompensationRepo(IDynamoDBContext context) 
        {
            this._context = context;
        }

        /// <summary>
        /// Inserts new <see cref="Compensation"/> data into DynamoDb.
        /// </summary>
        /// <param name="compensation"></param>
        /// <returns></returns>
        public Compensation? Add(Compensation compensation)
        {
            if(compensation is not null)
            {
                compensation.Id = Guid.NewGuid().ToString();
                var dc = new DynamoCompensation(compensation.Id, compensation.EmployeeId, compensation.Salary, compensation.EffectiveDate);
                _context.SaveAsync(dc).Wait();
                return compensation;
            }
            return null;
        }

        /// <summary>
        /// Gets all <see cref="Compensation"/> items for an employee from DynamoDb.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<Compensation> GetAllAsync(string employeeId)
        {
            foreach (var c in await _context.ScanAsync<DynamoCompensation>(default).GetRemainingAsync())
            {
                if (c.EmployeeId == employeeId)
                    yield return new Compensation()
                    {
                        Id = c.Id,
                        EmployeeId = c.EmployeeId,
                        Salary = c.Salary,
                        EffectiveDate = c.EffectiveDate
                    };
            }
        }
    }
}
