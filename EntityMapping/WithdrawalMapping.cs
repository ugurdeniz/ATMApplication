using Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EntityMapping
{
    public class WithdrawalMapping:EntityTypeConfiguration<Withdrawal>
    {
        public WithdrawalMapping()
        {
            Property(w => w.Time)
                .HasColumnName("IslemTarihi")
                .IsRequired();

            Property(w => w.Total)
                .HasColumnName("Miktar");

            HasMany(w => w.WithdrawalDetails)
                .WithRequired(wd => wd.Withdrawal)
                .WillCascadeOnDelete(false);

        }
    }
}
