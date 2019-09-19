using Bll.Abstract.Base;
using Common.Results;
using DTO_s.EntityDTO_s;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BllAbstract.EntityType
{
    public interface ICustomerService:IService<Customer>
    {
        ServiceResult<IEnumerable<CustomerDTO>> GetCustomers();
        ServiceResult AddCustomer(CustomerDTO customerDTO);
        ServiceResult GetCustomerByPassword(string password);

    }
}
