using NoPressure.Common.Enums;

namespace NoPressure.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime RegistrationDate { get; set; }
        public AuthType AuthType { get; set; }
        public string? ExternalToken { get; set; }
        public List<Activity> Activities { get; set; }
        public List<Plan> Plans { get; set; }
        public List<Subscription> Followers { get; set; }
        public List<Subscription> Followings { get; set; }
        public List<Team> CreatedTeams { get; set; }
        public List<Team>? Teams { get; set; }
    }
}
