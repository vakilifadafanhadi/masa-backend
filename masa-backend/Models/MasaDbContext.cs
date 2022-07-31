using Microsoft.EntityFrameworkCore;
using masa_backend.Models;

namespace masa_backend
{
    public class MasaDbContext : DbContext
    {
        public DbSet<PersonalInformation> PersonalInformations { get; set; } = default!;
        public DbSet<Province> Provinces { get; set; } = default!;
        public DbSet<City> Cities { get; set; } = default!;
        public DbSet<Country> Countries { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
        public MasaDbContext(DbContextOptions<MasaDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(e => e.PersonalInformation)
                .WithOne(c=>c.User)
                .HasForeignKey<PersonalInformation>(p=>p.NationalCode)
                .HasPrincipalKey<User>(u=>u.NationalCode);
        }
    }
}
