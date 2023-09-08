using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NoPressure.DAL.Entities.Configuration
{
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).IsRequired();
            builder.HasOne<User>().WithMany(c => c.Activities).HasForeignKey(a => a.UserId);
            builder.HasOne(c => c.Tag).WithMany(c => c.Activities).HasForeignKey(a => a.TagId);
        }
    }
}
