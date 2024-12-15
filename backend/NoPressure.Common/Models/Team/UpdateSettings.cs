using System.Drawing;
using NoPressure.Common.DTO;
using NoPressure.Common.Enums;

namespace NoPressure.Common.Models.Team
{
    public class UpdateSettings
    {
        public int Id { get; set; }
        public TeamAccess AddingUsers { get; set; }
        public TeamAccess AddingActivities { get; set; }
    }
}