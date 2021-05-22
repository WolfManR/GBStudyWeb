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
        
        /// <summary>
        /// Register agent by it's uri
        /// </summary>
        /// <param name="request">contains uri to agent</param>
        /// <response code="200">successfully registered</response>
        /// <response code="409">agent already registered</response>
        /// <response code="400">can't register agent</response>
        /// <response code="500">something bad happen on server</response>
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] RegisterAgentRequest request)
        {
            try
            {
                _repository.Create(Mapper.Map<AgentInfo>(request));
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
                return Conflict("Agent already registered");
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

        /// <summary>
        /// Enable agent by it's Id
        /// </summary>
        /// <param name="agentId">identifier of agent</param>
        /// <response code="200">agent enabled</response>
        /// <response code="400">agent not exist</response>
        /// <response code="500">something bad happen on server</response>
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
                return BadRequest("Agent not exist");
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

        /// <summary>
        /// Disable agent by it's Id
        /// </summary>
        /// <param name="agentId">identifier of agent</param>
        /// <response code="200">agent disabled</response>
        /// <response code="400">agent not exist</response>
        /// <response code="500">something bad happen on server</response>
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
                return BadRequest("Agent not exist");
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

        /// <summary>
        /// Get registered agents
        /// </summary>
        /// <returns>List of agent's</returns>
        [HttpGet]
        public IActionResult GetRegisteredAgents()
        {
            var agents = _repository.Get();

            return Ok(new GetRegisteredAgentsResponse()
            {
                Agents = agents.Select(Mapper.Map<GetAgentResponse>)
            });
        }
    }
}