using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NoPressure.DAL.Entities.Configuration
{
    public class ScheduleTimeConfiguration : IEntityTypeConfiguration<ScheduleHour>
    {
        public void Configure(EntityTypeBuilder<ScheduleHour> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasOne<Schedule>().WithMany(c => c.Time).HasForeignKey(c => c.ScheduleId);
        }
    }
}
