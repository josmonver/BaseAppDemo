using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApp.Model
{
    public enum DrinkType
    {
        Alcohol,
        SoftDrink,
        Other
    }

    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DrinkType Type { get; set; }
        public decimal UnitPrice { get; set; }
        public int SaleUnits { get; set; }

        public virtual ICollection<Agolpaito> Agolpaitos { get; set; }

        public Drink()
        {
            this.Agolpaitos = new List<Agolpaito>();
            this.SaleUnits = 1; // Default sale units = 1
        }
    }
}
