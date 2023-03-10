using Microsoft.EntityFrameworkCore;
using ProjectOverdrive.API.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProjectOverdrive.API.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) 
        {
        }

        public DbSet<Company> Company { get; set; }
        public DbSet<People> People { get; set; }
        public DbSet<Address> Address { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().Property(
                c => c.Status).HasConversion<string>();

            modelBuilder.Entity<People>().Property(
                p => p.Status).HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }
}
}
