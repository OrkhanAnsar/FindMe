using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Database
{
    public class FindMeDbContextFactory : IDesignTimeDbContextFactory<FindMeDbContext>
    {
        public FindMeDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FindMeDbContext>();
            builder.UseSqlServer("Server=tcp:findme.database.windows.net,1433;Initial Catalog=FindMe;Persist Security Info=False;User ID=findme;Password=Ashotscreenshot6321;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            return new FindMeDbContext(builder.Options);
        }
    }
}