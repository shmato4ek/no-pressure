using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NoPressure.DAL.Entities.Configuration
{
    public class SubscriptionsConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasOne<User>().WithMany(u => u.Followers).HasForeignKey(s => s.FollowerId);
            builder.HasOne<User>().WithMany(u => u.Following).HasForeignKey(s => s.FollowingId);
        }
    }
}
