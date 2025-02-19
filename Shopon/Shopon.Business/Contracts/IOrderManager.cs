using Shopon.Common.Models;

namespace Shopon.Business.Contracts
{
    public interface IOrderManager
    {
        /// <summary>
        /// Method to add order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public Order AddOrder(Order order);
    }
}
