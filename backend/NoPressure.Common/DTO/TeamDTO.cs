using NoPressure.Common.Enums;
using NoPressure.Common.Models.Tag;
using NoPressure.Common.Models.User;

namespace NoPressure.Common.DTO
{
    public class TeamDTO
    {   
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string UniqId { get; set; }
        public string Color { get; set; }
        public int AuthorId { get; set; }
        public TeamRole Role { get; set; }
        public TeamAccess AddingUsers { get; set; } = TeamAccess.Deny;
        public int? TeamRequestId { get; set; }
        public List<UserInfo>? Users { get; set; }
        public List<TeamTag>? Tags { get; set; }
    }
}