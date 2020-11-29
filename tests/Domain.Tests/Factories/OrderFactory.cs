using Domain.Models;
using System.Collections.Generic;

namespace Domain.Tests.Factories
{
    public static class OrderFactory
    {
        /// <summary>
        /// Returns Order with two order items.
        /// </summary>
        public static Order CreateSimpleOrder()
        {
            return new Order(customerId: 1,
                new List<OrderItem>()
                {
                    new OrderItem(orderId: 1, productId: 1, quantity: 1),
                    new OrderItem(orderId: 1, productId: 2, quantity: 1),
                }) ;
        }
    }
}
