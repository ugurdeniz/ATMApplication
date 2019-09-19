using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_s.EntityDTO_s
{
    public class WithdrawalDTO
    {
        public Guid Id { get; set; }
        public short Total { get; set; }
        public DateTime Time { get; set; }
        public bool IsDelete { get; set; }
        public int CustomerId { get; set; }
    }
}
