using BLL.EntityType;
using BllAbstract.EntityType;
using Core.DAL;
using Core.DAL.SqlServer.EntityFramework;
using Dal;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Module
{
    public class BllModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
            Kernel.Bind<ICustomerService>().To<CustomerService>();

            List<INinjectModule> moduleList = new List<INinjectModule>();
#if DEBUG
            moduleList.Add(new DalModule());
#else
            moduleList.Add(new Dal.Canli.DalModule());
#endif
            Kernel.Load(moduleList);
        }
    }
}
