using Microsoft.EntityFrameworkCore;
using Shopon.Common.Models;
using Shopon.Data.Contracts;
using Shopon.EF.Models;

namespace Shopon.EF
{
    public class BankEFRepository : IBankRepository
    {
        private readonly DbShoponContext context;

        public BankEFRepository(DbShoponContext context)
        {
            this.context = context;
        }

        public Bank AddBank(Bank bank)
        {
            try
            {
                // 0. Check if bank exists - IFSC
                var dbBank = context.Banks.FirstOrDefault(x => x.IFSC == bank.IFSC);
                if(dbBank == null)
                {
                    // 1. common.bank -> db.bank
                    dbBank = new Models.DbBank
                    {
                        BankName = bank.BankName,
                        Branch = bank.Branch,
                        City = bank.City,
                        IFSC = bank.IFSC,
                        State = bank.State,
                        Offers = GetDbOffers(bank.Offers),
                    };
                    context.Banks.Add(dbBank);
                    bank.BankId = dbBank.BankId;
                } else
                {
                    context.Entry<Models.DbBank>(dbBank).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                    var dbOffers = GetDbOffers(bank.Offers);
                    foreach (var dbOffer in dbOffers)
                    {
                        dbOffer.BankId = dbBank.BankId;
                        context.Offers.Add(dbOffer);
                    }
                    dbBank.Offers = dbOffers;
                    context.Banks.Update(dbBank);
                    
                    //var dbOffers = GetDbOffers(bank.Offers);
                    //foreach(var dbOffer in dbBank.Offers)
                    //{
                    //    Context.Offers.Add(dbOffer);
                    //}
                }
                context.SaveChanges();

            } catch (Exception ex)
            {

            }
            return bank;
        }

        public IEnumerable<Bank>? GetAllBanks()
        {
            try
            {
                var dbBanks = context.Banks;
                if (dbBanks != null)
                {
                    //var banks = dbBanks
                    //             .Include(x=>x.Offers)
                    //            .Where(x=>x.isDeleted==false)
                    //            .Select(x=>new Bank
                    //            {
                    //                BankId= x.BankId,
                    //                BankName= x.BankName,
                    //                Branch= x.Branch,
                    //                City= x.City,
                    //                IFSC= x.IFSC,
                    //                State= x.State,
                    //            }).ToList();
                    //return banks;

                    var dbBanksData = dbBanks.Where(x => x.IsDeleted == false).ToList();
                    List<Bank> banks = new List<Bank>();
                    foreach (var dbBank in dbBanksData)
                    {
                        //dbBank.Offers = GetEntityOffers(dbBank.Offers);

                        var Bank = new Bank
                        {
                            BankId = dbBank.BankId,
                            BankName = dbBank.BankName,
                            Branch = dbBank.Branch,
                            City = dbBank.City,
                            IFSC = dbBank.IFSC,
                            State = dbBank.State,
                            Offers = GetEntity(dbBank.Offers),
                        };
                        banks.Add(Bank);
                    }
                    return banks;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        private ICollection<Offer> GetEntity(ICollection<DbOffer> dbOffers)
        {
            List<Offer> offers = new List<Offer>();
            foreach (var dbOffer in dbOffers)
            {
                var offer = new Offer
                {
                    Discount = dbOffer.Discount,
                    OfferTime = dbOffer.OfferTime,
                    OfferType = dbOffer.OfferType,
                    Remark = dbOffer.Remark
                };

                offers.Add(offer);
            }
            return offers;
        }

        #region Private Methods
        private ICollection<DbOffer> GetDbOffers(ICollection<Offer> offers)
        {
            List<DbOffer> dbOffers = new List<DbOffer>();
            foreach (var offer in offers)
            {
                var dbOffer = new DbOffer
                {
                    Discount = offer.Discount,
                    OfferTime = offer.OfferTime,
                    IsDeleted = false,
                    OfferType = offer.OfferType,
                    Remark = offer.Remark,
                };
                dbOffers.Add(dbOffer);
            }
            return dbOffers;
        }
        #endregion
    }
}
