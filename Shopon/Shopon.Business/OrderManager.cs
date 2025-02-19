using Shopon.Business.Contracts;
using Shopon.Common.Models;
using Shopon.Data.Contracts;

namespace Shopon.Business
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public Order AddOrder(Order order) => this.orderRepository.AddOrder(order);
    }
}
