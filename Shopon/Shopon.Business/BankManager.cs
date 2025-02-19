using Shopon.Business.Contracts;
using Shopon.Common.Models;
using Shopon.Data.Contracts;

namespace Shopon.Business
{
    public class BankManager : IBankManager
    {
        private readonly IBankRepository bankRepository;
        public BankManager(IBankRepository bankRepository)
        {
            this.bankRepository = bankRepository;
        }

        public Bank AddBank(Bank bank) => this.bankRepository.AddBank(bank);

        public IEnumerable<Bank>? GetAllBanks() => this.bankRepository.GetAllBanks();
    }
}
