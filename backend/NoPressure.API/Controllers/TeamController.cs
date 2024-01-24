using Microsoft.AspNetCore.Mvc;
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
            var request = Request.Headers["auth-token"].ToString();
            var token = request[10..(request.Length-2)];
            var userId = _jwtFactory.GetValueFromToken(token);

            return Ok(await _teamService.GetTeams(userId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTeamById(string id)
        {
            return Ok(await _teamService.GetTeamByUniqId(id));
        }

        [HttpPost]
        public async Task<ActionResult> CreateTeam(NewTeam newTeam)
        {
            await _teamService.CreateTeam(newTeam);
            return Ok();
        }

        [HttpPost("invitation")]
        public async Task<ActionResult> InviteUserToTeam(NewTeamRequest newRequest)
        {
            var request = Request.Headers["auth-token"].ToString();
            var token = request[10..(request.Length-2)];
            var userId = _jwtFactory.GetValueFromToken(token);

            await _teamRequestService.CreateTeamRequest(newRequest, userId);
            return Ok();
        }

        [HttpPut("invitation")]
        public async Task<ActionResult> UpdateRequestStatus(UpdateTeamRequest request)
        {
            await _teamRequestService.ChangeRequestStatus(request.Id, request.Status);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveTeam(int id)
        {
            await _teamService.RemoveTeam(id);

            return NoContent();
        }
    }
}
