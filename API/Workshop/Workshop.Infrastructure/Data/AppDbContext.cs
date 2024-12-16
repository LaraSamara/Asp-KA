using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Workshop.Core.Models;

namespace Workshop.Infrastructure.Data
{
    public class AppDbContext:IdentityDbContext<Users, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ItemsUnits>()
                .HasKey(IU => new { IU.Units_Id, IU.Items_Id });

            builder.Entity<CustomersStores>()
                .HasKey(CS => new { CS.Customer_Id, CS.Store_Id });

            builder.Entity<InvItemsStores>()
                .HasKey(IS => new {IS.Items_Id, IS.Stores_Id });

            builder.Entity<ShoppingCartItems>()
                .HasKey(SCI => new {SCI.Customer_Id, SCI.Item_Id, SCI.Store_Id});

            builder.Entity<InvoicesDetails>()
                .HasKey(ID => new {ID.Invoice_Id, ID.Item_Id});
        }
        public DbSet<Users> Users {  get; set; }
        public DbSet<Goverments> Goverments { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Zones> Zones { get; set; }
        public DbSet<MainGroups> MainGroups { get; set; }
        public DbSet<SubGroupsA> SubGroupsA { get; set; }
        public DbSet<SubGroupsB> SubGroupsB { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<Units> Units { get; set; }
        public DbSet<ItemsUnits> ItemsUnits { get; set; }
        public DbSet<Stores> Stores { get; set; }
        public DbSet<CustomersStores> CustomersStores { get; set; }
        public DbSet<InvItemsStores> InvItemsStores { get; set; }
        public DbSet<Classifications> Classifications { get; set; }
        public DbSet<ShoppingCartItems> ShoppingCartItems { get; set;}
        public DbSet<Invoices> Invoices { get; set; }
        public DbSet<InvoicesDetails> InvoicesDetails { get; set; }

    }
}
