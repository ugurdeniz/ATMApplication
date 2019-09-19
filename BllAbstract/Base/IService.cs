using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Abstract.Base
{
    public interface IService<T>
        where T : class, new()
    {
    }
}
