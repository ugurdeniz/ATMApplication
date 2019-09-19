using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_s.EntityDTO_s
{
    public class MachineDTO
    {
        public Guid Id { get; set; }
        public string Location { get; set; }
        public short TotalMoneyOnMachine { get; set; }
    }
}
