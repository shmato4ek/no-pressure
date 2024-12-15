using System.Drawing;
using NoPressure.Common.DTO;
using NoPressure.Common.Enums;

namespace NoPressure.Common.Models.Team
{
    public class NewTeamRequest
    {
        public int InvitedUserId { get; set; }
        public int TeamId { get; set; }
    }
}