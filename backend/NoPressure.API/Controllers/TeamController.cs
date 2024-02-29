using Microsoft.AspNetCore.Mvc;
using NoPressure.BLL.Exceptions;
using NoPressure.BLL.JWT;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.Models.Activity;
using NoPressure.Common.Models.Requests;
using NoPressure.Common.Models.Schedule;
using NoPressure.Common.Models.Team;

namespace NoPressure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly ITeamRequestService _teamRequestService;
        private readonly JwtFactory _jwtFactory;
        public TeamController(ITeamService teamService, ITeamRequestService teamRequestService, JwtFactory jwtFactory)
        {
            _teamService = teamService;
            _teamRequestService = teamRequestService;
            _jwtFactory = jwtFactory;
        }

        [HttpGet("user")]
        public async Task<ActionResult> GetUsersTeams()
        {
            try
            {
                var request = Request.Headers["auth-token"].ToString();
                var token = request[10..(request.Length-2)];
                var userId = _jwtFactory.GetValueFromToken(token);

                return Ok(await _teamService.GetTeams(userId));
            }

            catch (NotAuthorizedException ex)
            {
                return StatusCode(401, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTeamById(string id)
        {
            try
            {
                var request = Request.Headers["auth-token"].ToString();
                var token = request[10..(request.Length-2)];
                var userId = _jwtFactory.GetValueFromToken(token);

                return Ok(await _teamService.GetTeamByUniqId(id, userId));
            }

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }

            catch (NotAuthorizedException ex)
            {
                return StatusCode(401, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateTeam(NewTeam newTeam)
        {
            try
            {
                await _teamService.CreateTeam(newTeam);
                return Ok();
            }

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }

            catch (NotAuthorizedException ex)
            {
                return StatusCode(401, ex.Message);
            }
        }

        [HttpPost("invitation")]
        public async Task<ActionResult> InviteUsersToTeam(AddUsersToTeam users)
        {
            try
            {
                var request = Request.Headers["auth-token"].ToString();
                var token = request[10..(request.Length-2)];
                var userId = _jwtFactory.GetValueFromToken(token);

                await _teamRequestService.AddUsersToTeam(users, userId);

                return Ok();
            }

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }

            catch (NotAuthorizedException ex)
            {
                return StatusCode(401, ex.Message);
            }

            catch (UserInTeamException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("invitation")]
        public async Task<ActionResult> UpdateRequestStatus(UpdateTeamRequest request)
        {
            try
            {
                await _teamRequestService.ChangeRequestStatus(request.Id, request.Status);
                return Ok();
            }
            
            catch (NotAuthorizedException ex)
            {
                return StatusCode(401, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveTeam(int id)
        {
            try
            {
                var request = Request.Headers["auth-token"].ToString();
                var token = request[10..(request.Length-2)];
                var userId = _jwtFactory.GetValueFromToken(token);

                await _teamService.RemoveTeam(id, userId);
            }

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }

            catch (NotAuthorizedException ex)
            {
                return StatusCode(401, ex.Message);
            }

            catch (NoAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("user")]
        public async Task<ActionResult> RemoveUserFromTeam(RemoveUserFromTeam request)
        {
            try
            {
                await _teamService.RemoveUserFromTeam(request.TeamId, request.UserId);
                return NoContent();
            }

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }            
        }

        [HttpGet("settings/{teamId}")]
        public async Task<ActionResult> GetTeamSettings(int teamId)
        {
            try
            {
                return Ok(await _teamService.GetSettings(teamId));
            }
            
            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }

            catch (NotAuthorizedException ex)
            {
                return StatusCode(401, ex.Message);
            }
        }

        [HttpPut("settings")]
        public async Task<ActionResult> UpdateSettings(UpdateTeamSettings settings)
        {
            try
            {
                await _teamService.UpdateTeamSettings(settings);
                return Ok();
            }

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }
    }
}
