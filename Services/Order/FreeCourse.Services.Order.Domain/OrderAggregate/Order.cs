using FreeCourse.Services.Order.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public DateTime CreatedDate { get; set; }
        public Address Address { get; set; }
        public string BuyerId { get; set; }
        private readonly List<OrderItem> _orderitems;
        public IReadOnlyCollection<OrderItem> Orderitems => _orderitems;
        public Order(Address address, string buyerId)
        {
            _orderitems = new List<OrderItem>();
            CreatedDate = DateTime.UtcNow;
            Address = address;
            BuyerId = buyerId;
        }
        public void AddOrderItems(string productId, string productName, decimal price, string pictureUrl)
        {
            var existProduct = _orderitems.Any(x => x.ProductId == productId);
            if (!existProduct)
            {
                var newOrderItem = new OrderItem(productId, productName, pictureUrl, price);
                _orderitems.Add(newOrderItem);
            }
        }

        public decimal GetTotalPrice => _orderitems.Sum(x => x.Price);
    }
}
