using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RadioFreeEurope.Data;
using RadioFreeEurope.Models;
using System.Threading.Tasks;

namespace RadioFreeEurope.Controllers
{
    [ApiController]
    public class DiffController : ControllerBase
    {
        private readonly IDiff _diff;

        private readonly ILogger<DiffController> _logger;

        public DiffController(ILogger<DiffController> logger, IDiff diff) 
        {
            (_diff, _logger) = (diff, logger);
        }

        [Route("v1/[controller]/{ID}/left")]
        [HttpPost]
        public async Task<Response> Left([FromBody]string value, int ID)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _logger.LogInformation($"Received data (ID: {ID} and {value} with type left)");
                return await _diff.AddDiffAsync(new Diff(ID, value, DiffType.Left));
            }
            else return new Response(200, "Empty value");
            
        }

        [Route("v1/[controller]/{ID}/right")]
        [HttpPost]
        public async Task<Response> Right([FromBody] string value, int ID)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _logger.LogInformation($"Received data (ID: {ID} and {value} with type right)");
                return await _diff.AddDiffAsync(new Diff(ID, value, DiffType.Right));
            }
            else return new Response(200, "Empty value");
        }

        [Route("v1/[controller]/{ID}")]
        [HttpPost]
        public async Task<Response> Diff(int ID)
        {
            _logger.LogInformation($"Checking diffs with ID: {ID}");
            return await _diff.GetDiffAsync(ID);
        }
    }
}
