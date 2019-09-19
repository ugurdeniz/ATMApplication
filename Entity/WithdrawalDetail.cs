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
    public class WithdrawalDetail:IEntity
    {
        [Key]
        [Column(Order = 0)]
        public int WithdrawalId { get; set; }
        [Key]
        [Column(Order = 1)]
        public byte MoneyType { get; set; } //composite key

        public byte Count { get; set; }

        public virtual Withdrawal Withdrawal { get; set; }
    }
}
