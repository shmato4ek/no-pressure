using NoPressure.Common.Enums;

namespace NoPressure.DAL.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string UniqId { get; set; }
        public EntityState State { get; set; }
        public TeamPrivacyState PrivacyState { get; set; }
        public string Color { get; set; }

        public int AuthorId { get; set; }
        public User Author { get; set; }
        public List<User> Users { get; set; }
        public List<Tag> Tags { get; set; }
        public List<TeamSettings> Settings { get; set; }
    }
}
