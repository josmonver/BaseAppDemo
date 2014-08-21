using BaseApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApp.Data.Configuration
{
    public class AgolpaitoConfiguration : EntityTypeConfiguration<Agolpaito>
    {
        public AgolpaitoConfiguration()
        {
            ToTable("Agolpaitos");
            // Agolpaito has one bill. One Bill belongs to one Agolpaito
            HasRequired(e => e.Bill)
                .WithRequiredPrincipal(e => e.Agolpaito);

            // One Agolpaitos drinks N Drink, One Drink can be had by N Agolpaitos
            HasMany(e => e.Drinks)
                .WithMany(e => e.Agolpaitos)
                .Map(m =>
                {
                    m.ToTable("AgolpaitosDrinks");
                    m.MapLeftKey("AgolpaitoId");
                    m.MapRightKey("DrinkId");
                });

            // One Agolpaitos goes to N PartyDays, To One PartyDay can go N Agolpaitos
            HasMany(e => e.PartyDays)
                .WithMany(e => e.Agolpaitos)
                .Map(m =>
                {
                    m.ToTable("Attendance");
                    m.MapLeftKey("AgolpaitoId");
                    m.MapRightKey("PartyDayId");
                });
        }
    }
}
