using Domain.Exceptions;
using Domain.Shared;
using System;

namespace Domain.Models
{
    public class OrderItem : IEntity
    {
        public int? OrderId { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }

        // EF private constructor
        private OrderItem() { } 

        public OrderItem(int? orderId, int productId, int quantity)
        {
            if (quantity <= 0)
            {
                throw new DomainException("Quantity has to be greater than one.");
            }
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
