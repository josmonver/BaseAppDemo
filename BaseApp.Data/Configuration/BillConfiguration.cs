using BaseApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApp.Data.Configuration
{
    public class BillConfiguration : EntityTypeConfiguration<Bill>
    {
        public BillConfiguration()
        {
            ToTable("Bills");

            HasKey(b => b.AgolpaitoId);
        }
    }
}
