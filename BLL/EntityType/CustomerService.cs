using BLL.Base;
using BllAbstract.EntityType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Results;
using DTO_s.EntityDTO_s;
using Entity;
using Core.DAL;
using Helper;
using Common.Enums;

namespace BLL.EntityType
{
    public class CustomerService : ICustomerService
    {
        IRepository<Customer> _cr;
        IUnitOfWork _uow;

        public CustomerService(IUnitOfWork uow)
        {
            _uow = uow;
            _cr = uow.GetRepository<Customer>();
        }
        public ServiceResult AddCustomer(CustomerDTO customerDTO)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Şifre bilgisine göre müşteri bilgisini getiren method
        /// </summary>
        /// <param name="password">Müşteri Şifresi</param>
        /// <returns></returns>
        public ServiceResult GetCustomerByPassword(string password)
        {
            Customer customer;

            try
            {
                customer = _cr.Get(x => x.Password == password);
            }
            catch (Exception ex)
            {
                return new ServiceResult(ProcessStateEnum.Error, ex.Message);
            }

            if (customer != null)
            {
                var customerDTO = Helpers.Mapping<Customer, CustomerDTO>(customer);
                return new ServiceResult<CustomerDTO>(ProcessStateEnum.Success, "Müşteri bulundu", customerDTO);
            }
            return new ServiceResult(ProcessStateEnum.Warning, "Müşteri bulunamadı");
            
        }

        public ServiceResult<IEnumerable<CustomerDTO>> GetCustomers()
        {
            throw new NotImplementedException();
        }
    }
}
