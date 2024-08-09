using EFTaskTwo_CRUD.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTaskTwo_CRUD.Configurations
{
    internal class ProductConfigurations :IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(nameof(Product.Name)).HasColumnType("varchar(50)");
        }
    }
}
