using Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mvc.ViewModels.Order
{
    public class CreateOrderViewModel
    {
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; } 
            // To prevent null exception error, when foreaching collection.
            = new List<OrderItemViewModel>();

        public class OrderItemViewModel
        {
            public string ProductName { get; set; }

            [Required]
            public int ProductId { get; set; }

            [Required]
            public int Quantity { get; set; }
        }
    }
}
