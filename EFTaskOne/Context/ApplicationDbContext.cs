<<<<<<< HEAD
﻿using Microsoft.EntityFrameworkCore;
using EFTaskOne.Models;
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
            optionsBuilder.UseSqlServer("Server = .; Database = EF-TaskOne; Trusted_Connection = True; TrustServerCertificate = True");
        }
        public DbSet<Product>Products { get; set; }
    }
}
=======
﻿using Microsoft.EntityFrameworkCore;
using EFTaskOne.Models;
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
            optionsBuilder.UseSqlServer("Server = .; Database = EF-TaskOne; Trusted_Connection = True; TrustServerCertificate = True");
        }
        public DbSet<Product>Products { get; set; }
    }
}
>>>>>>> 70d0ec8 (Upload EF Tasks)
