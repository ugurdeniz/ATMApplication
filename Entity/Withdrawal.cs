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
    public class Withdrawal :IEntity
    {
        public Withdrawal()
        {
            WithdrawalDetails = new HashSet<WithdrawalDetail>();
        }
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName ="smallint")]
        public short Total { get; set; }
        public DateTime Time { get; set; }
        
       
        public int CustomerId { get; set; }

        
        public virtual Customer Customer { get; set; }
        public virtual ICollection<WithdrawalDetail> WithdrawalDetails { get; set; }
    }
}
