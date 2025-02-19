using Shopon.Common.Models;

namespace Shopon.Data.Contracts
{
    /// <summary>
    /// The ICompanyAsyncRepository
    /// </summary>
    public interface ICompanyAsyncRepository
    {
        /// <summary>
        /// Method to get the company by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Company if found else null</returns>
        public Task<Company?> GetCompanyByIdAsync(int id);
    }
}
