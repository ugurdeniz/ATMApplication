using Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityMapping
{
    public class WithdrawalDetailMapping:EntityTypeConfiguration<WithdrawalDetail>
    {
        public WithdrawalDetailMapping()
        {
            Property(wd => wd.Count)
                .HasColumnType("tinyint")
                .HasColumnName("Miktar");

            Property(wd => wd.MoneyType)
                .HasColumnType("tinyint")
                .HasColumnName("ParaTipi");

            HasRequired(wd => wd.Withdrawal)
                .WithMany(w => w.WithdrawalDetails);
        
        }
    }
}
