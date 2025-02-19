namespace Shopon.Common.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public Customer Customer { get; set; }
        public double OrderTotal
        {
            get
            {
                double total = 0;
                foreach(var item in orderItems)
                {
                    total += item.Amount;
                }
                return total;
            }
        }

        private List<OrderItem> orderItems = new List<OrderItem>();
        
        public void AddOrderItem(OrderItem item) => this.orderItems.Add(item);

        public IEnumerable<OrderItem> GetOrderItems() => this.orderItems;
    }
}
