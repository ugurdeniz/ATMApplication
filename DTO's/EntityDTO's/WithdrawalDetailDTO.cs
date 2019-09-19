using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_s.EntityDTO_s
{
    public class WithdrawalDetailDTO
    {
        public Guid WithdrawalId { get; set; }
        public byte MoneyType { get; set; } 
        public byte Count { get; set; }
    }
}
