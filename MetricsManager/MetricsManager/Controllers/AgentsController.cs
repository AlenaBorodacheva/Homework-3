using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsCommon;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;
        public AgentsController(ILogger<AgentsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _logger.LogInformation("Сообщение из AgentsController из метода RegisterAgent");
            _logger.LogInformation($"{agentInfo.AgentId}, {agentInfo.AgentAddress}");
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation("Сообщение из AgentsController из метода EnableAgentById");
            _logger.LogInformation($"{agentId}");
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation("Сообщение из AgentsController из метода DisableAgentById");
            _logger.LogInformation($"{agentId}");
            return Ok();
        }

        [HttpGet("read")]
        public IActionResult ListOfRegisteredObjects()
        {
            _logger.LogInformation("Сообщение из AgentsController из метода ListOfRegisteredObjects");
            return Ok();
        }
    }
}
