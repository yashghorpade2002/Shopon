using Shopon.Common.Models;
using Shopon.Data.Contracts;

namespace Shopon.Data
{
    /// <summary>
    /// The CompanyListRepository
    /// </summary>
    public class CompanyListRepository : ICompanyRepository
    {
        private List<Company> companyList = new List<Company>();
        public Company AddCompany(Company company)
        {
            companyList.Add(company);
            return company;
        }
        public IEnumerable<Company> GetCompanies() => companyList;

        public Company GetCompanyById(int id) => companyList.FirstOrDefault(x => x.CompanyId == id);
        //{
        //    //Company company1 = null;
        //    //foreach (var company in this.companyList)
        //    //{
        //    //    if(company.CompanyId == id)
        //    //    {
        //    //        company1 = company;
        //    //        break;
        //    //    }

        //    //}
        //    //return company1;

        //    return this.companyList.FirstOrDefault(x => x.CompanyId == id);
        //}
    }
}
