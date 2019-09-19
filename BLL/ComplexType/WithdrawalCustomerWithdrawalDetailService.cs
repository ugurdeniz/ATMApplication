using BllAbstract.ComplexType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Results;
using Core.DAL;
using Entity;
using BLL.EntityType;
using Common.Enums;
using Helper;
using DTO_s.EntityDTO_s;

namespace BLL.ComplexType
{
    public class WithdrawalCustomerWithdrawalDetailService : IWithdrawalCustomerWithdrawalDetailService
    {
        IUnitOfWork _uow;
        IRepository<Withdrawal> _wr;
        IRepository<Customer> _cr;
        IRepository<WithdrawalDetail> _wdr;
        IRepository<ATMMachine> _amr;

        List<byte> _notes;
        short[] _noteCounter;

        public WithdrawalCustomerWithdrawalDetailService(IUnitOfWork uow)
        {
            _uow = uow;
            _wr = uow.GetRepository<Withdrawal>();
            _cr = uow.GetRepository<Customer>();
            _wdr = uow.GetRepository<WithdrawalDetail>();
            _amr = uow.GetRepository<ATMMachine>();
            _notes = new List<byte> { 200, 100, 50, 20, 10, 5 };
            _noteCounter = new short[5];
        }
        /// <summary>
        /// Eşit/Yaklaşık sayıda banknot kullanarak para çekme işlemini gerçekleştiren method
        /// </summary>
        /// <param name="password">Müşteri Şifresi</param>
        /// <param name="machineId">ATM'nin Id numarası</param>
        /// <param name="withdrawQuantity">Çekilecek miktar</param>
        /// <returns></returns>
        public ServiceResult WithdrawMoneyApproximateBanknot(string password, int machineId, short withdrawQuantity)
        {


            throw new NotImplementedException();
        }

        /// <summary>
        /// En az sayıda banknot kullanarak para çekme işlemini gerçekleştiren method
        /// </summary>
        /// <param name="password">Müşteri Şifresi</param>
        /// <param name="machineId">ATM'nin Id numarası</param>
        /// <param name="withdrawQuantity">Çekilecek miktar</param>
        /// <returns></returns>
        public ServiceResult WithdrawMoneyLeastBanknot(string password, int machineId, short withdrawQuantity)
        {
            Customer customer;

            try
            {
                customer = _cr.Get(x => x.Password == password);
                if (customer == null)
                {
                    return new ServiceResult(ProcessStateEnum.Error, "Kullanıcı bulunamadı. Lütfen şifreyi tekrar giriniz");
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(ProcessStateEnum.Error, "Müşteri bilgileri alınırken hata oluştu. Hata:" + ex.Message);
            }

            //Müşteri bilgileri alınıp DTO ile maplendi 
            var customerDTO = Helpers.Mapping<Customer, CustomerDTO>(customer);

            //ATM bilgileri alındı
            var atmMachine = _amr.Get(x => x.Id == machineId);

            //Hata kontrolleri
            if (atmMachine.TotalMoney < withdrawQuantity)
            {
                return new ServiceResult(ProcessStateEnum.Warning, "Hata! ATM'de " + withdrawQuantity + "TL'den daha az para bulunmaktadır.");
            }
            else
            {
                if (withdrawQuantity < 5)
                {
                    return new ServiceResult(ProcessStateEnum.Warning, "Yalnızca 5 ve katları şeklinde çekim yapılabilir");
                }

                for (int i = 0; i < 5; i++)
                {
                    if (withdrawQuantity >= _notes[i])
                    {
                        _noteCounter[i] = (short)(withdrawQuantity / _notes[i]);
                        withdrawQuantity = (short)(withdrawQuantity - _noteCounter[i] * _notes[i]);
                    }
                }

                //Çekilen para bilgisinin veritabanına kayıt işlemi
                try
                {
                    _uow.BeginTran();

                    Withdrawal withdrawal = new Withdrawal();
                    withdrawal.CustomerId = customerDTO.Id;
                    withdrawal.Time = DateTime.Now;
                    withdrawal.Total = withdrawQuantity;
                    _wr.Add(withdrawal);

                    for (int i = 0; i < _notes.Count; i++)
                    {
                        if (_noteCounter[i] != 0) //Verilecek banknot varsa veritabanına kaydeder
                        {
                            WithdrawalDetail withdrawalDetail = new WithdrawalDetail()
                            {
                                Count = (byte)(_noteCounter[i]),
                                MoneyType = _notes[i],
                                WithdrawalId = withdrawal.Id
                            };
                            _wdr.Add(withdrawalDetail);
                        }
                    }
                    _uow.CommitTran();
                }
                catch (Exception ex)
                {
                    _uow.RollBackTran();
                    return new ServiceResult(ProcessStateEnum.Error, "Bir hata nedeniyle kayıt yapılamamıştır. Hata:" + ex.Message);
                }
                finally
                {
                    _uow.Dispose();
                }
            }
            return new ServiceResult(ProcessStateEnum.Warning, "Para çekme işlemi başarı ile gerçekleşmiştir.");

        }

    }
}
