using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NoPressure.DAL.Entities.Configuration
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasMany(t => t.Tags).WithOne(a => a.Team);

            builder.HasMany(t => t.Users).WithMany(u => u.Teams);

            builder.HasOne(t => t.Author).WithMany(u => u.CreatedTeams).HasForeignKey(t => t.AuthorId);

            builder.OwnsMany(t => t.Settings);
        }
    }
}
