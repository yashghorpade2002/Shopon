using Shopon.Business.Contracts;
using Shopon.Common.Models;
using Shopon.Data.Contracts;

namespace Shopon.Business
{
    public class CompanyManager : ICompanyManager
    {
        private readonly ICompanyRepository companyRepository;
        public CompanyManager(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public Company AddCompany(Company company) => this.companyRepository.AddCompany(company);

        public IEnumerable<Company> GetCompanies() => this.companyRepository.GetCompanies();

        public Company GetCompanyById(int id) => this.companyRepository.GetCompanyById(id);
    }
}
