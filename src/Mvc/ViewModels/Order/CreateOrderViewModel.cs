using Domain.Models;
using Mvc.Application.Commands.CreateOrder;
using System.Collections.Generic;

namespace Mvc.ViewModels.Order
{
    public class CreateOrderViewModel
    {
        public CreateOrderCommand Command { get; set; }
        public Customer Customer { get; set; }
        public List<Product> Products { get; set; }
    }
}
