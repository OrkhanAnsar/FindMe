using ApplicationCore.Models;
using ApplicationCore.ServiceImplementations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class FindMeDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Lost> Losts { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<InstitutionType> InstitutionTypes { get; set; }

        public FindMeDbContext(DbContextOptions<FindMeDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(new City[] {
                new City{ Id = 1, Name = "Baku"}
            });
            modelBuilder.Entity<InstitutionType>().HasData(new InstitutionType[] {
                new InstitutionType { Id = 1, Type = "Medical"},
                new InstitutionType { Id = 2, Type = "Police"}
            });
            modelBuilder.Entity<Institution>().HasData(new Institution[] {
                new Institution{
                    Id = 1,
                    CityId = 1,
                    InstitutionTypeId = 1,
                    IsAdmin = true,
                    Address = "Underground",
                    Name = "admin",
                    Password = Cryptor.EncryptPassword("admin"),
                    Website = "www.admin.com",
                    Phone = "+994-55-148-82-28",
                    Login = "admin",
                    OpeningHours = "24/7",
                    Latitude = 40.409264,
                    Longitude = 49.867092
                }
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}