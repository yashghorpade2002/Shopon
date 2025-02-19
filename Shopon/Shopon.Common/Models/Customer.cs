using System.Runtime.InteropServices;

namespace Shopon.Common.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public string ASPNetUserId { get; set; } = string.Empty;

        public CustomerAddress CustomerAddress { get; set; }
        private List<Order> orders = new List<Order>();

        public void AddOrder(Order order) => this.orders.Add(order);

        public IEnumerable<Order> GetOrders() => this.orders;
    }
}
