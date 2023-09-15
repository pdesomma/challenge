using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportStructureController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public ReportStructureController(ILogger<ReportStructureController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpGet("direct/{employeeId}", Name = "getDirectReportingStructureByEmployeeId")]
        public async Task<IActionResult> GetDirectReportingStructure(string employeeId)
        {
            _logger.LogDebug($"Received report structure get request for employee '{employeeId}'");

            var structure = await _employeeService.GetDirectReportStructureAsync(employeeId);

            if (structure is null)
            {
                _logger.LogDebug($"No structure generated for '{employeeId}'");
                return NotFound();
            }

            return Ok(structure);
        }

        [HttpGet("full/{employeeId}", Name = "getFullReportingStructureByEmployeeId")]
        public async Task<IActionResult> GetFullReportingStructure(string employeeId)
        {
            _logger.LogDebug($"Received report structure get request for employee '{employeeId}'");

            var structure = await _employeeService.GetFullReportStructureAsync(employeeId);

            if (structure is null)
            {
                _logger.LogDebug($"No structure generated for '{employeeId}'");
                return NotFound();
            }

            return Ok(structure);
        }
    }
}
