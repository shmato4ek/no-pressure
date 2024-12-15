using System.Drawing;
using NoPressure.Common.DTO;

namespace NoPressure.Common.Models.Team
{
    public class AddUserToTeam
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }
    }
}