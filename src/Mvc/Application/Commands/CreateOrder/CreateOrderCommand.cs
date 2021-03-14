using MediatR;
using System.Collections.Generic;

namespace Mvc.Application.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<int>
    {
        public int CustomerId { get; set; }

        public int? VoucherPercentageDiscount { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public class OrderItem
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
