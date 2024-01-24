using System.Drawing;
using NoPressure.Common.DTO;
using NoPressure.Common.Enums;

namespace NoPressure.Common.Models.Team
{
    public class UpdateTeamRequest
    {
        public int Id { get; set; }
        public TeamRequestStatus Status { get; set; }
    }
}