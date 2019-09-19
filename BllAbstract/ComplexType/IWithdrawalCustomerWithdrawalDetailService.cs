using Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BllAbstract.ComplexType
{
    public interface IWithdrawalCustomerWithdrawalDetailService
    {   
        ServiceResult WithdrawMoneyLeastBanknot(string password, int machineId, short withdrawQuantity);
        ServiceResult WithdrawMoneyApproximateBanknot(string password, int machineId, short withdrawQuantity);
    }

}
