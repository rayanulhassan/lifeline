using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lifeline.BOL;

namespace lifeline.DAL
{
    public class lifelineDbContext : DbContext
    {
        public lifelineDbContext() : base("lifelinedatabase")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<lifelineDbContext, lifeline.DAL.Migrations.Configuration>());
            Configuration.ProxyCreationEnabled = false;
            
        }

        public DbSet<Members> members { set; get; }
        public DbSet<Style_Accessories> styleAccessories { set; get; }
        public DbSet<Styles> styles { set; get; }
        public DbSet<Food_Items> foodItems { set; get; }
        
     
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<Members>()
                 .HasIndex(u => u.email)
                 .IsUnique();

        }
    }
}
