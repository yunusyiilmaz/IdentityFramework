using DataAccess.Context.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.Context
{

    public class DbContextFactory : IDesignTimeDbContextFactory<AppIdentityDbContext>
    {
        AppIdentityDbContext IDesignTimeDbContextFactory<AppIdentityDbContext>.CreateDbContext(string[] args)
        {
            string path = Directory.GetCurrentDirectory();

            IConfigurationBuilder builder =
                new ConfigurationBuilder()
                    .SetBasePath(path)
                    .AddJsonFile("appsettings.json");

            IConfigurationRoot config = builder.Build();
            Console.WriteLine($"DesignTimeDbContextFactory: using base path = {path}");
            string connectionString = config.GetConnectionString("DefaultConnectionString");
            
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Could not find connection string named 'Default'");
            }

            
            Console.WriteLine($"DesignTimeDbContextFactory: using connection string = {connectionString}");

            

            DbContextOptionsBuilder<AppIdentityDbContext> dbContextOptionsBuilder =
                new DbContextOptionsBuilder<AppIdentityDbContext>();
            dbContextOptionsBuilder.UseSqlServer(connectionString);

            return new AppIdentityDbContext(dbContextOptionsBuilder.Options);
        }
    }
}

