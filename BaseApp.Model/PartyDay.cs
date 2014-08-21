using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApp.Model
{
    public class PartyDay
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Agolpaito> Agolpaitos { get; set; }

        public PartyDay()
        {
            this.Agolpaitos = new List<Agolpaito>();
            this.Events = new List<Event>();
        }

    }
}
