using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebProducts.Models
{
    public class ProductDBContext:DbContext
    {
        public ProductDBContext() : base("KeyDB") { }

        public DbSet<Product> Products { get; set; }
    }
}