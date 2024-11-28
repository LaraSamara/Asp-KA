using CRUDWithFeature.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDWithFeature.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
