using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Canli
{
    public class ProjeCanliDbContext : ATMDbContext
    {
#if DEBUG
        public ProjeCanliDbContext()
        {
            throw new Exception("Debug Modda Canlı Dal Kullanılamaz!");
        }
#else
         public ProjeCanliDbContext(): base("server=.;database=ATMApplicationDBCANLI;User ID=sa;password=159357;MultipleActiveResultSets=True")
        {
            Helper.BuildMode.IsTest = false;
        }
#endif

    }
}
