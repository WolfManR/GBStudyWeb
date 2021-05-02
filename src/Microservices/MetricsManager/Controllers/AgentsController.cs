using System;
using System.Linq;
using Common;
using MetricsManager.Controllers.Requests;
using MetricsManager.Controllers.Responses;
using MetricsManager.DataBase.Interfaces;
using MetricsManager.DataBase.Models;
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
            var agent = new AgentInfo() {Uri = request.Uri.AbsoluteUri};
            try
            {
                _repository.Create(agent);
                _logger.LogInformation(LogEvents.EntityCreationSuccess, "Agent successfully added to database");
                return Ok();
            }
            catch (ArgumentException e)
            {
                _logger.LogWarning(
                    LogEvents.EntityCreationFailure,
                    e,
                    "Failure to add entity: {Uri} to database, entity already exist",
                    e.Data["uri"]);
                return Problem("Agent already registered");
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(
                    LogEvents.EntityCreationFailure,
                    e,
                    "Failure to add entity: {Uri} to database",
                    e.Data["uri"]);
                return BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError(
                    LogEvents.EntityCreationFailure,
                    e,
                    "Not handled exception on registering agent: {Uri} in database",
                    e.Data["uri"]);
                return StatusCode(500);
            }
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            try
            {
                var agent = _repository.GetById(agentId);
                if (agent is null)
                {
                    return NotFound();
                }
                _repository.Update(agent with {IsEnabled = true});
                return Ok();
            }
            catch (ArgumentException e)
            {
                _logger.LogWarning(
                    LogEvents.EntityCreationFailure,
                    "Failure to update entity: {Id} {Uri} in database, entity not exist",
                    e.Data["id"],
                    e.Data["uri"]);
                return Problem("Agent not exist");
            }
            catch (Exception e)
            {
                _logger.LogError(
                    LogEvents.EntityCreationFailure,
                    "Failure to update entity: {Id} {Uri} in database",
                    e.Data["id"],
                    e.Data["uri"]);
                return StatusCode(500);
            }
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            try
            {
                var agent = _repository.GetById(agentId);
                if (agent is null)
                {
                    return NotFound();
                }
                _repository.Update(agent with {IsEnabled = false});
                return Ok();
            }
            catch (ArgumentException e)
            {
                _logger.LogWarning(
                    LogEvents.EntityCreationFailure,
                    "Failure to update entity: {Id} {Uri} in database, entity not exist",
                    e.Data["id"],
                    e.Data["uri"]);
                return Problem("Agent not exist");
            }
            catch (Exception e)
            {
                _logger.LogError(
                    LogEvents.EntityCreationFailure,
                    "Failure to update entity: {Id} {Uri} in database",
                    e.Data["id"],
                    e.Data["uri"]);
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult GetRegisteredAgents()
        {
            var agents = _repository.Get();
            if (agents is null)
            {
                return NotFound();
            }

            return Ok(new GetRegisteredAgentsResponse()
            {
                Agents = agents
                    .Select(info =>
                        new GetAgentResponse()
                        {
                            Id = info.Id,
                            Uri = info.Uri,
                            IsEnabled = info.IsEnabled
                        }
                    )
                    .ToList()
            });
        }
    }
}