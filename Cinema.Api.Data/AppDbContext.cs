using Cinema.Api.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=AppDbConnectionString")
        {
            //Disable initializer
            Database.SetInitializer<AppDbContext>(null);

            //Uncomment this line to disable lazy loading
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        // tables 
        public DbSet<Movies> Movies { get; set; }
        public DbSet<MoviesType> MoviesType { get; set; }
        public DbSet<Salon> Salon { get; set; }
        public DbSet<Seance> Seance { get; set; }
    }
}
