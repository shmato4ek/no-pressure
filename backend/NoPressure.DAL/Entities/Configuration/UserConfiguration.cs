using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPressure.DAL.Entities.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.Name).IsRequired();
            builder.Property(u => u.Password).IsRequired();

            builder.HasMany(u => u.Teams).WithMany(t => t.Users);

            builder.HasMany(u => u.CreatedTeams).WithOne(t => t.Author);
        }
    }
}
