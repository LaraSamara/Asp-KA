using EFTaskOne.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTaskOne.Context
{
    internal class ApplicationDbContext :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = .; Database = EFTaskOne; Trusted_Connection = True; TrustServerCertificate = True");
        }
        public DbSet<Product> Products { get; set; }
    }
}
