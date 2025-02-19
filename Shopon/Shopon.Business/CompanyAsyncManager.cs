using Shopon.Business.Contracts;
using Shopon.Common.Models;
using Shopon.Data.Contracts;

namespace Shopon.Business
{
    public class CompanyAsyncManager : ICompanyAsyncManager
    {
        private readonly ICompanyAsyncRepository companyrepo;

        public CompanyAsyncManager(ICompanyAsyncRepository companyrepo)
        {
            this.companyrepo = companyrepo;
        }
        public Task<Company?> GetCompanyByIdAsync(int id)
                => this.companyrepo.GetCompanyByIdAsync(id);
    }
}
