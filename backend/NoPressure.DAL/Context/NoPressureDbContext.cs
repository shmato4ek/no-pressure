using Microsoft.EntityFrameworkCore;
using NoPressure.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPressure.DAL.Context
{
    public class NoPressureDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public NoPressureDbContext()
        {

        }

        public NoPressureDbContext(DbContextOptions<NoPressureDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Configure();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=NoPressureDemo1;UserName=postgres;Password=0985883147");
    }
}
