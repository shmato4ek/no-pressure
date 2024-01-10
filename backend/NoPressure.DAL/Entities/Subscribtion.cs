namespace NoPressure.DAL.Entities
{
    public class Subscription
    {
        public int Id { get; set; }
        public int FollowerId { get; set; }
        public int FollowingId { get; set; }
        public DateTime Date { get; set; }

        public User Follower { get; set; }
        public User Following { get; set; }
    }
}