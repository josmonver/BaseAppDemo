using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApp.Model
{
    public class Agolpaito : Person
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        
        public virtual Bill Bill { get; set; }
        public virtual ICollection<Drink> Drinks { get; set; }
        public virtual ICollection<PartyDay> PartyDays { get; set; }

        public Agolpaito()
        {
            this.Drinks = new List<Drink>();
            this.PartyDays = new List<PartyDay>();
        }
    }
}
