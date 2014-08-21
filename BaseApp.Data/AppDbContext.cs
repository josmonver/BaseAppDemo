using BaseApp.Data.Configuration;
using BaseApp.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApp.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext() : base("AppDbContext")
        {

        }

        
        public DbSet<Agolpaito> Agolpaitos { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<PartyDay> PartyDays { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Use singular table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new AgolpaitoConfiguration());
            modelBuilder.Configurations.Add(new BillConfiguration());
            modelBuilder.Configurations.Add(new DrinkConfiguration());
            modelBuilder.Configurations.Add(new PartyDayConfiguration());
            modelBuilder.Configurations.Add(new PaymentConfiguration());
            modelBuilder.Configurations.Add(new EventConfiguration());
            modelBuilder.Configurations.Add(new PartyConfiguration());
        }
    }
}
