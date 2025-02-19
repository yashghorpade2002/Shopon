using Shopon.Common.Models;

namespace Shopon.Data.Contracts
{
    public interface IOrderRepository
    {
        public Order AddOrder(Order order);
    }
}
