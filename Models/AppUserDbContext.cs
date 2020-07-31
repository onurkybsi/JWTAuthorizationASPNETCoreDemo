using Microsoft.EntityFrameworkCore;

namespace JWTAuthorizationASPNETCoreDemo.Models
{
    public class AppUserDbContext : DbContext
    {
        public AppUserDbContext()
        { }

        public AppUserDbContext(DbContextOptions<AppUserDbContext> options)
            : base(options)
        { }

        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=Mysqlparola123;database=AppUserDb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.Property(e => e.Id)
                        .ValueGeneratedOnAdd();

                entity.HasKey(e => e.Id);
            });
        }
    }
}