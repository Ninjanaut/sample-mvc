using Domain.Models;
using Domain.Services;
using MediatR;
using Mvc.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mvc.Application.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        // We might use repository pattern, if we need to decouple EF.
        private readonly AppDbContext _context;
        private readonly IDiscountValidator _discountValidator;
        public CreateOrderCommandHandler(AppDbContext context, IDiscountValidator discountValidator)
        {
            _context = context;
            _discountValidator = discountValidator;
        }
        public Task<int> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderItems = new List<OrderItem>();

            foreach (CreateOrderCommand.OrderItem orderItem in command.OrderItems)
            {
                orderItems.Add(
                    new OrderItem(
                        orderId: null,
                        productId: orderItem.ProductId,
                        quantity: orderItem.Quantity));
            }

            var order = new Order(command.CustomerId, orderItems);

            // The SetDiscount operation can also be part of Order constructor,
            // this is just an example of calling some operation.
            if (command.VoucherPercentageDiscount != null)
            {
                order.SetDiscount((int)command.VoucherPercentageDiscount, _discountValidator);
            }

            _context.Orders.Add(order);

            _context.SaveChanges();

            return Task.FromResult((int)order.Id);
        }
    }
}
