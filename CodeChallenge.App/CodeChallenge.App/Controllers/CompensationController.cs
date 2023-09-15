using CodeChallenge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public CompensationController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received compensation create request for '{compensation.EmployeeId}'");

            compensation = await _employeeService.CreateCompensationAsync(compensation);
            if (compensation is not null)
            {
                return Created("createCompensation", compensation);

            }
            return NotFound();
        }

        [HttpGet("{employeeId}", Name = "getCompensationsById")]
        public IActionResult GetCompensations(string employeeId)
        {
            _logger.LogDebug($"Received compensations get request for '{employeeId}'");

            return Ok(_employeeService.GetCompensationsAsync(employeeId));
        }
    }
}
