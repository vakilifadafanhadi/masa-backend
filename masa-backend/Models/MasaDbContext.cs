using Microsoft.EntityFrameworkCore;
using masa_backend.Models;
using File = masa_backend.Models.File;

namespace masa_backend
{
    public class MasaDbContext : DbContext
    {
        public DbSet<City>? Cities { get; set; }
        public DbSet<Country>? Countries { get; set; }
        public DbSet<PersonalInformation>? PersonalInformations { get; set; }
        public DbSet<Province>? Provinces { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<Wallet>? Wallets { get; set; }
        public DbSet<WalletHistory>? WalletHistories { get; set; }
        public DbSet<File>? Files { get; set; }
        public MasaDbContext(DbContextOptions<MasaDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(city =>
            {
                city.HasOne(city => city.Country)
                .WithMany(country => country.Citys)
                .HasForeignKey(city => city.CountryId)
                .HasPrincipalKey(country => country.Id);
                city.HasOne(city => city.Province)
                .WithMany(province => province.Citys)
                .HasForeignKey(city => city.ProvinceId)
                .HasPrincipalKey(province => province.Id);
                city.HasMany(city => city.PersonalInformation)
                .WithOne(person => person.City)
                .HasForeignKey(person => person.CityId)
                .HasPrincipalKey(city => city.Id);
            });
            modelBuilder.Entity<Country>(country =>
            {
                country.HasMany(country => country.Provinces)
                .WithOne(province => province.Country)
                .HasForeignKey(province => province.CountryId)
                .HasPrincipalKey(country => country.Id);
                country.HasMany(country => country.Citys)
                .WithOne(city => city.Country)
                .HasForeignKey(city => city.CountryId)
                .HasPrincipalKey(country => country.Id);
                country.HasMany(country => country.PersonalInformation)
                .WithOne(person => person.Country)
                .HasForeignKey(person => person.CountryId)
                .HasPrincipalKey(country => country.Id);
            });
            modelBuilder.Entity<PersonalInformation>(person =>
            {
                person.HasIndex(person => person.NationalCode)
                .IsUnique();
                person.HasOne(person => person.User)
                .WithOne(user => user.PersonalInformation)
                .HasForeignKey<PersonalInformation>(person => person.UserId)
                .HasPrincipalKey<User>(user => user.Id);
                person.HasOne(person => person.Wallet)
                .WithOne(wallet => wallet.PersonalInformation)
                .HasForeignKey<PersonalInformation>(person => person.WalletId)
                .HasPrincipalKey<Wallet>(wallet=>wallet.Id);
                person.HasOne(person => person.City)
                .WithMany(city => city.PersonalInformation)
                .HasForeignKey(person => person.CityId)
                .HasPrincipalKey(city => city.Id);
                person.HasOne(person => person.Country)
                .WithMany(country => country.PersonalInformation)
                .HasForeignKey(person => person.CountryId)
                .HasPrincipalKey(country => country.Id);
                person.HasOne(person => person.Province)
                .WithMany(province => province.PersonalInformation)
                .HasForeignKey(person => person.ProvinceId)
                .HasPrincipalKey(province => province.Id);
            });
            modelBuilder.Entity<Province>(province => {
                province.HasMany(province => province.Citys)
                .WithOne(city => city.Province)
                .HasForeignKey(city => city.ProvinceId)
                .HasPrincipalKey(province => province.Id);
                province.HasMany(province => province.PersonalInformation)
                .WithOne(person => person.Province)
                .HasForeignKey(person => person.ProvinceId)
                .HasPrincipalKey(province => province.Id);
            });
            modelBuilder.Entity<User>(user =>
            {
                user.HasOne(user => user.PersonalInformation)
                .WithOne(person => person.User)
                .HasForeignKey<User>(user => user.PersonId)
                .HasPrincipalKey<PersonalInformation>(person => person.Id);
                user.HasMany(user => user.Files)
                .WithOne(file => file.User)
                .HasForeignKey(file => file.UserId)
                .HasPrincipalKey(user => user.Id);
            });
            modelBuilder.Entity<Wallet>(wallet =>
            {
                wallet.HasOne(wallet => wallet.PersonalInformation)
                .WithOne(person => person.Wallet)
                .HasForeignKey<Wallet>(wallet => wallet.PersonId)
                .HasPrincipalKey<PersonalInformation>(person => person.Id);
                wallet.HasMany(wallet => wallet.WalletHistories)
                .WithOne(walletHistory => walletHistory.Wallet)
                .HasForeignKey(walletHistory=>walletHistory.WalletId)
                .HasPrincipalKey(wallet=>wallet.Id);
            });
            modelBuilder.Entity<WalletHistory>(walletHistory =>
            {
                walletHistory.HasOne(walletHistory => walletHistory.Wallet)
                .WithMany(wallet => wallet.WalletHistories)
                .HasForeignKey(walletHistory=>walletHistory.WalletId)
                .HasPrincipalKey(wallet=>wallet.Id);
            });
            modelBuilder.Entity<File>(file =>
            {
                file.HasOne(file => file.User)
                .WithMany(user => user.Files)
                .HasForeignKey(file => file.UserId)
                .HasPrincipalKey(user => user.Id);
            });
        }
    }
}
