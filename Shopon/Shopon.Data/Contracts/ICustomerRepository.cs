using Shopon.Common.Models;

namespace Shopon.Data.Contracts
{
    /// <summary>
    /// The ICustomerRepository
    /// </summary>
    public interface ICustomerRepository
    {
        //public Customer AddCustomer(Customer customer); // We will implement this later
        
        
        /// <summary>
        /// Method to Get Customer By Id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Customer GetCustomerById(int customerId);

    }
}
