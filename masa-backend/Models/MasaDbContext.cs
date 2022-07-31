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
                .WithOne(e => e.User)
                .HasForeignKey<PersonalInformation>(e => e.NationalCode)
                .HasPrincipalKey<User>(e => e.NationalCode);
            modelBuilder.Entity<Country>()
                .HasMany(e => e.Provinces)
                .WithOne(e => e.Country)
                .HasForeignKey(e => e.CountryCode)
                .HasPrincipalKey(e => e.CountryCode);
            modelBuilder.Entity<Province>()
                .HasMany(e => e.Cities)
                .WithOne(e => e.Province)
                .HasForeignKey(e => e.ProvinceCode)
                .HasPrincipalKey(e => e.ProvinceCode);
            modelBuilder.Entity<City>()
                .HasOne(e => e.Province)
                .WithMany(e => e.Cities)
                .HasForeignKey(e => e.ProvinceCode);
        }
    }
}
