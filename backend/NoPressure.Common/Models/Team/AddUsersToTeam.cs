using System.Drawing;
using NoPressure.Common.DTO;

namespace NoPressure.Common.Models.Team
{
    public class AddUsersToTeam
    {
        public int TeamId { get; set; }
        public List<string> Users { get; set; }
    }
}