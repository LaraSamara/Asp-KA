using Identity_Jwt.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity_Jwt.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Departments>()
                .HasIndex(D => D.Name).IsUnique();
        }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Departments> Departments { get; set; }
    }

}
 