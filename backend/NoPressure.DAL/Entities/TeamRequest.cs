using NoPressure.Common.Enums;

namespace NoPressure.DAL.Entities
{
    public class TeamRequest
    {
        public int Id { get; set; }
        public TeamRequestStatus Status { get; set; }
        public DateTime Date { get; set; }

        public int AuthorId { get; set; }
        public int InvitedUserId { get; set; }
        public int TeamId { get; set; }
    }
}
