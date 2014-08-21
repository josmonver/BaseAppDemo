using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApp.Model
{
    public class Bill
    {
        public int AgolpaitoId { get; set; }
        public decimal Price { get; set; }
        //public decimal Paid { get; set; } // = Sum Paiments. Paiment have a Date property so we can get amount paid by year

        public virtual Agolpaito Agolpaito { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }

        public Bill()
        {
            this.Payments = new List<Payment>();
        }
    }
}
