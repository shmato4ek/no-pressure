using NoPressure.Common.Enums;
using NoPressure.Common.Models.Tag;
using NoPressure.Common.Models.User;

namespace NoPressure.Common.DTO
{
    public class TeamSettingsDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public TeamAccess AddingUsers { get; set; }
        public TeamAccess AddingActivities { get; set; }
    }
}