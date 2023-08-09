using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NoPressure.DAL.Entities.Configuration
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasOne<User>().WithMany(c => c.Schedules).HasForeignKey(c => c.UserId);
        }
    }
}
