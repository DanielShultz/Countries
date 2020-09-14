using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Countries.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DbConnectionString") { }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
    }
}