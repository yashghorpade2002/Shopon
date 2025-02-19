using Shopon.Common.Models;

namespace Shopon.Data.Contracts
{
    /// <summary>
    /// The IBankRepository
    /// </summary>
    public interface IBankRepository
    {
        /// <summary>
        /// Method to add the bank
        /// </summary>
        /// <param name="bank"></param>
        /// <returns></returns>
        public Bank AddBank(Bank bank);

        /// <summary>
        /// Method to get all banks
        /// </summary>
        /// <returns>List of all the banks</returns>
        public IEnumerable<Bank>? GetAllBanks();
    }
}
