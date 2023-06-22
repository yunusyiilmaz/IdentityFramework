using Microsoft.EntityFrameworkCore;
using Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Context.Concrete
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {

        }
        private string PassWordHash(AppUser user,string Pass)
        {
            var passWordHash = new PasswordHasher<AppUser>();
            return passWordHash.HashPassword(user, Pass);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<IdentityUserToken<Guid>>().HasNoKey();
            //modelBuilder.Entity<IdentityUserRole<Guid>>().HasNoKey();
            //modelBuilder.Entity<IdentityUserLogin<Guid>>().HasNoKey();
            var user = new AppUser()
            {
                Id=Guid.NewGuid(),
                UserName="admin",
                NormalizedUserName="ADMIN",
                Email="admin@gmail.com",
                NormalizedEmail="ADMIN@GMAIL.COM",
                EmailConfirmed=false,
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumber="1111111111",
                PhoneNumberConfirmed=false,
                TwoFactorEnabled=false,
                LockoutEnd=DateTimeOffset.UtcNow,
                LockoutEnabled=false,
            };
            user.PasswordHash = PassWordHash(user,"1234");
            modelBuilder.Entity<AppUser>().HasData(user);
            base.OnModelCreating(modelBuilder);
        }
    }
}
