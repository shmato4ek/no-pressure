using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NoPressure.DAL.Entities.Configuration
{
    public class SubscriptionsConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasOne(s => s.Follower).WithMany(u => u.Followers).HasForeignKey(s => s.FollowerId);
            builder.HasOne(s => s.Following).WithMany(u => u.Followings).HasForeignKey(s => s.FollowingId);
        }
    }
}
