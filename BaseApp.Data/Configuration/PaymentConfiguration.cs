using BaseApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApp.Data.Configuration
{
    public class PaymentConfiguration : EntityTypeConfiguration<Payment>
    {
        public PaymentConfiguration()
        {
            ToTable("Payments");

            HasRequired<Bill>(e => e.Bill)
                .WithMany(e => e.Payments).HasForeignKey(e => e.BillId);
        }
    }
}
