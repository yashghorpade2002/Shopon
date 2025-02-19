using Microsoft.EntityFrameworkCore;
using Shopon.Data.Contracts;
using Shopon.EF.Models;

namespace Shopon.EF
{
    public class CompanyAsyncEFRepository : ICompanyAsyncRepository
    {
        private readonly DbShoponContext context;

        public CompanyAsyncEFRepository(DbShoponContext context)
        {
            this.context = context;
        }

        public async Task<Common.Models.Company?> GetCompanyByIdAsync(int id)
        {
            try
            {
                var dbCompany = await context
                                    .Companies
                                    .AsNoTracking()
                                    .Where(x => x.CompanyId == id && x.IsDeleted == false)
                                    .FirstOrDefaultAsync();

                Common.Models.Company company = new Common.Models.Company
                {
                    CompanyId = dbCompany.CompanyId,
                    CompanyName = dbCompany.CompanyName
                };

                return company;

            } catch (Exception ex)
            {
                // log
                throw;
            }
        }
    }
}
