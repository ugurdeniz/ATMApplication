using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Customer:IEntity
    {
        public Customer()
        {
            Withdrawals = new HashSet<Withdrawal>();
        }
        public int Id { get; set; }
        [Column("Sifre"),StringLength(4)]
        public string Password { get; set; }
        [Column("ToplamPara",TypeName ="smallint")]
        public short TotalMoney { get; set; }

        public virtual ICollection<Withdrawal> Withdrawals { get; set; }
    }
}
