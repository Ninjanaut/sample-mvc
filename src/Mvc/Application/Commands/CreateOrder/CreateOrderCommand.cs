using MediatR;
using System.Collections.Generic;

namespace Mvc.Application.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<int>
    {
        /// <summary>
        /// Returns Domain.Models.Order.Id
        /// </summary>
        public CreateOrderCommand(
            int customerId, 
            List<OrderItemDto> orderItems, 
            int voucherPercentageDiscount)
        {
            CustomerId = customerId;
            OrderItems = orderItems;
            VoucherPercentageDiscount = voucherPercentageDiscount;
        }

        public int CustomerId { get; private set; }

        public int? VoucherPercentageDiscount { get; private set; }

        public List<OrderItemDto> OrderItems { get; private set; }

        public class OrderItemDto
        {
            public OrderItemDto(int productId, int quantity)
            {
                ProductId = productId;
                Quantity = quantity;
            }
            public int ProductId { get; private set; }
            public int Quantity { get; private set; }
        }
    }
}
