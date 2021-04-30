using System;
using System.Collections.Generic;
using MetricsManager.Controllers.Requests;
using MetricsManager.Controllers.Responses;
using MetricsManager.DataBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    public class AgentsController : ApiController
    {
        private readonly IAgentsRepository _repository;
        private readonly ILogger<AgentsController> _logger;

        public AgentsController(IAgentsRepository repository, ILogger<AgentsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        
        
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] RegisterAgentRequest request)
        {
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetRegisteredAgents()
        {
            return Ok(new GetRegisteredAgentsResponse());
        }
    }
}