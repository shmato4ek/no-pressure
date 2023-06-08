using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPressure.DAL.Context
{
    public class NoPressureDbContext : DbContext
    {
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
            => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=NoPressureDemo;UserName=postgres;Password=0985883147");
    }
}
