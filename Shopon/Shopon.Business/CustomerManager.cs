using Shopon.Business.Contracts;
using Shopon.Common.Models;
using Shopon.Data.Contracts;

namespace Shopon.Business
{
    public class CustomerManager : ICustomerManager
    {
        private ICustomerRepository customerRepository;
        public CustomerManager(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public Customer? GetCustomerById(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
