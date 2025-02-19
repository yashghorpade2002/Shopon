using Shopon.Common.Models;

namespace Shopon.Business.Contracts
{
    /// <summary>
    /// The ICompanyManager
    /// </summary>
    public interface ICompanyManager
    {
        /// <summary>
        /// Mathod to add company
        /// </summary>
        /// <param name="company"></param>
        /// <returns>Newely added company</returns>
        public Company AddCompany(Company company);

        /// <summary>
        /// Mathod to get companies
        /// </summary>
        /// <returns>List of companies</returns>
        public IEnumerable<Company> GetCompanies();
        /// <summary>
        /// The GetCompanyById
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If found returns Company By Company Id Else Null</returns>
        public Company GetCompanyById(int id);
    }
}
