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

        List<int> _banknotes;
        List<short> _noteCounter;

        public WithdrawalCustomerWithdrawalDetailService(IUnitOfWork uow)
        {
            _uow = uow;
            _wr = uow.GetRepository<Withdrawal>();
            _cr = uow.GetRepository<Customer>();
            _wdr = uow.GetRepository<WithdrawalDetail>();
            _amr = uow.GetRepository<ATMMachine>();
            _banknotes = new List<int>();
            _noteCounter = new List<short>();
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

        public static void makeChangeLimitedCoins(ref List<int> _banknotes, ref List<short> _notecounter, int withdrawQuantity)
        {
            int[] C = new int[withdrawQuantity + 1];
            C[0] = 0;

            int len = _banknotes.Count;
            int[,] track = new int[withdrawQuantity + 1, len];

            for (int i = 0; i < len; i++)
            {
                track[0, i] = _notecounter[i];
            }
            int[] denom = new int[withdrawQuantity + 1];
            for (int j = 1; j <= withdrawQuantity; j++)
            {
                C[j] = int.MaxValue;
                for (int k = 0; k < len; k++)
                {
                    if (j >= _banknotes[k] && (C[j - _banknotes[k]] < int.MaxValue) && (track[j - _banknotes[k], k] > 0))
                    {
                        //C[j] = C[j] > 1+C[j-_banknotes[k]] ? 1+C[j-_banknotes[k]] : C[j];
                        if ((C[j] > 1 + C[j - _banknotes[k]]))
                        {
                            C[j] = 1 + C[j - _banknotes[k]];
                            denom[j] = _banknotes[k];
                            track[j, k] = track[j - _banknotes[k], k] - 1;
                        }
                        else
                        {
                            track[j, k] = track[j - _banknotes[k], k];
                        }
                    }
                    else if (j < _banknotes[k])
                    {
                        track[j, k] = track[0, k];
                    }
                }
            }

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

            #region Veritabanındaki banknot tipi ve sayıları daha sonra işlem yapmak üzere aktarılıyor
            if (atmMachine.BirTL > 0)
            {
                _banknotes.Add(1);
                _noteCounter.Add(atmMachine.BirTL);
            }

            if (atmMachine.BesTL > 0)
            {
                _banknotes.Add(5);
                _noteCounter.Add(atmMachine.BesTL);
            }

            if (atmMachine.OnTL > 0)
            {
                _banknotes.Add(10);
                _noteCounter.Add(atmMachine.OnTL);
            }

            if (atmMachine.YirmiTL > 0)
            {
                _banknotes.Add(20);
                _noteCounter.Add(atmMachine.YirmiTL);
            }

            if (atmMachine.ElliTL > 0)
            {
                _banknotes.Add(50);
                _noteCounter.Add(atmMachine.ElliTL);
            }

            if (atmMachine.YuzTL > 0)
            {
                _banknotes.Add(100);
                _noteCounter.Add(atmMachine.YuzTL);
            }

            if (atmMachine.IkiyuzTL > 0)
            {
                _banknotes.Add(200);
                _noteCounter.Add(atmMachine.IkiyuzTL);
            }
            #endregion

            //Hata kontrolleri
            if (atmMachine.TotalMoney < withdrawQuantity)
            {
                return new ServiceResult(ProcessStateEnum.Warning, "Hata! ATM'de " + withdrawQuantity + "TL'den daha az para bulunmaktadır.");
            }
            else
            {
                makeChangeLimitedCoins(ref _banknotes, ref _noteCounter, withdrawQuantity);

                //Çekilen para bilgisinin veritabanına kayıt işlemi
                try
                {
                    _uow.BeginTran();

                    Withdrawal withdrawal = new Withdrawal();
                    withdrawal.CustomerId = customerDTO.Id;
                    withdrawal.Time = DateTime.Now;
                    withdrawal.Total = withdrawQuantity;
                    _wr.Add(withdrawal);

                    for (int i = 0; i < _banknotes.Count; i++)
                    {
                        if (_noteCounter[i] != 0) //Verilecek banknot varsa veritabanına kaydeder
                        {
                            WithdrawalDetail withdrawalDetail = new WithdrawalDetail()
                            {
                                Count = (byte)(_noteCounter[i]),
                                MoneyType = (byte)_banknotes[i],
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
