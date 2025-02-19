using Shopon.Common.Models;

namespace Shopon.Business.Contracts
{
    /// <summary>
    /// The ICompanyAsyncManager
    /// </summary>
    public interface ICompanyAsyncManager
    {
        /// <summary>
        /// Method to get the company by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Company if found else null</returns>
        public Task<Company?> GetCompanyByIdAsync(int id);
    }
}
