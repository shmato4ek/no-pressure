namespace NoPressure.Common.Models.Requests
{
    public class RemoveUserFromTeam
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }
    }
}