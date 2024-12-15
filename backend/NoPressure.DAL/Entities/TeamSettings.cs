using NoPressure.Common.Enums;

namespace NoPressure.DAL.Entities
{
    public class TeamSettings
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public TeamAccess AddingUsers { get; set; }
        public TeamAccess AddingActivities { get; set; }
    }
}